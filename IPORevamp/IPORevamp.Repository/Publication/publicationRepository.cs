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
            List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult> DataResult = new List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>();
            var details = _contex.DataResult
           .FromSql($"GetPublicationFreshApplication    @p0, @p1 ", parameters: new[] { DATASTATUS.Publication, STATUS.Fresh })
          .ToList();

            foreach(var detail in details )
            {
                detail.BatCount =Convert.ToString(BatchCount);
                DataResult.Add(detail);
            }
           
            
            return DataResult;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRefuseApplicationByUserid(string userid)
        {
            //  var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;
            var details = _contex.DataResult
            .FromSql($"GetRefuseApplicationByUserid    @p0, @p1 ", parameters: new[] { STATUS.Refused, userid })
           .ToList();
           
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPublicationById(String id)
        {
            List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult> DataResult = new List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>();
            var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;

            var details = _contex.DataResult
          .FromSql($"GetPublicationByBatchNumber    @p0, @p1 , @p2 ", parameters: new[] { DATASTATUS.Publication,STATUS.Batch , id })
         .ToList();

            foreach (var detail in details)
            {
                detail.BatCount = Convert.ToString(BatchCount);

                detail.logo_pic = detail.logo_pic == null ? detail.logo_pic : GetBaseImage(detail.logo_pic);
                DataResult.Add(detail);

            }


              return DataResult;
           
            // return null;
        }


        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignPublication>> GetPublication()
        {
            List<IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignPublication> DataResult = new List<IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignPublication>();
            var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;

            var details = _contex.DesignPublication
          .FromSql($"GetPublication    @p0, @p1 ", parameters: new[] { DATASTATUS.Publication, STATUS.Batch })
         .ToList();

            foreach (var detail in details)
            {
                // detail.BatCount = Convert.ToString(BatchCount);
                var result = (from c in _contex.TrademarkApplicationHistory where c.FromDataStatus == DATASTATUS.Examiner && c.ToDataStatus == DATASTATUS.Publication && c.FromStatus == STATUS.Fresh && c.ToStatus == STATUS.Fresh select c).FirstOrDefault();
                detail.acceptance_date = result.DateCreated.ToString();

                //  detail.logo_pic = detail.logo_pic == null ? detail.logo_pic : GetBaseImage(detail.logo_pic);
                DataResult.Add(detail);

            }


            return DataResult;

            // return null;
        }
        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPublicationByRegistrationId(String id)
        {
            List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult> DataResult = new List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>();
            var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;

            var details = _contex.DataResult
        .FromSql($"GetPublicationByRegistrationId    @p0, @p1 ,@p2 ", parameters: new[] { DATASTATUS.Publication,STATUS.Batch, id })
       .ToList();

            foreach (var detail in details)
            {
                detail.BatCount = Convert.ToString(BatchCount);
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
            var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;

            foreach (var val in ss)
            {
               
                var intval = Convert.ToInt32(val);
                var App = (from p in _contex.Application where p.Id == intval select p).FirstOrDefault();
                App.Batchno = Convert.ToString(BatchCount);
                _contex.SaveChanges();

               

            }

            PublicationBatch bb = new PublicationBatch();
            bb.DateCreated = DateTime.Now;
            bb.BatchNo = BatchCount;
            bb.IsActive = true;
            bb.IsDeleted = false;
            bb.NumberOfApplication = ss.Length;
            await _contex.AddAsync(bb);
            _contex.SaveChanges();


            return "success";
            // return null;
        }

      

       


        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Batch.PublicationBatch>> SelectBatches()
        {
            var App = await (from p in _contex.PublicationBatch
                             join c in _contex.Application
                              on p.BatchNo equals Convert.ToInt32(c.Batchno)
                             where c.DataStatus ==DATASTATUS.Publication 

                             select p).Distinct().ToListAsync();



            return App;
            // return null;
        }
    }
}

