using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.MarkInfo;
using IPORevamp.Data.Entity.Interface.Entities.Pwallet;
using IPORevamp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using EmailEngine.Base.Entities;

namespace IPORevamp.Repository.FileNewApplication
{
    class newApplication : InewApplication
    {
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> _trademarkhistoryrepository;
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> _Applicationrepository;
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> _markinforepository;
        private readonly IPOContext _contex;

        public newApplication(IRepository<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> trademarkhistoryrepository , IRepository<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> Applicationrepository, IRepository<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> markinforepository , IPOContext contex)
        {
            _trademarkhistoryrepository = trademarkhistoryrepository;
            _Applicationrepository = Applicationrepository;
            _markinforepository = markinforepository;
            _contex = contex;



        }
        public  async Task<TrademarkApplicationHistory>  SaveAppHistory(TrademarkApplicationHistory apphistory)
        {
            var saveContent = await  _trademarkhistoryrepository.InsertAsync(apphistory);
            await _trademarkhistoryrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async  Task<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> UpdateMarkInfo(int id)

        {
          
              var vmark_info = (from c in _contex.MarkInformation where c.Id ==id select c).FirstOrDefault();

            if (vmark_info.TradeMarkTypeID == 1)
            {
                vmark_info.RegistrationNumber = "NG/TM/O/" + DateTime.Today.Date.ToString("yyyy") + "/" + id;
            }

            else
            {
                vmark_info.RegistrationNumber = "F/TM/O/" + DateTime.Today.Date.ToString("yyyy") + "/" + id;
            }

          

            _contex.SaveChanges();
            return vmark_info;
         



        }

        public async Task<MarkInformation>  SaveMarkInfo(MarkInformation markinfo)
        {

            _contex.MarkInformation.Add(markinfo);
            _contex.SaveChanges();

            // var saveContent = await _markinforepository.InsertAsync(markinfo);
            // await _markinforepository.SaveChangesAsync();
            return markinfo;

           
        }



        public async Task<String> updateTransactionById(string transactionid , string paymentid)
        {

            // check for user information before processing the request
            int id = Convert.ToInt32(transactionid);

            var vpwallet = (from c in _contex.Application where c.Id == id select c).FirstOrDefault();



            if (vpwallet != null)
            {
                vpwallet.TransactionID = paymentid;
                vpwallet.ApplicationStatus = STATUS.Fresh;

                _contex.SaveChanges();

                // get User Information




            }

            // var saveContent = await _markinforepository.InsertAsync(markinfo);
            // await _markinforepository.SaveChangesAsync();
            return "success";


        }


        public async Task<MarkInformation> GetMarkInfo(int id)
        {

            MarkInformation markinfo = new MarkInformation();
            markinfo = await _markinforepository.GetAll().FirstOrDefaultAsync(x => x.applicationid == id);



            return markinfo;
        }

        public async Task<Application> SaveApplication(Application application)
        {
            var saveContent = await  _Applicationrepository.InsertAsync(application);
            await  _Applicationrepository.SaveChangesAsync();

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
