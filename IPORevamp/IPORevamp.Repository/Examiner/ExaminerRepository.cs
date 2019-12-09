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


        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetUserKiv(string userid)
        {

            var details = _contex.DataResult
            .FromSql($"GetUserKivApplication   @p0, @p1,@p2", parameters: new[] { DATASTATUS.ApplicantKiv , STATUS.ApplicantKiv , userid })
           .ToList();
        
            return details;
            // return null;
        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetExaminerKiv()
        {
            var details = _contex.DataResult
             .FromSql($"GetExaminerKiv   @p0, @p1", parameters: new[] { DATASTATUS.ApplicantKiv , STATUS.ApplicantKiv })
            .ToList();

            return details;
            // return null;
        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetExaminerReconductSearch()
        {
            var details = _contex.DataResult
           .FromSql($"GetExaminerReconductSearch   @p0, @p1", parameters: new[] { DATASTATUS.ReconductSearch, STATUS.ReconductSearch })
          .ToList();

           
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

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetApplicationByUserid(string userid,string start_date ,string end_date)
        {
            var details = _contex.DataResult
           .FromSql($"GetApplicationByUserid   @p0,@p1,@p2", parameters: new[] { userid , start_date, end_date })
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
