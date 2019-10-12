using IPORevamp.Data;
using IPORevamp.Repository.PTApplicationStatus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.Search;
using Microsoft.EntityFrameworkCore;
using EmailEngine.Repository.FileUploadRepository;
using Microsoft.AspNetCore.Http;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using Microsoft.Extensions.Configuration;
using IPORevamp.Data.Entities.AuditTrail;
using EmailEngine.Base.Entities;

namespace IPORevamp.Repository.Search_Unit
{
    public  class searchRepository : IsearchRepository
    {
        private readonly IPOContext _contex;
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;
       


        public searchRepository(IPOContext contex, IFileHandler fileUploadRespository , IConfiguration configuration)
        {
            _contex = contex;
            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;


        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication()
        {
            var details = _contex.DataResult
                 .FromSql($"GetFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Search , STATUS.Fresh  })
                .ToList();

            return details;
           // return null;
        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetKivApplication()
        {

            var details = _contex.DataResult
                .FromSql($"GetKivApplication   @p0, @p1", parameters: new[] { DATASTATUS.Search, STATUS.Kiv  })
               .ToList();

   
            return details;
            // return null;
        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetTreatedApplication()
        {

            var details = _contex.DataResult
               .FromSql($"GetTreatedApplication   @p0, @p1", parameters: new[] { DATASTATUS.Search , DATASTATUS.Examiner  })
              .ToList();

            return details;
            // return null;
        }
        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.TrademarkType.TrademarkType>> GetTradeMarkType()
        {

            var details =  await _contex.TrademarkType.ToListAsync();

          
            return details;
            // return null;
        }


        public async void  SaveApplicationHistory(int id ,string userrole , HttpRequest request ,string tostatus ,string toDatastatus ,string fromDatastatus ,string fromstatus ,string comment ,string description ,string userid,string uploadpath)
        {

            var vpwallet = (from c in _contex.Application where c.Id == id select c).FirstOrDefault();

            string transactionid = vpwallet.TransactionID;
            string prevappstatus = vpwallet.ApplicationStatus;
            string prevDatastatus = vpwallet.DataStatus;



            if (vpwallet != null)
            {

                vpwallet.ApplicationStatus = tostatus;
                vpwallet.DataStatus = toDatastatus;



                // get User Information




            }



            // file upload
          //  string msg = "";


          


            await _contex.AddAsync(new TrademarkApplicationHistory
            {
                ApplicationID = id,
                DateCreated = DateTime.Now,
                TransactionID = transactionid,
                FromDataStatus = prevDatastatus,
                trademarkcomment = comment,
                description = description,

                ToDataStatus = toDatastatus,
                FromStatus = prevappstatus,
                ToStatus = tostatus,
                UploadsPath1 = uploadpath,
                userid = Convert.ToInt32(userid),
                Role = userrole
            });



            _contex.SaveChanges();



            // return null;
        }


        public async void SaveApplicationHistoryMultiple( string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid,string Batch)
        {

            var vpwallet = (from c in _contex.Application where c.Batchno == Batch select c).ToList();

            foreach (var Application in vpwallet)
            {

                string transactionid = Application.TransactionID;
                string prevappstatus = Application.ApplicationStatus;
                string prevDatastatus = Application.DataStatus;



                if (Application != null)
                {

                    Application.ApplicationStatus = tostatus;
                    Application.DataStatus = toDatastatus;



                    // get User Information




                }



                // file upload
                string msg = "";



                await _contex.AddAsync(new TrademarkApplicationHistory
                {
                    ApplicationID = Application.Id,
                    DateCreated = DateTime.Now,
                    TransactionID = transactionid,
                    FromDataStatus = prevDatastatus,
                    trademarkcomment = comment,
                    description = description,

                    ToDataStatus = toDatastatus,
                    FromStatus = prevappstatus,
                    ToStatus = tostatus,
                    UploadsPath1 = "",
                    userid = Convert.ToInt32(userid),
                    Role = userrole
                });


            }


            // return null;
        }






    }
}
