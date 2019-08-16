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

                               
                       


                                 where  c.RegistrationNumber == id && p.NextRenewalDate != null

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





                                 where c.RegistrationNumber == id 

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


        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger> GetMergerApplicationById(int applicationid)
        {

            var details = await (from p in _contex.RecordalMerger




                                 where p.Id == applicationid

                                 select p).Include(a => a.Country).FirstOrDefaultAsync();
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

        public async System.Threading.Tasks.Task<Int32> Saveform(IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger RecordalMerger)
        {

            _contex.RecordalMerger.Add(RecordalMerger);

            _contex.SaveChanges();


            return RecordalMerger.Id;

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
                trademarkcomment = "Recordal Merger",
                description = "",

                ToDataStatus = "Recordal",
                FromStatus = prevappstatus,
                ToStatus = "Merger",
                UploadsPath1 = "",
                userid = userid,
                Role = Convert.ToString(roleid)
            });

            _contex.SaveChanges();


            return details.Id;

        }

        public async System.Threading.Tasks.Task<Int32> UpdateRennewalRecord( int NoticeAppID, int userid)
        {

            var details = await (from p in _contex.RecordalMerger




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();
            var pid = details.applicationid;
            details.Status = STATUS.Approved;

            _contex.SaveChanges();

            var detail = await (from p in _contex.Application

                                 where p.Id == pid

                                 select p).FirstOrDefaultAsync();

            detail.NextRenewalDate = Convert.ToDateTime(detail.NextRenewalDate).AddYears(5);
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





                                 where p.NextRenewalDate != null

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

    }
}
