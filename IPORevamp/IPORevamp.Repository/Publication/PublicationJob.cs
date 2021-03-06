﻿using IPORevamp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using EmailEngine.Base.Entities;
using IPORevamp.Repository.Email;
using IPORevamp.Data.Entities.Email;
using Microsoft.Extensions.Configuration;
using IPORevamp.Data.Entity.Interface;
using Microsoft.EntityFrameworkCore;

namespace IPORevamp.Repository.Publication
{
   public  class PublicationJob : IPublicationJob
    {
        private readonly IEmailTemplateRepository _EmailTemplateRepository;
        private readonly IConfiguration _configuration; 
        private readonly IPOContext _contex;
        private readonly IEmailSender _emailsender;
        public PublicationJob(IPOContext contex, IEmailTemplateRepository EmailTemplateRepository, IConfiguration configuration, IEmailSender emailsender)
       
        {
            _contex = contex;
            _EmailTemplateRepository = EmailTemplateRepository;
            _configuration = configuration;
            _emailsender = emailsender;

        }
       
        public   void PrintTime()
        {
            Console.WriteLine($"{DateTime.Now}");
        }


        public void CheckPublicationStatus()
        {
            var settings = (from c in _contex.Settings where c.SettingCode == IPOCONSTANT.PublicationMaxDay  select c).FirstOrDefault();

         

                //  settings.ItemValue
            var result = (from p in _contex.Application
                          join f in _contex.TrademarkApplicationHistory
                               on p.Id equals f.ApplicationID

                          where f.ToDataStatus ==DATASTATUS.Publication && f.ToStatus ==STATUS.Batch  && p.DataStatus ==DATASTATUS.Publication  && p.ApplicationStatus ==STATUS.Batch 
                              select f).ToList();

            // System.Console.Write("result count = " + result.Count);
            foreach (var results in result)
            {

                if ((results.DateCreated.AddDays(Convert.ToInt32(settings.ItemValue))) < DateTime.Now)
                {


                    var vpwallet = (from c in _contex.Application where c.Id == results.ApplicationID select c).FirstOrDefault();


                    string prevappstatus = vpwallet.ApplicationStatus;
                    string prevDatastatus = vpwallet.DataStatus;

                    vpwallet.ApplicationStatus =STATUS.Fresh ;
                    vpwallet.DataStatus = DATASTATUS.Certificate ;
                    var appid = results.ApplicationID;
                    var userid = results.userid;

                    _contex.SaveChanges();

                    int userroles = Convert.ToInt32(IPORoles.SuperAdministrator);
                       
                    _contex.AddAsync(new TrademarkApplicationHistory
                    {
                        ApplicationID = appid,
                        DateCreated = DateTime.Now,
                        TransactionID = "",
                        FromDataStatus = prevDatastatus,
                        trademarkcomment =STATUS.AutoMoveComment,
                        description = "",

                        ToDataStatus = DATASTATUS.Certificate ,
                        FromStatus = prevappstatus,
                        ToStatus = STATUS.Fresh ,
                        UploadsPath1 = "",
                        userid = 39,
                        Role =Convert.ToString(userroles)
                    });

                    //  if ((results.DateCreated.AddDays(Convert.ToInt32(PublicationDuration))) > DateTime.Now) {

                    _contex.SaveChanges();

                    SendEmail(Convert.ToInt32(vpwallet.userid));

                    //  }

                }

            }




            //   return "success";
            // return null;
        }

        public void CheckDesignPublicationStatus()
        {
            var settings = (from c in _contex.Settings where c.SettingCode == IPOCONSTANT.PublicationDesignPending select c).FirstOrDefault();
            var settingsDays = (from c in _contex.Settings where c.SettingCode == IPOCONSTANT.PublicationDesignNumberOfDays select c).FirstOrDefault();



            var details = _contex.DesignDataCount
         .FromSql($"GetDesignPublicationCount   @p0, @p1, @p2", parameters: new[] { DATASTATUS.Publication, STATUS.Pending, settingsDays.ItemValue })
        .FirstOrDefault();

            int resultcount = Convert.ToInt32(details.datacount);

            //  settings.ItemValue
           
            if (resultcount >= Convert.ToInt32(settings.ItemValue))
            {
                SendPublicationOfficer();
                SendRegistraEmail();
            }

         




            //   return "success";
            // return null;
        }


        public void SendMonthyUserReport()
        {
            this.MonthlymailtoRegistra();

        }
            public void CheckPendingApplication()
        {
            var DesignPending = (from p in _contex.DesignApplication

                                 where p.ApplicationStatus == STATUS.Pending && p.DataStatus ==DATASTATUS.Search
                                 select p).ToList();

            var PatentPending = (from p in _contex.PatentApplication

                                 where p.ApplicationStatus == STATUS.Pending && p.DataStatus == DATASTATUS.Search
                                 select p).ToList();

            var TrademarkPending = (from p in _contex.Application

                                    where p.ApplicationStatus == STATUS.Pending && p.DataStatus == DATASTATUS.Search
                                    select p).ToList();

            foreach (var trademark in TrademarkPending)
            {
                SendEmailonPendingApplication(Convert.ToInt32(trademark.userid), "Trademark");


            }


            foreach (var patent in PatentPending)
            {
                SendEmailonPendingApplication(Convert.ToInt32(patent.userid), "Patent");


            }

            foreach (var design in DesignPending)
            {
                SendEmailonPendingApplication(Convert.ToInt32(design.userid), "Design");


            }

        }

            public void CheckPublicationCount()
        {
            var Countval  = (from c in _contex.Settings where c.SettingCode == IPOCONSTANT.PublicationCount select c).FirstOrDefault();

            var Publicationcount = (from p in _contex.Application

                                    where p.ApplicationStatus == STATUS.Fresh && p.DataStatus == DATASTATUS.Publication && p.Batchno == null
                                    select p).Count();

            if(Publicationcount  >=  Convert.ToInt32(Countval.ItemValue))
            {
                SendEmail();
            }


        }



        public async void SendRegistraEmail()
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.DesignPendingPublication && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();





            var roleid = Convert.ToInt32(IPORoles.RegistrarDesign);

            var ApplicationUser = (from c in _contex.Users where c.RolesId == roleid && c.department == DEPARTMENT.Design select c).ToList();
          
            // ApplicationUser[] currentUser = _contex.Users.FirstOrDefault(x => x.RolesId == roleid);

            foreach (var users in ApplicationUser)
            {
                var vname = users.FirstName + " " + users.LastName;

                string mailContent = emailtemplate.EmailBody;

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                mailContent = mailContent.Replace("#Name", vname);
                

                await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);



            }


        }


        public async void MonthlymailtoRegistra()
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.MonthlyMail && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();





            var roleid = Convert.ToInt32(IPORoles.RegistrarTrademark);
            var rolepatentid = Convert.ToInt32(IPORoles.RegistrarPatent);
            var roledesignid = Convert.ToInt32(IPORoles.RegistrarDesign);

            var ApplicationUser = (from c in _contex.Users where c.RolesId == roleid && c.department == DEPARTMENT.Trademark select c).ToList();
            var ApplicationPatentUser = (from c in _contex.Users where c.RolesId == rolepatentid && c.department == DEPARTMENT.Patent select c).ToList();
            var ApplicationDesignUser = (from c in _contex.Users where c.RolesId == roledesignid && c.department == DEPARTMENT.Design select c).ToList();

            // ApplicationUser[] currentUser = _contex.Users.FirstOrDefault(x => x.RolesId == roleid);
            DateTime now = DateTime.Today;

            foreach (var users in ApplicationUser)
            {
                var vname = users.FirstName + " " + users.LastName;

                string mailContent = emailtemplate.EmailBody;

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                mailContent = mailContent.Replace("#Name", vname);
                mailContent = mailContent.Replace("#month", now.ToString("MMM"));
                mailContent = mailContent.Replace("#year", now.ToString("yyyy"));

                mailContent = mailContent.Replace("#Link", _configuration["PAGEURL"] + "Trademarkuserlisting");


                await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);



            }

            foreach (var users in ApplicationPatentUser)
            {
                var vname = users.FirstName + " " + users.LastName;

                string mailContent = emailtemplate.EmailBody;

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                mailContent = mailContent.Replace("#Name", vname);
                mailContent = mailContent.Replace("#month", now.ToString("MMM"));
                mailContent = mailContent.Replace("#year", now.ToString("yyyy"));

                mailContent = mailContent.Replace("#Link", _configuration["PAGEURL"] + "Patentuserlisting");


                await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);



            }


            foreach (var users in ApplicationDesignUser)
            {
                var vname = users.FirstName + " " + users.LastName;

                string mailContent = emailtemplate.EmailBody;

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                mailContent = mailContent.Replace("#Name", vname);
                mailContent = mailContent.Replace("#month", now.ToString("MMM"));
                mailContent = mailContent.Replace("#year", now.ToString("yyyy"));

                mailContent = mailContent.Replace("#Link", _configuration["PAGEURL"] + "Designuserlisting");


                await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);



            }


        }


        public async void SendPublicationOfficer()
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.DesignPendingPublication && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();





            var roleid = Convert.ToInt32(IPORoles.Publication_Officer_Design);

            var ApplicationUser = (from c in _contex.Users where c.RolesId == roleid && c.department == DEPARTMENT.Design select c).ToList();

            // ApplicationUser[] currentUser = _contex.Users.FirstOrDefault(x => x.RolesId == roleid);

            foreach (var users in ApplicationUser)
            {
                var vname = users.FirstName + " " + users.LastName;

                string mailContent = emailtemplate.EmailBody;

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                mailContent = mailContent.Replace("#Name", vname);


                await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);



            }


        }

        public async  void SendEmail()
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.PublicationDue_Mail select c).FirstOrDefault();

            var user  = (from c in _contex.ApplicationUsers where (c.RolesId == Convert.ToInt32(IPORoles.RegistrarTrademark) || c.RolesId == Convert.ToInt32(IPORoles.Publication_Officer_Trade_Mark)) && c.department == DEPARTMENT.Trademark select c).ToList();
            // var user = _userManager.Users.Where(x => x.RolesId == roleid && x.department == model.department).ToList();
            foreach (var users in user)
            {
                var vname = users.FirstName + " " + users.LastName;

                string mailContent = emailtemplate.EmailBody;
                mailContent = mailContent.Replace("#Name", vname);
               
                //  mailContent = mailContent.Replace("#Password", password);
                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);



                // await _emailsender.SendEmailAsync(Setting[0].ItemValue, emailtemplate.EmailSubject, mailContent);
                await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);


            }
            }


        public async void SendEmail(int  userid)
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.CertificatePayment select c).FirstOrDefault();

            var user = (from c in _contex.ApplicationUsers where c.Id == userid select c).FirstOrDefault();
            // var user = _userManager.Users.Where(x => x.RolesId == roleid && x.department == model.department).ToList();
        
                var vname = user.FirstName + " " + user.LastName;

                string mailContent = emailtemplate.EmailBody;
                mailContent = mailContent.Replace("#Name", vname);

                //  mailContent = mailContent.Replace("#Password", password);
                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);



                // await _emailsender.SendEmailAsync(Setting[0].ItemValue, emailtemplate.EmailSubject, mailContent);
                await _emailsender.SendEmailAsync(user.Email, emailtemplate.EmailSubject, mailContent);


            //}
        }


        public async void SendEmailonPendingApplication(int userid,string app)
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.PendingApplication select c).FirstOrDefault();

            var user = (from c in _contex.ApplicationUsers where c.Id == userid select c).FirstOrDefault();
            // var user = _userManager.Users.Where(x => x.RolesId == roleid && x.department == model.department).ToList();

            var vname = user.FirstName + " " + user.LastName;

            string mailContent = emailtemplate.EmailBody;
            mailContent = mailContent.Replace("#Name", vname);
            mailContent = mailContent.Replace("#App", app);

            //  mailContent = mailContent.Replace("#Password", password);
            mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);



            // await _emailsender.SendEmailAsync(Setting[0].ItemValue, emailtemplate.EmailSubject, mailContent);
            await _emailsender.SendEmailAsync(user.Email, emailtemplate.EmailSubject, mailContent);


            //}
        }




        //   return "success";
        // return null;
    }



    }

