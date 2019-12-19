using EmailEngine.Base.Entities;
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.DelegateJob;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.DesignInvention;
using IPORevamp.Data.Entity.Interface.Entities.DesignPriority;
using IPORevamp.Data.Entity.Interface.Entities.Search;
using IPORevamp.Repository.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.DesignCertificate
{
    class DesignCertificateApplication :IDesignCertificateApplication
    {

        private readonly IPOContext _contex;
        private readonly Data.Entity.Interface.IEmailSender _emailsender;
        private readonly Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;

        public DesignCertificateApplication(IPOContext contex, IConfiguration configuration, IEmailTemplateRepository EmailTemplateRepository, IEmailSender emailsender, IFileHandler fileUploadRespository)
        {

            _contex = contex;
            _emailsender = emailsender;
            _EmailTemplateRepository = EmailTemplateRepository;
            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;

        }

        public async Task<List<DesignDataResult>> GetDesignConfirmCertificate()
        {



            var details = _contex.DesignDataResult
            .FromSql($"DesignConfirmCertificateApplication   @p0, @p1", parameters: new[] { DATASTATUS.Certificate, STATUS.Confirm })
           .ToList();



            return details;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetApplicationByRegistrationId(String id)
        {

            var details = await (from p in _contex.DesignApplication
                                 join c in _contex.DesignInformation
                                  on p.Id equals c.DesignApplicationID
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.DesignType
                                 on c.DesignTypeID equals e.Id





                                 where c.RegistrationNumber == id 

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = d.FirstName + " " + d.LastName,
                                     ApplicantAddress = d.Street,
                                     ProductTitle = c.TitleOfDesign,
                                     Applicationclass = Convert.ToString(c.NationClassID),
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.DesignDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.PriorityDocument,
                                     auth_doc = c.LetterOfAuthorization,
                                     sup_doc1 = c.RepresentationOfDesign1,
                                     sup_doc2 = c.RepresentationOfDesign2,


                                     pwalletid = p.Id

                                 }).ToListAsync();
            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPatentApplicationByRegistrationId(String id)
        {

            var details = await (from p in _contex.PatentApplication
                                 join c in _contex.PatentInformation
                                  on p.Id equals c.PatentApplicationID
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.PatentType
                                 on c.PatentTypeID equals e.Id





                                 where c.RegistrationNumber == id

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = d.FirstName + " " + d.LastName,
                                     ProductTitle = c.TitleOfInvention,
                                     Applicationclass = "",
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = "",
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.PctDocument ,
                                     auth_doc = c.LetterOfAuthorization,
                                     sup_doc1 = c.DeedOfAssignment,
                                     sup_doc2 = c.CompleteSpecificationForm,


                                     pwalletid = p.Id

                                 }).ToListAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPatentApplicationById(int  id)
        {

            var details = await (from p in _contex.PatentApplication
                                 join c in _contex.PatentInformation
                                  on p.Id equals c.PatentApplicationID
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.PatentType
                                 on c.PatentTypeID equals e.Id





                                 where c.PatentApplicationID == id

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = d.FirstName + " " + d.LastName,
                                     ApplicantAddress = d.Street,
                                     ProductTitle = c.TitleOfInvention,
                                     Applicationclass = "",
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = "",
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = c.PctDocument,
                                     auth_doc = c.LetterOfAuthorization,
                                     sup_doc1 = c.DeedOfAssignment,
                                     sup_doc2 = c.CompleteSpecificationForm,


                                     pwalletid = p.Id

                                 }).ToListAsync();
            return details;
            // return null;
        }
        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetApplicationByAppId(String id)
        {

            var details = await (from p in _contex.DesignApplication
                                 join c in _contex.DesignInformation
                                  on p.Id equals c.DesignApplicationID
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.DesignType
                                 on c.DesignTypeID equals e.Id





                                 where p.Id == Convert.ToInt32( id)

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = d.FirstName + " " + d.LastName,
                                     ProductTitle = c.TitleOfDesign,
                                     Applicationclass = Convert.ToString(c.NationClassID),
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.DesignDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     userid = p.userid,
                                     logo_pic = "",
                                     auth_doc = "",
                                     sup_doc1 = "",
                                     sup_doc2 = "",


                                     pwalletid = p.Id

                                 }).ToListAsync();
            return details;
            // return null;
        }


        public async Task<List<DesignDataResult>> GetDesignFreshApplication()
        {



            var details = _contex.DesignDataResult
            .FromSql($"DesignFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Certificate, STATUS.Paid })
           .ToList();



            return details;
        }
    }
}
