using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.MarkInfo;
using IPORevamp.Data.Entity.Interface.Entities.Pwallet;
using IPORevamp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using EmailEngine.Base.Entities;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using EmailEngine.Repository.FileUploadRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplicationHistory;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Repository.Email;
using IPORevamp.Data.Entities.Email;

namespace IPORevamp.Repository.PatentExaminer
{
  public   class PatentExaminerApplication : IPatentExaminerApplication
    {
        private readonly IPOContext _contex;
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;
        private readonly IEmailSender _emailsender;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;

        public PatentExaminerApplication(IPOContext contex, IFileHandler fileUploadRespository, IConfiguration configuration, IEmailTemplateRepository EmailTemplateRepository, IEmailSender emailsender)
        {

            _contex = contex;

            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;
            _emailsender = emailsender;
            _EmailTemplateRepository = EmailTemplateRepository;



        }


        public async Task<List<PatentDataResult>> GetPatentFreshApplication()
        {



            var details = _contex.PatentDataResult
            .FromSql($"PatentFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Examiner, STATUS.Fresh })
           .ToList();



            return details;
        }
    }
}
