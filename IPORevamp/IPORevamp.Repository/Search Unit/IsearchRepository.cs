using IPORevamp.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Search_Unit
{
 public    interface IsearchRepository : IAutoDependencyRegister
    {
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetKivApplication();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetTreatedApplication();

        Task<List<IPORevamp.Data.Entity.Interface.Entities.TrademarkType.TrademarkType>> GetTradeMarkType();
        void SaveApplicationHistory(int id, string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid);
        void SaveApplicationHistoryMultiple( string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid, string Batch);
    }
}
