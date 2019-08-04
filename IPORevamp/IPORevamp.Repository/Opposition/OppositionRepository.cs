using IPORevamp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.Search;
using EmailEngine.Base.Entities;

namespace IPORevamp.Repository.Opposition
{
    class OppositionRepository : IOppositionRepository
    {
        private readonly IPOContext _contex;

        public OppositionRepository(IPOContext contex)
        {
            _contex = contex;


        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> GetApplicationById(int applicationid)
        {

            var details = await (from p in _contex.Application




                                 where p.Id == applicationid 

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication()
        {
            // var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;
            var details = _contex.DataResult
                 .FromSql($"GetOppositionFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Opposition, STATUS.Fresh })
                .ToList();
           
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetNewJudgment()
        {
            var details = _contex.DataResult
                .FromSql($"GetOppositionJudgement   @p0, @p1", parameters: new[] { DATASTATUS.Opposition, STATUS.Judgement })
               .ToList();

           
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetOppositionByUserId(String id)
        {

            var details = _contex.DataResult
               .FromSql($"GetOppositionByUserid    @p0, @p1 , @p2", parameters: new[] { DATASTATUS.Opposition, STATUS.Applicant, id })
              .ToList();

            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetCounterOppositionByUserId(String id)
        {
            var details = _contex.DataResult
             .FromSql($"GetCounterOppositionByUserId    @p0, @p1 , @p2", parameters: new[] { DATASTATUS.Opposition, STATUS.Counter, id })
            .ToList();

           
            return details;
            // return null;
        }

        

            public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.NoticeOfOpposition> GetNoticeApplicationByTransactionid(int applicationid,string transactionid)
        {

            var details = await (from p in _contex.NoticeOfOpposition




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();

            details.PaymentReference = transactionid;
            details.Status = "Paid";
            _contex.SaveChanges();
            return details;
            // return null;
        }


        

         public async System.Threading.Tasks.Task<Int32> SaveForm(IPORevamp.Data.Entity.Interface.Entities.Opposition.CounterOpposition CounterOpposition)
        {

            _contex.CounterOpposition.Add(CounterOpposition);
            _contex.SaveChanges();

            return CounterOpposition.Id;



            
        }

        public async System.Threading.Tasks.Task<Int32> SaveForm(IPORevamp.Data.Entity.Interface.Entities.Opposition.NoticeOfOpposition NoticeOfOpposition)
        {

            _contex.NoticeOfOpposition.Add(NoticeOfOpposition);
            _contex.SaveChanges();


     
            return NoticeOfOpposition.Id;
            // return null;
        }


        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.NoticeOfOpposition> UpdateForm(string opponentName, string opponentAddress, string Comment, string filepath,int  NoticeAppID)
        {

            var details = await (from p in _contex.NoticeOfOpposition




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.opponentName = opponentName;
            details.opponentAddress = opponentAddress;
            details.Comment = Comment;
            details.Upload = filepath;

            _contex.SaveChanges();



            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.CounterOpposition> UpdateCounterForm(string opponentName, string opponentAddress, string Comment, string filepath, int NoticeAppID)
        {

            var details = await (from p in _contex.CounterOpposition




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.ApplicantName = opponentName;
            details.ApplicantAddress = opponentAddress;
            details.Comment = Comment;
            details.Upload = filepath;

            _contex.SaveChanges();



            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.NoticeOfOpposition> GetNoticeApplicationById(int applicationid)
        {

            var details = await (from p in _contex.NoticeOfOpposition




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();

           
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.CounterOpposition> GetCounterOppostionApplicationById(int applicationid)
        {

            var details = await (from p in _contex.CounterOpposition




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.CounterOpposition> GetCounterOppostionApplicationByTransactionid(int applicationid,string transactionid)
        {

            var details = await (from p in _contex.CounterOpposition




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();

            details.PaymentReference = transactionid;
            details.Status = "Paid";
            _contex.SaveChanges();
            return details;
            // return null;
        }
    }
}
