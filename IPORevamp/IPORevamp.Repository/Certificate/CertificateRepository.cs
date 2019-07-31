using IPORevamp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.Search;

namespace IPORevamp.Repository.Certificate
{
    class CertificateRepository : ICertificateRepository
    {

        private readonly IPOContext _contex;

        public CertificateRepository(IPOContext contex)
        {
            _contex = contex;


        }


        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Certificate.PayCertificate> GetCertificatePaymentById(int applicationid)
        {

            var details = await (from p in _contex.PayCertificate




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<String > UpdateApplication(int applicationid)
        {
            var details = await (from p in _contex.Application




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            DateTime theDate = DateTime.Now;

            details.NextRenewalDate = theDate.AddYears(5);

            _contex.SaveChanges();

            return "success";
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplicationByUserid(string userid)
        {

            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.TrademarkApplicationHistory
                                 on p.Id equals f.ApplicationID


                                 where p.ApplicationStatus == "Fresh" && p.DataStatus == "Certificate" && f.ToDataStatus == "Certificate" && f.ToDataStatus == "Certificate" && p.userid == userid

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
                                     attach_doc = f.UploadsPath1,
                                     pwalletid = p.Id
                                    
                                 }).ToListAsync();
            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult> GetApplicationById(int id)
        {

            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                               


                                 where  p.Id ==id 

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
                                     ApplicantAddress =d.Street + " " + d.City ,
                                     
                                     pwalletid = p.Id

                                 }).FirstOrDefaultAsync();
            return details;
            // return null;
        }
        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPaidCertificate()
        {

            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.TrademarkApplicationHistory
                                 on p.Id equals f.ApplicationID


                                 where p.ApplicationStatus == "Paid" && p.DataStatus == "Certificate" && f.ToDataStatus == "Certificate" && f.ToStatus == "Paid" && p.NextRenewalDate ==null

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
                                     attach_doc = f.UploadsPath1,
                                     certificatePaymentReference = p.CertificatePayReference ,
                                     pwalletid = p.Id

                                 }).ToListAsync();
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

                                


                                 where   p.DataStatus == "Certificate" && p.ApplicationStatus == "Paid" &&  p.NextRenewalDate != null

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
                                     
                                     certificatePaymentReference = p.CertificatePayReference,
                                     NextrenewalDate =p.NextRenewalDate.ToString() ,
                                     pwalletid = p.Id

                                 }).ToListAsync();
            return details;
            // return null;
        }
    }
}
