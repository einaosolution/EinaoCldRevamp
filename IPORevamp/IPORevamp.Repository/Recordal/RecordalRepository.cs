using IPORevamp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.Search;
using IPORevamp.Data.Entity.Interface.Entities.Batch;
using System.IO;
using System.Net;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using EmailEngine.Base.Entities;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.Recordal;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.Pwallet;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;

namespace IPORevamp.Repository.Recordal
{
    class RecordalRepository : IRecordalRepository
    {

        private readonly IPOContext _contex;

        public RecordalRepository(IPOContext contex)
        {
            _contex = contex;


        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetApplicationByRegistrationId(String id)
        {
           
            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                               
                       


                                 where  c.RegistrationNumber == id && p.DataStatus==DATASTATUS.Certificate

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = d.FirstName + " " + d.LastName,
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.LogoPicture ,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,
                                    

                                     pwalletid = p.Id
                                    
                                 }).ToListAsync();
            return details;
            // return null;
        }




        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetApplicationByRegistrationId2(String id)
        {

            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id





                                 where c.RegistrationNumber == id  && p.DataStatus !=DATASTATUS.Migration

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = d.FirstName + " " + d.LastName,
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,


                                     pwalletid = p.Id

                                 }).ToListAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult> GetApplicationById(int applicationid)
        {
            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id





                                 where p.Id == applicationid 

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = d.FirstName + " " + d.LastName,
                                     ApplicantAddress = d.Street + " " + d.City ,
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,
                                     NextrenewalDate = p.NextRenewalDate.ToString(),

                                     pwalletid = p.Id

                                 }).FirstOrDefaultAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalRenewal> GetRenewalApplicationById(int applicationid)
        {

            var details = await (from p in _contex.RecordalRenewal




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }



        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalPatentRenewal> GetRenewalPatentApplicationById(int applicationid)
        {

            var details = await (from p in _contex.RecordalPatentRenewal




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalDesignRenewal> GetRenewalDesignApplicationById(int applicationid)
        {

            var details = await (from p in _contex.RecordalDesignRenewal




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }
        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger> GetMergerApplicationById(int applicationid)
        {

            var details = await (from p in _contex.RecordalMerger




                                 where p.Id == applicationid

                                 select p).Include(a => a.Country).FirstOrDefaultAsync();
            return details;
            // return null;
        }



        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger> GetMergerApplication()
        {

            var details = await (from p in _contex.RecordalMerger

                                                                                                                               

                                 select p).Include(a => a.Country).FirstOrDefaultAsync();
            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.ChangeOfName> GetChangeOfNameApplicationById(int applicationid)
        {

            var details = await (from p in _contex.ChangeOfName




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.ChangeOfAddress> GetChangeOfAddressApplicationById(int applicationid)
        {

            var details = await (from p in _contex.ChangeOfAddress




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }


       
        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger> GetMergerApplicationByAppId(int applicationid)
        {

            var details = await (from p in _contex.RecordalMerger




                                 where p.applicationid == applicationid

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }



        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger> GetMergerApplicationByAppId2(int applicationid)
        {

            var details = await (from p in _contex.RecordalMerger




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalRenewal>> GetRenewalApplicationByDocumentId(int applicationid)
        {

            var details = await (from p in _contex.RecordalRenewal




                                 where p.Id == applicationid

                                 select p).ToListAsync();
            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<Int32> Saveform(IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalRenewal RecordalRenewal)
        {

            _contex.RecordalRenewal.Add(RecordalRenewal);

            _contex.SaveChanges();


            return RecordalRenewal.Id;
           
        }

        public async System.Threading.Tasks.Task<Int32> SaveDesignform(IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalDesignRenewal RecordalRenewal)
        {

            _contex.RecordalDesignRenewal.Add(RecordalRenewal);

            _contex.SaveChanges();


            return RecordalRenewal.Id;

        }


        public async System.Threading.Tasks.Task<Int32> SavePatentform(IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalPatentRenewal RecordalRenewal)
        {

            _contex.RecordalPatentRenewal.Add(RecordalRenewal);

            _contex.SaveChanges();


            return RecordalRenewal.Id;

        }

        public async System.Threading.Tasks.Task<Int32> Saveform(IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger RecordalMerger)
        {

            _contex.RecordalMerger.Add(RecordalMerger);

            _contex.SaveChanges();


            return RecordalMerger.Id;

        }

        public async System.Threading.Tasks.Task<Int32> Savechangeofname(IPORevamp.Data.Entity.Interface.Entities.Recordal.ChangeOfName Recordalchangeofname)
        {

            _contex.ChangeOfName.Add(Recordalchangeofname);

            _contex.SaveChanges();


            return Recordalchangeofname.Id;

        }

        public async System.Threading.Tasks.Task<Int32> Savechangeofaddress(IPORevamp.Data.Entity.Interface.Entities.Recordal.ChangeOfAddress Recordalchangeofname)
        {

            _contex.ChangeOfAddress.Add(Recordalchangeofname);

            _contex.SaveChanges();


            return Recordalchangeofname.Id;

        }

        public async System.Threading.Tasks.Task<Int32> Updateform(string Name , string Address, string Comment, string filepath,  string filepath2, string Type ,int NoticeAppID)
        {


            var details = await (from p in _contex.RecordalRenewal




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.ApplicantName = Name;
            details.ApplicantAddress = Address;
            details.DetailOfRequest = Comment;
            details.PowerOfAttorney = filepath;
            details.CertificateOfTrademark = filepath2;
            details.RenewalType = Type;

            _contex.SaveChanges();


            return details.Id;

        }

        public async System.Threading.Tasks.Task<Int32> UpdateDesignform(string Name, string Address, string Comment, string filepath, string filepath2, string Type, int NoticeAppID)
        {


            var details = await (from p in _contex.RecordalDesignRenewal




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.ApplicantName = Name;
            details.ApplicantAddress = Address;
            details.DetailOfRequest = Comment;
            details.PowerOfAttorney = filepath;
            details.CertificateOfTrademark = filepath2;
            details.RenewalType = Type;

            _contex.SaveChanges();


            return details.Id;

        }

        public async System.Threading.Tasks.Task<Int32> UpdatePatentform(string Name, string Address, string Comment, string filepath, string filepath2, string Type, int NoticeAppID)
        {


            var details = await (from p in _contex.RecordalPatentRenewal




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.ApplicantName = Name;
            details.ApplicantAddress = Address;
            details.DetailOfRequest = Comment;
            details.PowerOfAttorney = filepath;
            details.CertificateOfTrademark = filepath2;
            details.RenewalType = Type;

            _contex.SaveChanges();


            return details.Id;

        }

        public async System.Threading.Tasks.Task<Int32> Updateform(string Name, string Address, string Name2, string Address2, string Comment, string filepath, string filepath2, string filepath3, string MergerDate,int Nationality, int NoticeAppID)
        {


            var details = await (from p in _contex.RecordalMerger




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.AssignorName = Name;
            details.AssignorAddress = Address;
            details.AssigneeName = Name2;
            details.AssigneeAddress = Address2;
            details.DetailOfRequest = Comment;
            details.DateOfAssignment = MergerDate;
            details.AssigneeNationality = Convert.ToInt32(Nationality);
            if (filepath != "")
            {
                details.PowerOfAttorney = filepath;

            }

            if (filepath3 != "")
            {
                details.Certificate = filepath3;

            }

            if (filepath2 != "")
            {
                details.DeedOfAssigment = filepath2;

            }


            _contex.SaveChanges();


            return details.Id;

        }

        public async System.Threading.Tasks.Task<Int32> Updatechangeofname(string newfirstname, string newlastname , int NoticeAppID)
        {


            var details = await (from p in _contex.ChangeOfName




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.NewApplicantFirstname = newfirstname;
            details.NewApplicantSurname = newlastname;
           
           


            _contex.SaveChanges();


            return details.Id;

        }

        public async System.Threading.Tasks.Task<Int32> Updatechangeofaddress(string newaddress, int NoticeAppID)
        {


            var details = await (from p in _contex.ChangeOfAddress




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.NewApplicantAddress = newaddress;





            _contex.SaveChanges();


            return details.Id;

        }


        public async System.Threading.Tasks.Task<Int32> UpdateRecord(string roleid, string TransactionId,int NoticeAppID,int userid)
        {


            var details = await (from p in _contex.RecordalRenewal




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.PaymentReference = TransactionId;
            details.Status =STATUS.Paid;

            var appid = details.applicationid;

            var vpwallet = (from c in _contex.Application where c.Id == appid select c).FirstOrDefault();


            string prevappstatus = vpwallet.ApplicationStatus;
            string prevDatastatus = vpwallet.DataStatus;



            if (vpwallet != null)
            {

                vpwallet.ApplicationStatus =STATUS.Renewal;
                vpwallet.DataStatus =DATASTATUS.Recordal ;

                _contex.SaveChanges();

                // get User Information




            }



            // file upload
            string msg = "";



      _contex.Add(new TrademarkApplicationHistory
            {
                ApplicationID = appid,
                DateCreated = DateTime.Now,
                TransactionID = TransactionId,
                FromDataStatus = prevDatastatus,
                trademarkcomment =STATUS.RecordalRenewalComment,
                description = "",

                ToDataStatus =DATASTATUS.Recordal ,
                FromStatus = prevappstatus,
                ToStatus = STATUS.Renewal,
                UploadsPath1 = "",
                userid = userid,
                Role = Convert.ToString(roleid)
            });
            _contex.SaveChanges();

            return details.Id;

        }

        public async System.Threading.Tasks.Task<Int32> UpdateDesignRecord(string roleid, string TransactionId, int NoticeAppID, int userid)
        {


            var details = await (from p in _contex.RecordalDesignRenewal




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.PaymentReference = TransactionId;
            details.Status = STATUS.Paid;

            var appid = details.applicationid;

            var vpwallet = (from c in _contex.DesignApplication where c.Id == appid select c).FirstOrDefault();


            string prevappstatus = vpwallet.ApplicationStatus;
            string prevDatastatus = vpwallet.DataStatus;



            if (vpwallet != null)
            {

                vpwallet.ApplicationStatus = STATUS.Renewal;
                vpwallet.DataStatus = DATASTATUS.Recordal;

                _contex.SaveChanges();

                // get User Information




            }



            // file upload
            string msg = "";


        _contex.Add(new DesignApplicationHistory
            {
                DesignApplicationID = appid,
                DateCreated = DateTime.Now,
                TransactionID = TransactionId,
                FromDataStatus = prevDatastatus,
                patentcomment = STATUS.RecordalRenewalComment,
                description = "",

                ToDataStatus = DATASTATUS.Recordal,
                FromStatus = prevappstatus,
                ToStatus = STATUS.Renewal,
                UploadsPath1 = "",
                userid = userid,
                Role = Convert.ToString(roleid)
            });
            _contex.SaveChanges();

            return details.Id;

        }



        public async System.Threading.Tasks.Task<Int32> UpdatePatentRecord(string roleid, string TransactionId, int NoticeAppID, int userid)
        {


            var details = await (from p in _contex.RecordalPatentRenewal




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.PaymentReference = TransactionId;
            details.Status = STATUS.Paid;

            var appid = details.applicationid;

            var vpwallet = (from c in _contex.PatentApplication where c.Id == appid select c).FirstOrDefault();


            string prevappstatus = vpwallet.ApplicationStatus;
            string prevDatastatus = vpwallet.DataStatus;



            if (vpwallet != null)
            {

                vpwallet.ApplicationStatus = STATUS.Renewal;
                vpwallet.DataStatus = DATASTATUS.Recordal;

                _contex.SaveChanges();

                // get User Information




            }



            // file upload
            string msg = "";



    _contex.Add(new PatentApplicationHistory
            {
                PatentApplicationID = appid,
                DateCreated = DateTime.Now,
                TransactionID = TransactionId,
                FromDataStatus = prevDatastatus,
                patentcomment = STATUS.RecordalRenewalComment,
                description = "",

                ToDataStatus = DATASTATUS.Recordal,
                FromStatus = prevappstatus,
                ToStatus = STATUS.Renewal,
                UploadsPath1 = "",
                userid = userid,
                Role = Convert.ToString(roleid)
            });
            _contex.SaveChanges();

            return details.Id;

        }

        public async System.Threading.Tasks.Task<Int32> UpdateMergerRecord(string roleid, string TransactionId, int NoticeAppID, int userid)
        {


            var details = await (from p in _contex.RecordalMerger




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.PaymentReference = TransactionId;
            details.Status =STATUS.Paid ;
            var appid = details.applicationid;

            var vpwallet = (from c in _contex.Application where c.Id == appid select c).FirstOrDefault();


            string prevappstatus = vpwallet.ApplicationStatus;
            string prevDatastatus = vpwallet.DataStatus;



            if (vpwallet != null)
            {

                vpwallet.ApplicationStatus =STATUS.Merger ;
                vpwallet.DataStatus =DATASTATUS.Recordal ;



                // get User Information




            }



            // file upload
            string msg = "";



            await _contex.AddAsync(new TrademarkApplicationHistory
            {
                ApplicationID = appid,
                DateCreated = DateTime.Now,
                TransactionID = TransactionId,
                FromDataStatus = prevDatastatus,
                trademarkcomment =STATUS.RecordalComment ,
                description = "",

                ToDataStatus =DATASTATUS.Recordal ,
                FromStatus = prevappstatus,
                ToStatus = STATUS.Merger ,
                UploadsPath1 = "",
                userid = userid,
                Role = Convert.ToString(roleid)
            });

            _contex.SaveChanges();


            return details.Id;

        }


        public async System.Threading.Tasks.Task<Int32> UpdateChangeOfNameRecord(string roleid, string TransactionId, int NoticeAppID, int userid)
        {


            var details = await (from p in _contex.ChangeOfName




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.PaymentReference = TransactionId;
            details.Status = STATUS.Paid;
            var appid = details.applicationid;

            var vpwallet = (from c in _contex.Application where c.Id == appid select c).FirstOrDefault();


            string prevappstatus = vpwallet.ApplicationStatus;
            string prevDatastatus = vpwallet.DataStatus;



            if (vpwallet != null)
            {

                vpwallet.ApplicationStatus = STATUS.ChangeOfName;
                vpwallet.DataStatus = DATASTATUS.Recordal;



                // get User Information




            }



            // file upload
            string msg = "";



            await _contex.AddAsync(new TrademarkApplicationHistory
            {
                ApplicationID = appid,
                DateCreated = DateTime.Now,
                TransactionID = TransactionId,
                FromDataStatus = prevDatastatus,
                trademarkcomment = STATUS.RecordalComment,
                description = "",

                ToDataStatus = DATASTATUS.Recordal,
                FromStatus = prevappstatus,
                ToStatus = STATUS.ChangeOfName,
                UploadsPath1 = "",
                userid = userid,
                Role = Convert.ToString(roleid)
            });

            _contex.SaveChanges();


            return details.Id;

        }


        public async System.Threading.Tasks.Task<Int32> UpdateChangeOfAddressRecord(string roleid, string TransactionId, int NoticeAppID, int userid)
        {


            var details = await (from p in _contex.ChangeOfAddress




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.PaymentReference = TransactionId;
            details.Status = STATUS.Paid;
            var appid = details.applicationid;

            var vpwallet = (from c in _contex.Application where c.Id == appid select c).FirstOrDefault();


            string prevappstatus = vpwallet.ApplicationStatus;
            string prevDatastatus = vpwallet.DataStatus;



            if (vpwallet != null)
            {

                vpwallet.ApplicationStatus = STATUS.ChangeOfAddress;
                vpwallet.DataStatus = DATASTATUS.Recordal;



                // get User Information




            }



            // file upload
            string msg = "";



            await _contex.AddAsync(new TrademarkApplicationHistory
            {
                ApplicationID = appid,
                DateCreated = DateTime.Now,
                TransactionID = TransactionId,
                FromDataStatus = prevDatastatus,
                trademarkcomment = STATUS.RecordalComment,
                description = "",

                ToDataStatus = DATASTATUS.Recordal,
                FromStatus = prevappstatus,
                ToStatus = STATUS.ChangeOfAddress,
                UploadsPath1 = "",
                userid = userid,
                Role = Convert.ToString(roleid)
            });

            _contex.SaveChanges();


            return details.Id;

        }

        public async System.Threading.Tasks.Task<String> UpdateRennewalRecord(int NoticeAppID)
        {

            var details = await (from p in _contex.RecordalDesignRenewal




                                 where p.applicationid == NoticeAppID && p.Status == STATUS.Paid

                                 select p).FirstOrDefaultAsync();
            var pid = details.applicationid;
            details.Status = STATUS.Approved;

            _contex.SaveChanges();


            //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

            // get User Information





            return "success";

        }

        public async System.Threading.Tasks.Task<Int32> UpdateRennewalRecord(int NoticeAppID, int userid)
        {

            var details =  (from p in _contex.RecordalRenewal




                                 where p.Id == NoticeAppID && p.Status == STATUS.Paid

                                 select p).FirstOrDefault();



            var payref = details.PaymentReference;




            TrademarkApplicationHistory  results =  (from p in _contex.TrademarkApplicationHistory
                                 where p.ApplicationID == details.applicationid && p.ToStatus ==STATUS.Renewal && p.ToDataStatus ==DATASTATUS.Recordal && p.TransactionID == payref



                                select p).FirstOrDefault();
         int  pid = details.applicationid;
            details.Status = STATUS.Approved;
           

            //   _contex.SaveChanges();
            try
            {
                Application detail2 = (from p2 in _contex.Application

                               where p2.Id == details.applicationid

                               select p2).FirstOrDefault();
              DateTime ttdate = DateTime.Now.AddYears(5);
                 detail2.NextRenewalDate = ttdate;
                detail2.ApplicationStatus = results.FromStatus;
                detail2.DataStatus = results.FromDataStatus;

                _contex.SaveChanges();

            }

            catch(Exception ee)
            {
                string message = ee.Message;
            }
            //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

            // get User Information





            return details.Id;

        }


        public async System.Threading.Tasks.Task<Int32> UpdateRennewalDesignRecord(int NoticeAppID, int userid)
        {

            var details = (from p in _contex.RecordalDesignRenewal




                           where p.Id == NoticeAppID && p.Status == STATUS.Paid

                           select p).FirstOrDefault();



            var payref = details.PaymentReference;




            DesignApplicationHistory results = (from p in _contex.DesignApplicationHistory
                                                   where p.DesignApplicationID == details.applicationid && p.ToStatus == STATUS.Renewal && p.ToDataStatus == DATASTATUS.Recordal && p.TransactionID == payref



                                                   select p).FirstOrDefault();
            int pid = details.applicationid;
            details.Status = STATUS.Approved;
            details.DateApproved = DateTime.Now;

            details.ApprovedBy = Convert.ToString(userid);




            //   _contex.SaveChanges();
            try
            {
                DesignApplication detail2 = (from p2 in _contex.DesignApplication

                                       where p2.Id == details.applicationid

                                       select p2).FirstOrDefault();
               // DateTime ttdate = DateTime.Now.AddYears(5);
               // detail2.NextRenewalDate = ttdate;
                detail2.ApplicationStatus = results.FromStatus;
                detail2.DataStatus = results.FromDataStatus;

                _contex.SaveChanges();

            }

            catch (Exception ee)
            {
                string message = ee.Message;
            }
            //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

            // get User Information





            return details.Id;

        }

        public async System.Threading.Tasks.Task<Int32> UpdateRennewalPatentRecord(int NoticeAppID, int userid)
        {

            var details = (from p in _contex.RecordalPatentRenewal




                           where p.Id == NoticeAppID && p.Status == STATUS.Paid

                           select p).FirstOrDefault();



            var payref = details.PaymentReference;




            PatentApplicationHistory results = (from p in _contex.PatentApplicationHistory
                                                where p.PatentApplicationID == details.applicationid && p.ToStatus == STATUS.Renewal && p.ToDataStatus == DATASTATUS.Recordal && p.TransactionID == payref



                                                select p).FirstOrDefault();
            int pid = details.applicationid;
            details.Status = STATUS.Approved;
            details.DateApproved = DateTime.Now;

            details.ApprovedBy = Convert.ToString(userid);




            //   _contex.SaveChanges();
            try
            {
                PatentApplication detail2 = (from p2 in _contex.PatentApplication

                                             where p2.Id == details.applicationid

                                             select p2).FirstOrDefault();
                // DateTime ttdate = DateTime.Now.AddYears(5);
                // detail2.NextRenewalDate = ttdate;
                detail2.ApplicationStatus = results.FromStatus;
                detail2.DataStatus = results.FromDataStatus;

                _contex.SaveChanges();

            }

            catch (Exception ee)
            {
                string message = ee.Message;
            }
            //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

            // get User Information





            return details.Id;

        }


        public async System.Threading.Tasks.Task<Int32> UpdateRennewalRecord2( int NoticeAppID, int userid,string Status,string   requestuser)
        {

            var details = await (from p in _contex.ChangeOfName




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();
            var pid = details.applicationid;
            details.Status = Status;
            details.ApprovalDate = DateTime.Now;
            details.ApprovedBy = requestuser;

            _contex.SaveChanges();

            var detail = await (from p in _contex.Application

                                 where p.Id == pid

                                 select p).FirstOrDefaultAsync();

            if(Status == STATUS.Approved)
            {
                var result  = await (from p in _contex.ApplicationUsers

                                    where p.Id == Convert.ToInt32(detail.userid)

                                     select p).FirstOrDefaultAsync();
                result.FirstName = details.NewApplicantFirstname;
                result.LastName = details.NewApplicantSurname;
                _contex.SaveChanges();

            }


            var detail2 = await (from p in _contex.TrademarkApplicationHistory

                                where p.ApplicationID == pid && p.TransactionID == details.PaymentReference &&  p.ToDataStatus == DATASTATUS.Recordal && p.ToStatus==STATUS.ChangeOfName

                                 select p).FirstOrDefaultAsync();

            detail.ApplicationStatus = detail2.FromStatus;
            detail.DataStatus = detail2.FromDataStatus;


            //  detail.NextRenewalDate = Convert.ToDateTime(detail.NextRenewalDate).AddYears(5);
            _contex.SaveChanges();
            //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

            // get User Information
       

          


            return details.Id;

        }


        public async System.Threading.Tasks.Task<Int32> UpdateRennewalRecord3(int NoticeAppID, int userid, string Status, string requestuser)
        {

            var details = await (from p in _contex.ChangeOfAddress




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();
            var pid = details.applicationid;
            details.Status = Status;
            details.ApprovalDate = DateTime.Now;
            details.ApprovedBy = requestuser;

            _contex.SaveChanges();

            var detail = await (from p in _contex.Application

                                where p.Id == pid

                                select p).FirstOrDefaultAsync();

            if (Status == STATUS.Approved)
            {
                var result = await (from p in _contex.ApplicationUsers

                                    where p.Id == Convert.ToInt32(detail.userid)

                                    select p).FirstOrDefaultAsync();
                result.Street = details.NewApplicantAddress;
               
                _contex.SaveChanges();

            }


            var detail2 = await (from p in _contex.TrademarkApplicationHistory

                                 where p.ApplicationID == pid && p.TransactionID == details.PaymentReference && p.ToDataStatus == DATASTATUS.Recordal && p.ToStatus == STATUS.ChangeOfAddress

                                 select p).FirstOrDefaultAsync();

            detail.ApplicationStatus = detail2.FromStatus;
            detail.DataStatus = detail2.FromDataStatus;


            //  detail.NextRenewalDate = Convert.ToDateTime(detail.NextRenewalDate).AddYears(5);
            _contex.SaveChanges();
            //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

            // get User Information





            return details.Id;

        }


        public async System.Threading.Tasks.Task<Int32> UpdateRennewalRecord4(int NoticeAppID, int userid, string Status, string requestuser)
        {

            var details = await (from p in _contex.RecordalMerger




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();
            var pid = details.applicationid;
            details.Status = Status;
            details.ApprovalDate = DateTime.Now;
           details.ApprovedBy = requestuser;

            _contex.SaveChanges();

            var detail = await (from p in _contex.Application

                                where p.Id == pid

                                select p).FirstOrDefaultAsync();

            if (Status == STATUS.Approved)
            {
                var result = await (from p in _contex.ApplicationUsers

                                    where p.Id == Convert.ToInt32(detail.userid)

                                    select p).FirstOrDefaultAsync();
                string[] words = details.AssignorName.Split(' ');
                result.FirstName = words[0];
                result.LastName = words[1];
                result.Street = details.AssignorAddress;
                result.CountryCode = Convert.ToString( details.AssigneeNationality);


                _contex.SaveChanges();

            }


            var detail2 = await (from p in _contex.TrademarkApplicationHistory

                                 where p.ApplicationID == pid && p.TransactionID == details.PaymentReference && p.ToDataStatus == DATASTATUS.Recordal && p.ToStatus == STATUS.ChangeOfAddress

                                 select p).FirstOrDefaultAsync();

            detail.ApplicationStatus = detail2.FromStatus;
            detail.DataStatus = detail2.FromDataStatus;


            //  detail.NextRenewalDate = Convert.ToDateTime(detail.NextRenewalDate).AddYears(5);
            _contex.SaveChanges();
            //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

            // get User Information





            return details.Id;

        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetIssuedCertificate()
        {

            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.RecordalRenewal

                                  on p.Id equals  f.applicationid





                                 where  p.NextRenewalDate != null

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = f.ApplicantName,
                                     ApplicantAddress = f.ApplicantAddress ,
                                     renewalstatus  = f.Status ,
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,

                                     certificatePaymentReference = p.CertificatePayReference,
                                     NextrenewalDate = p.NextRenewalDate.ToString(),
                                     pwalletid = p.Id

                                 }).ToListAsync();
            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalPatentCertificate()
        {

            var details = await (from p in _contex.PatentApplication
                                 join c in _contex.PatentInformation
                                  on p.Id equals c.PatentApplicationID
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.PatentType
                                 on c.PatentTypeID equals e.Id

                                 join f in _contex.RecordalPatentRenewal

                                  on p.Id equals f.applicationid





                                 where f.Status == STATUS.Paid

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = f.ApplicantName,
                                     ApplicantAddress = f.ApplicantAddress,
                                     renewalstatus = f.Status,
                                     ProductTitle = c.TitleOfInvention,
                                     Applicationclass = "",
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = "",
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = f.CertificateOfTrademark,
                                     auth_doc = f.PowerOfAttorney,
                                     sup_doc1 = c.LetterOfAuthorization,
                                     sup_doc2 = c.PctDocument,
                                     renewalid = Convert.ToString( f.Id) ,

                                     certificatePaymentReference = f.PaymentReference,
                                     NextrenewalDate = Convert.ToString(f.RenewalDueDate),
                                     pwalletid = p.Id

                                 }).ToListAsync();
            // return null;
            return details;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalPatentCertificate2()
        {

            var details = await (from p in _contex.PatentApplication
                                 join c in _contex.PatentInformation
                                  on p.Id equals c.PatentApplicationID
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.PatentType
                                 on c.PatentTypeID equals e.Id

                                 join f in _contex.RecordalPatentRenewal

                                  on p.Id equals f.applicationid





                                 where f.Status == STATUS.Approved

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = f.ApplicantName,
                                     ApplicantAddress = f.ApplicantAddress,
                                     renewalstatus = f.Status,
                                     ProductTitle = c.TitleOfInvention,
                                     Applicationclass = "",
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = "",
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = f.CertificateOfTrademark,
                                     auth_doc = f.PowerOfAttorney,
                                     sup_doc1 = c.LetterOfAuthorization,
                                     sup_doc2 = c.PctDocument,
                                     renewalid = Convert.ToString(f.Id),

                                     certificatePaymentReference = f.PaymentReference,
                                     NextrenewalDate = Convert.ToString(f.RenewalDueDate),
                                     pwalletid = p.Id

                                 }).ToListAsync();
            // return null;
            return details;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalDesignCertificate()
        {

            var details = await (from p in _contex.DesignApplication
                                 join c in _contex.DesignInformation
                                  on p.Id equals c.DesignApplicationID
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.DesignType
                                 on c.DesignTypeID equals e.Id

                                 join f in _contex.RecordalDesignRenewal

                                  on p.Id equals f.applicationid





                                 where f.Status==STATUS.Paid

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = f.ApplicantName,
                                     ApplicantAddress = f.ApplicantAddress,
                                     renewalstatus = f.Status,
                                     ProductTitle = c.TitleOfDesign,
                                     Applicationclass = Convert.ToString(c.NationClassID),
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.DesignDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     renewalid = Convert.ToString(f.Id),
                                     logo_pic = f.CertificateOfTrademark,
                                     auth_doc = f.PowerOfAttorney,
                                     sup_doc1 = c.RepresentationOfDesign1,
                                     sup_doc2 = c.RepresentationOfDesign2,

                                     certificatePaymentReference = f.PaymentReference,
                                     NextrenewalDate = Convert.ToString(f.RenewalDueDate),
                                     pwalletid = p.Id

                                 }).ToListAsync();
            // return null;
            return details;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalDesignCertificate2()
        {

            var details = await (from p in _contex.DesignApplication
                                 join c in _contex.DesignInformation
                                  on p.Id equals c.DesignApplicationID
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.DesignType
                                 on c.DesignTypeID equals e.Id

                                 join f in _contex.RecordalDesignRenewal

                                  on p.Id equals f.applicationid





                                 where f.Status == STATUS.Approved

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = f.ApplicantName,
                                     ApplicantAddress = f.ApplicantAddress,
                                     renewalstatus = f.Status,
                                     ProductTitle = c.TitleOfDesign,
                                     Applicationclass = Convert.ToString(c.NationClassID),
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.DesignDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = f.CertificateOfTrademark,
                                     auth_doc = f.PowerOfAttorney,
                                     sup_doc1 = c.RepresentationOfDesign1,
                                     sup_doc2 = c.RepresentationOfDesign2,

                                     certificatePaymentReference = p.CertificatePayReference,
                                     NextrenewalDate = Convert.ToString(f.RenewalDueDate),
                                     pwalletid = p.Id

                                 }).ToListAsync();
            // return null;
            return details;
        }
        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalCertificate()
        {

            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.RecordalRenewal

                                  on p.Id equals f.applicationid





                                 where p.ApplicationStatus ==STATUS.Renewal && p.DataStatus ==DATASTATUS.Recordal

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = f.ApplicantName,
                                     ApplicantAddress = f.ApplicantAddress,
                                     renewalstatus = f.Status,
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = f.Status,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,

                                     certificatePaymentReference = p.CertificatePayReference,
                                     NextrenewalDate = p.NextRenewalDate.ToString(),
                                     pwalletid = p.Id ,
                                     renewalid= Convert.ToString(f.Id)

                                 }).ToListAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalCertificate2()
        {

            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.RecordalRenewal

                                  on p.Id equals f.applicationid





                                 where f.Status  == STATUS.Approved

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = f.ApplicantName,
                                     ApplicantAddress = f.ApplicantAddress,
                                     renewalstatus = f.Status,
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = f.Status,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,

                                     certificatePaymentReference = p.CertificatePayReference,
                                     NextrenewalDate = p.NextRenewalDate.ToString(),
                                     pwalletid = p.Id,
                                     renewalid = Convert.ToString(f.Id)

                                 }).ToListAsync();
            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalChangeOfName(string status)
        {

            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.ChangeOfName

                                  on p.Id equals f.applicationid





                                 where  f.Status == status

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = f.NewApplicantFirstname,
                                     ApplicantAddress = f.NewApplicantSurname,
                                     renewalstatus = f.Status,
                                     renewalid = Convert.ToString(f.Id) ,
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = f.Status,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,

                                     
                                     pwalletid = p.Id
                                   

                                 }).ToListAsync();
            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult3>> GetRecordalMerger(string status)
        {

            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.RecordalMerger.Include(a => a.Country)

                                  on p.Id equals f.applicationid





                                 where f.Status == status

                                 select new DataResult3
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     AssignorName = f.AssignorName,
                                     AssignorAddress = f.AssignorAddress,
                                     AssigneeName = f.AssigneeName,
                                     AssigneeAddress = f.AssigneeAddress ,
                                     AssigneeNationality = f.Country.Name ,
                                     DetailOfRequest = f.DetailOfRequest,
                                     powerofattorney = f.PowerOfAttorney ,
                                     deedofassignment = f.DeedOfAssigment ,
                                     certificate = f.Certificate ,
                                    
                                     renewalstatus = f.Status,
                                     renewalid = Convert.ToString(f.Id),
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = f.Status,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,


                                     pwalletid = p.Id


                                 }).ToListAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<ChangeOfAddressView>> GetRecordalChangeOfAddress(string status)
        {

            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.ChangeOfAddress

                                  on p.Id equals f.applicationid





                                 where f.Status == status

                                 select new ChangeOfAddressView
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     OldApplicantAddress = f.OldApplicantAddress,
                                     NewApplicantAddress = f.NewApplicantAddress,
                                     renewalstatus = f.Status,
                                     renewalid = Convert.ToString(f.Id),
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = f.Status,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,


                                     pwalletid = p.Id


                                 }).ToListAsync();
            return details;
            // return null;
        }

    }
}
