using IPORevamp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.Search;
using EmailEngine.Base.Entities;

namespace IPORevamp.Repository.Certificate
{
    class CertificateRepository : ICertificateRepository
    {

        private readonly IPOContext _contex;
        private static readonly object _syncRoot = new object();

        public CertificateRepository(IPOContext contex)
        {
            _contex = contex;


        }


        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Certificate.PayCertificate> GetCertificatePaymentById(int applicationid)
        {

            var details = await (from p in _contex.PayCertificate




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<String > UpdateApplication(int applicationid)
        {
            var details = await (from p in _contex.Application




                                 where p.Id == applicationid

                                 select p).FirstOrDefaultAsync();
            DateTime theDate = DateTime.Now;

            details.NextRenewalDate = theDate.AddYears(5);

            _contex.SaveChanges();

            return "success";
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplicationByUserid(string userid)
        {

            var details = _contex.DataResult
        .FromSql($"GetCertificateFreshApplication    @p0, @p1 , @p2  ", parameters: new[] { DATASTATUS.Certificate,  STATUS.Batch, userid })
       .ToList();

            return details;
            // return null;
        }


        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult> GetApplicationById(int id)
        {
            string val = Convert.ToString(id);
            var details = _contex.DataResult
             
       .FromSql($"GetApplicationById    @p0", parameters: new[] { val })
      .FirstOrDefault();

           
            return details;
            // return null;
        }
        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPaidCertificate()
        {
            var details = _contex.DataResult
       .FromSql($"GetPaidCertificate    @p0, @p1  ", parameters: new[] { DATASTATUS.Certificate, STATUS.Paid})
      .ToList();

      
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Certificate.PayCertificate> UpdateForm(string opponentName, string opponentAddress, int NoticeAppID)
        {

            var details = await (from p in _contex.PayCertificate




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();

            details.ApplicantName = opponentName;
            details.ApplicantAddress = opponentAddress;
           

            _contex.SaveChanges();



            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Certificate.PayCertificate> ProcessCertificatePayment( int NoticeAppID,string TransactionId)
        {

            var details = await (from p in _contex.PayCertificate




                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();



            var appid = details.ApplicationId.Split(',');
            details.PaymentReference = TransactionId;
            details.Status = STATUS.Paid;

            _contex.SaveChanges();

            foreach (var kk in appid)
            {
                var App = (from p in _contex.Application where p.Id == Convert.ToInt32(kk) select p).FirstOrDefault();

                App.CertificatePayReference = TransactionId;
                App.RtNumber = Convert.ToString(getMaxRtNo() + 1);
                _contex.SaveChanges();
            }




            return details;
            // return null;
        }

        public long getMaxRtNo()
        {
            long MaxRt;
            lock (_syncRoot)
            {
                return MaxRt = _contex.Application.ToList().Max(e => Convert.ToInt64(e.RtNumber));
                //Body function
            }


        }

        public async System.Threading.Tasks.Task<Int32> SaveForm(IPORevamp.Data.Entity.Interface.Entities.Certificate.PayCertificate PayCertificate)
        {

            _contex.PayCertificate.Add(PayCertificate);
            _contex.SaveChanges();



            return PayCertificate.Id;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetIssuedCertificate()
        {

            var details = _contex.DataResult
     .FromSql($"GetIssuedCertificate    @p0, @p1  ", parameters: new[] { DATASTATUS.Certificate, STATUS.Paid })
    .ToList();

          
            return details;
            // return null;
        }
    }
}
