using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.MarkInfo;
using IPORevamp.Data.Entity.Interface.Entities.Pwallet;
using IPORevamp.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IPORevamp.Repository.FileNewApplication
{
    class newApplication : InewApplication
    {
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> _trademarkhistoryrepository;
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> _Applicationrepository;
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> _markinforepository;

        public newApplication(IRepository<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> trademarkhistoryrepository , IRepository<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> Applicationrepository, IRepository<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> markinforepository)
        {
            _trademarkhistoryrepository = trademarkhistoryrepository;
            _Applicationrepository = Applicationrepository;
            _markinforepository = markinforepository;


        }
        public  async Task<TrademarkApplicationHistory>  SaveAppHistory(TrademarkApplicationHistory apphistory)
        {
            var saveContent = await  _trademarkhistoryrepository.InsertAsync(apphistory);
            await _trademarkhistoryrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

       public async Task<MarkInformation>  SaveMarkInfo(MarkInformation markinfo)
        {
            var saveContent = await _markinforepository.InsertAsync(markinfo);
            await _markinforepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<MarkInformation> GetMarkInfo(int id)
        {

            MarkInformation markinfo = new MarkInformation();
            markinfo = await _markinforepository.GetAll().FirstOrDefaultAsync(x => x.applicationid == id);



            return markinfo;
        }

        public async Task<Application> SaveApplication(Application application)
        {
            var saveContent = await _Applicationrepository.InsertAsync(application);
            await _Applicationrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<Application> GetApplication(int id)
        {

            Application application  = new Application();
            application = await _Applicationrepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

          

            return application;
        }

        public async Task<Application> UpdateApplication(Application application)
        {
            var saveContent = await _Applicationrepository.UpdateAsync(application);
            await _Applicationrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<MarkInformation> UpdateMarkInfo(MarkInformation markinfo)
        {
            var saveContent = await _markinforepository.UpdateAsync(markinfo);
            await _markinforepository.SaveChangesAsync();

            return saveContent.Entity;
        }
    }
}
