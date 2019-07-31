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
