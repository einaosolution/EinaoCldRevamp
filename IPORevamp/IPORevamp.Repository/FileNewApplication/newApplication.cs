using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.MarkInfo;
using IPORevamp.Data.Entity.Interface.Entities.Pwallet;
using IPORevamp.Repository.Interface;

namespace IPORevamp.Repository.FileNewApplication
{
    class newApplication : InewApplication
    {
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> _trademarkhistoryrepository;
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Pwallet> _pwalletrepository;
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.Mark_Info> _markinforepository;

        public newApplication(IRepository<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> trademarkhistoryrepository , IRepository<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Pwallet> pwalletrepository , IRepository<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.Mark_Info> markinforepository)
        {
            _trademarkhistoryrepository = trademarkhistoryrepository;
            _pwalletrepository = pwalletrepository;
            _markinforepository = markinforepository;


        }
        public  async Task<TrademarkApplicationHistory>  SaveAppHistory(TrademarkApplicationHistory apphistory)
        {
            var saveContent = await  _trademarkhistoryrepository.InsertAsync(apphistory);
            await _trademarkhistoryrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

       public async Task<Mark_Info>  SaveMarkInfo(Mark_Info markinfo)
        {
            var saveContent = await _markinforepository.InsertAsync(markinfo);
            await _markinforepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<Pwallet> Savepwallet(Pwallet pwallet)
        {
            var saveContent = await _pwalletrepository.InsertAsync(pwallet);
            await _pwalletrepository.SaveChangesAsync();

            return saveContent.Entity;
        }
    }
}
