using EmailEngine.Base.Entities;
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.DesignInvention;
using IPORevamp.Data.Entity.Interface.Entities.DesignPriority;
using IPORevamp.Repository.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.DesignRegistra
{
    class DesignRegistraApplication : IDesignRegistraApplication
    {
        private readonly IPOContext _contex;
        private readonly Data.Entity.Interface.IEmailSender _emailsender;
        private readonly Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;

        public DesignRegistraApplication(IPOContext contex, IConfiguration configuration, IEmailTemplateRepository EmailTemplateRepository, IEmailSender emailsender, IFileHandler fileUploadRespository)
        {

            _contex = contex;
            _emailsender = emailsender;
            _EmailTemplateRepository = EmailTemplateRepository;
            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;

        }

        public async Task<List<DesignDataResult>> GetDesignFreshApplication()
        {



            var details = _contex.DesignDataResult
            .FromSql($"DesignFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Acceptance, STATUS.Fresh })
           .ToList();



            return details;
        }

    }
}
