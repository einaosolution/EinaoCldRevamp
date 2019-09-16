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
using IPORevamp.Repository.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.DesignPublication
{
    class DesignPublicationApplication : IDesignPublicationApplication
    {

        private readonly IPOContext _contex;
        private readonly Data.Entity.Interface.IEmailSender _emailsender;
        private readonly Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;

        public DesignPublicationApplication(IPOContext contex, IConfiguration configuration, IEmailTemplateRepository EmailTemplateRepository, IEmailSender emailsender, IFileHandler fileUploadRespository)
        {

            _contex = contex;
            _emailsender = emailsender;
            _EmailTemplateRepository = EmailTemplateRepository;
            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;

        }

        public async void UpdateApplicationBatch( int BatchNo,string userole, string userid)
        {

            var details = (from p in _contex.DesignApplication where p.ApplicationStatus == STATUS.Pending && p.DataStatus == DATASTATUS.Publication


                           select p).ToList();

            foreach(var detail in details)
            {
                detail.BatchNo = BatchNo;
                _contex.SaveChanges();

                SaveApplicationHistory(detail.Id, userole, null, STATUS.Fresh, DATASTATUS.Publication, DATASTATUS.Publication, STATUS.Pending, "", "", userid, "");

            }


        }


        public async void SaveApplicationHistory(int id, string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid, string filepath)
        {

            var vpwallet = (from c in _contex.DesignApplication where c.Id == id select c).FirstOrDefault();

            string transactionid = vpwallet.TransactionID;
            string prevappstatus = vpwallet.ApplicationStatus;
            string prevDatastatus = vpwallet.DataStatus;



            if (vpwallet != null)
            {

                vpwallet.ApplicationStatus = tostatus;
                vpwallet.DataStatus = toDatastatus;


                _contex.SaveChanges();
                // get User Information




            }



            // file upload





            _contex.Add(new DesignApplicationHistory
            {
                DesignApplicationID = id,
                DateCreated = DateTime.Now,
                TransactionID = transactionid,
                FromDataStatus = prevDatastatus,
                patentcomment = comment,
                description = description,

                ToDataStatus = toDatastatus,
                FromStatus = prevappstatus,
                ToStatus = tostatus,
                UploadsPath1 = filepath,
                userid = Convert.ToInt32(userid),
                Role = userrole
            });



            _contex.SaveChanges();






            // return null;
        }

        public async Task<List<DesignDataResult>> GetDesignFreshApplication()
        {



            var details = _contex.DesignDataResult
            .FromSql($"DesignFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Publication, STATUS.Pending })
           .ToList();



            return details;
        }

        public async System.Threading.Tasks.Task<List<DesignDataResult>> GetPublicationById(String id)
        {
            List<DesignDataResult> DataResult = new List<DesignDataResult>();
            var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;

            var details = _contex.DesignDataResult
          .FromSql($"GetDesignPublicationByBatchNumber    @p0, @p1 , @p2 ", parameters: new[] { DATASTATUS.Publication, STATUS.Fresh, id })
         .ToList();

            foreach (var detail in details)
            {
               

                detail.RepresentationOfDesign1 = detail.RepresentationOfDesign1 == null ? detail.RepresentationOfDesign1 : GetBaseImage(detail.RepresentationOfDesign1);
                DataResult.Add(detail);

            }


            return DataResult;

            // return null;
        }

        private byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }
        public String GetBaseImage(String property1)
        {
            StringBuilder _sb = new StringBuilder();
          var applicationurl =  _configuration["ApiPath"];
            string imageurl = applicationurl + property1;
            Byte[] _byte = this.GetImage(imageurl);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return _sb.ToString();

        }

        public async Task<List<Int32>> GetDesignApplicationBatch()
        {



            var details = (from p in _contex.DesignApplication where p.BatchNo !=null
                          


                           select Convert.ToInt32(p.BatchNo)).Distinct().ToList();



            return details;
        }

    }
}
