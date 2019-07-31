using IPORevamp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.Search;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EmailEngine.Base.Entities;

namespace IPORevamp.Repository.Examiner
{
    class ExaminerRepository : IExaminerRepository
    {
        private readonly IPOContext _contex;

        public ExaminerRepository(IPOContext contex)
        {
            _contex = contex;


        }


        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetUserKiv()
        {

            var details = _contex.DataResult
            .FromSql($"GetUserKivApplication   @p0, @p1", parameters: new[] { DATASTATUS.ApplicantKiv , STATUS.ApplicantKiv  })
           .ToList();
        
            return details;
            // return null;
        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetExaminerKiv()
        {
            var details = _contex.DataResult
             .FromSql($"GetExaminerKiv   @p0, @p1", parameters: new[] { DATASTATUS.Examiner , STATUS.Kiv })
            .ToList();

            return details;
            // return null;
        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetExaminerReconductSearch()
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


                                 where p.DataStatus == "Reconduct-Search" && p.ApplicationStatus == "Reconduct-Search" && f.ToStatus == "Reconduct-Search"

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
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,
                                     pwalletid = p.Id,
                                     comment = f.trademarkcomment,
                                     attach_doc = f.UploadsPath1,
                                     commentby = d.FirstName + " " + d.LastName
                                 }).ToListAsync();
            return details;
            // return null;
        }
        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication()
        {
            var details = _contex.DataResult
              .FromSql($"ExaminerFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Examiner, STATUS.Fresh  })
             .ToList();
       
            return details;
            // return null;
        }


        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetTreatedApplication()
        {

            var details = _contex.DataResult
            .FromSql($"GetTreatedApplication   @p0, @p1", parameters: new[] { DATASTATUS.Examiner, DATASTATUS.Publication  })
           .ToList();
         
            return details;
            // return null;
        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetApplicationByUserid(string userid)
        {
            var details = _contex.DataResult
           .FromSql($"GetApplicationByUserid   @p0", parameters: new[] { userid })
          .ToList();


           
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.PreviousComments>> GetPreviousComment(int id)
        {
            string applicationid = Convert.ToString(id);


            var details = _contex.PreviousComments
                .FromSql($"GetPreviousComment   @p0", parameters: new[] { applicationid })
               .ToList();

         
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> GetApplicationHistoryById(int   applicationid)
        {
            
            var details = await (from p in _contex.TrademarkApplicationHistory

                                


                                 where  p.ApplicationID == applicationid && p.ToStatus== STATUS.Refused

                                 select  p).FirstOrDefaultAsync();
            return details;
            // return null;
        }
    }
}
