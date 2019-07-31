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
using Microsoft.Extensions.Configuration;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using EmailEngine.Base.Entities;

namespace IPORevamp.Repository.Publication
{
  public   class publicationRepository : IpublicationRepository
    {

        private readonly IPOContext _contex;
        protected readonly IConfiguration _configuration;


        public publicationRepository(IPOContext contex, IConfiguration configuration)
        {
            _contex = contex;
            _configuration = configuration;


        }
        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication()
        {
            var BatchCount =  (from p in _contex.PublicationBatch select p).Count() + 1;
            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.TrademarkApplicationHistory
                                 on p.Id equals f.ApplicationID


                                 where p.ApplicationStatus ==STATUS.Fresh && p.DataStatus ==DATASTATUS.Publication  && f.ToDataStatus== DATASTATUS.Publication && p.Batchno==null

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
                                     pwalletid = p.Id ,
                                     BatCount= BatchCount.ToString()
                                 }).ToListAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRefuseApplicationByUserid(string userid)
        {
            var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;
            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.TrademarkApplicationHistory
                                 on p.Id equals f.ApplicationID


                                 where p.ApplicationStatus ==STATUS.Refused   && p.userid ==userid &&  f.ToStatus == STATUS.Refused

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
                                     pwalletid = p.Id,
                                     BatCount = BatchCount.ToString()
                                 }).ToListAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPublicationById(String id)
        {
            var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;
            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.TrademarkApplicationHistory
                                 on p.Id equals f.ApplicationID


                                 where  p.DataStatus ==DATASTATUS.Publication  && f.ToDataStatus == DATASTATUS.Publication && p.Batchno == id

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
                                     logo_pic = c.LogoPicture == null ?c.LogoPicture: GetBaseImage(c.LogoPicture) ,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,
                                     attach_doc = f.UploadsPath1,
                                     pwalletid = p.Id,
                                     BatCount = BatchCount.ToString()
                                 }).ToListAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPublicationByRegistrationId(String id)
        {
            var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;
            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.TrademarkApplicationHistory
                                 on p.Id equals f.ApplicationID


                                 where p.DataStatus == DATASTATUS.Publication  && f.ToDataStatus == DATASTATUS.Publication && c.RegistrationNumber == id

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
                                     logo_pic = c.LogoPicture == null ? c.LogoPicture : GetBaseImage(c.LogoPicture),
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,
                                     attach_doc = f.UploadsPath1,
                                     pwalletid = p.Id,
                                     BatCount = BatchCount.ToString()
                                 }).ToListAsync();
            return details;
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

        public String GetBaseImage( String property1)
        {
            StringBuilder _sb = new StringBuilder();
            string imageurl = "http://5.77.54.44/EinaoCldRevamp2/Upload/" + property1;
            Byte[] _byte = this.GetImage(imageurl);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return _sb.ToString();

        }


        public async System.Threading.Tasks.Task<String> UpdateBatch(String[] ss)
        {
            foreach (var val in ss)
            {
                var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;
                var intval = Convert.ToInt32(val);
                var App = (from p in _contex.Application where p.Id == intval select p).FirstOrDefault();
                App.Batchno = Convert.ToString(BatchCount);
                _contex.SaveChanges();

                PublicationBatch bb = new PublicationBatch();
                bb.DateCreated = DateTime.Now;
                bb.BatchNo = BatchCount;
                bb.IsActive = true;
                bb.IsDeleted = false;
                bb.NumberOfApplication = ss.Length;
                await _contex.AddAsync(bb);
                _contex.SaveChanges();

            }


            return "success";
            // return null;
        }

      

       


        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Batch.PublicationBatch>> SelectBatches()
        {
            var App = await (from p in _contex.PublicationBatch
                             join c in _contex.Application
                              on p.BatchNo equals Convert.ToInt32(c.Batchno)
                             where c.DataStatus ==DATASTATUS.Publication 

                             select p).ToListAsync();



            return App;
            // return null;
        }
    }
}

