using EmailEngine.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailEngine.Repository.EmailRepository
{
    public interface IAuditTrailManager<TActivity> where TActivity: IPOActivityLog
    {
        Task<int?> AddAuditTrail(TActivity log);
        Task<List<TActivity>> GetAuditTrails();
        Task<List<TActivity>> FilterAuditTrail();
        TActivity GetAuditTrail(int id);   
    }
}
