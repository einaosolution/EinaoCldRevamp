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
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;

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


        public async Task<AppCount> AllApplicationUserCount( string  userid)
        {
            AppCount ApplicationCount = new AppCount();
            var result = 0;
            try { 
           result = (from c in _contex.Application where c.userid == userid select c).Count();
            ApplicationCount.trademarkcount = result;

            }

            catch(Exception ee)
            {
                ApplicationCount.trademarkcount = 0;
            }
            try
            {
                result = (from c in _contex.PatentApplication where c.userid == userid select c).Count();
            ApplicationCount.patentcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.patentcount = 0;
            }

            try
            {
                result = (from c in _contex.DesignApplication where c.userid == userid select c).Count();
            ApplicationCount.designcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.designcount = 0;
            }

            return ApplicationCount;


        }

        public async Task<Int32> ApplicationUserCount(int  ApplicationId,int  userid)
        {

            var result = (from c in _contex.TrademarkApplicationHistory where c.ApplicationID == ApplicationId && c.userid == userid  select c).Count();

        
            return result;


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


        public async Task<String> updatePatentTransactionById(string transactionid, string paymentid)
        {

            // check for user information before processing the request
            int id = Convert.ToInt32(transactionid);

            var result = (from c in _contex.PatentApplication where c.Id == id select c).FirstOrDefault();



            if (result != null)
            {
                result.TransactionID = paymentid;
                result.ApplicationStatus = STATUS.Fresh;

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

        public async Task<PatentApplication> SavePatentApplication(PatentApplication application)
        {
            _contex.PatentApplication.Add(application);

            _contex.SaveChanges();

            return application;
        }

        public async Task<PatentAssignment> SavePatentAssignment(PatentAssignment patentAssignment)
        {
            _contex.PatentAssignment.Add(patentAssignment);
          
           
            _contex.SaveChanges();


            return patentAssignment;
        }

        public async Task<AddressOfService> SaveAddressOfService(AddressOfService addressOfService)
        {
            try
            {
                var result = (from c in _contex.AddressOfService where c.PatentApplicationID == addressOfService.PatentApplicationID select c).FirstOrDefault();

                _contex.AddressOfService.Remove(result);
                _contex.SaveChanges();

            }

            catch(Exception ee)
            {

            }
            try
            {
                _contex.AddressOfService.Add(addressOfService);


                _contex.SaveChanges();

            }catch(Exception ee)
            {
                var errmessage = ee.Message;
            }


            return addressOfService;
        }

        public async Task<PatentInventionView[]> SavePatentInvention(PatentInventionView[] PatentInventionView, int ApplicationId)
        {
            List<PatentInvention> PtInvention = new List<PatentInvention>();

            foreach(var patentInventionview  in PatentInventionView)
            {
                PatentInvention patentInvention = new PatentInvention();
                patentInvention.CountryId = patentInventionview.CountryId;
                patentInvention.InventorAddress = patentInventionview.InventorAddress;
                patentInvention.InventorEmail = patentInventionview.InventorEmail;
                patentInvention.InventorMobileNumber = patentInventionview.InventorMobileNumber;
                patentInvention.InventorName = patentInventionview.InventorName;
                patentInvention.PatentApplicationID = patentInventionview.PatentApplicationID;

                patentInvention.IsActive = true;
                patentInvention.IsDeleted = false;
                patentInvention.DateCreated = DateTime.Now;


                PtInvention.Add(patentInvention);




            }

            var result  = (from c in _contex.PatentInvention where c.PatentApplicationID == ApplicationId select c).ToList();

            if (result.Count() > 0)
            {
                _contex.PatentInvention.RemoveRange(result);
                _contex.SaveChanges();

            }

           
            _contex.PatentInvention.AddRangeAsync(PtInvention);


            _contex.SaveChanges();


            return PatentInventionView;
        }

        public async Task<PatentPriorityInformationView[]> SavePriorityInformation(PatentPriorityInformationView[] PatentPriorityInformationView, int ApplicationId)
        {
            List<PatentPriorityInformation> PatentPriority = new List<PatentPriorityInformation>();

            foreach (var patentPriorityInformation in PatentPriorityInformationView)
            {
                PatentPriorityInformation patentpriorityInformation = new PatentPriorityInformation();
                patentpriorityInformation.CountryId = Convert.ToInt32(patentPriorityInformation.CountryId);
                patentpriorityInformation.ApplicationNumber = patentPriorityInformation.ApplicationNumber;
                patentpriorityInformation.PatentApplicationID = patentPriorityInformation.PatentApplicationID;
                patentpriorityInformation.RegistrationDate = patentPriorityInformation.RegistrationDate;


                patentpriorityInformation.IsActive = true;
                patentpriorityInformation.IsDeleted = false;
                patentpriorityInformation.DateCreated = DateTime.Now;


                PatentPriority.Add(patentpriorityInformation);




            }

            var result = (from c in _contex.PatentPriorityInformation where c.PatentApplicationID == ApplicationId select c).ToList();

            if (result.Count() > 0)
            {
                _contex.PatentPriorityInformation.RemoveRange(result);
                _contex.SaveChanges();

            }
          
            _contex.PatentPriorityInformation.AddRangeAsync(PatentPriority);


            _contex.SaveChanges();


            return PatentPriorityInformationView;
        }



        public async Task<PatentInformation> GetPatentApplication(int id )
        {
            var patentInformation = (from c in _contex.PatentInformation where c.PatentApplicationID == id select c).FirstOrDefault();


            return patentInformation;
        }


        public async Task<PatentApplication> GetPatentApplicationById(int id)
        {
            var patentApplication = (from c in _contex.PatentApplication where c.Id == id select c).Include(s => s.PatentAssignment).Include("PatentAssignment.AssigneeNationality").Include("PatentAssignment.AssignorNationality").Include(s => s.PatentInformation).Include("PatentInformation.PatentType").Include(s => s.PatentInvention).Include("PatentInvention.Country").Include(s => s.PatentPriorityInformation).Include("PatentPriorityInformation.Country").Include(s => s.AddressOfService).Include("AddressOfService.State").FirstOrDefault();


            return patentApplication;
        }


        public async Task<PatentApplication> GetPatentApplicationByUserId(string  userid)
        {
            var patentApplication = (from c in _contex.PatentApplication where c.userid == userid && c.ApplicationStatus ==STATUS.Pending select c).Include(s => s.PatentAssignment).Include("PatentAssignment.AssigneeNationality").Include("PatentAssignment.AssignorNationality").Include(s => s.PatentInformation).Include("PatentInformation.PatentType").Include(s => s.PatentInvention).Include("PatentInvention.Country").Include(s => s.PatentPriorityInformation).Include("PatentPriorityInformation.Country").Include(s => s.AddressOfService).Include("AddressOfService.State").FirstOrDefault();


            return patentApplication;
        }


        public async Task<PatentAssignment> UpdatePatentAssignment(int id , PatentAssignment patentassignment)
        {
            var result = (from c in _contex.PatentAssignment where c.PatentApplicationID == id select c).FirstOrDefault();

            //  result = patentassignment;
            result.AssigneeAddress = patentassignment.AssigneeAddress;
            result.AssigneeName = patentassignment.AssigneeName;
            result.AssigneeNationalityId = patentassignment.AssigneeNationalityId;
            result.AssignorName = patentassignment.AssignorName;

            result.AssignorAddress = patentassignment.AssignorAddress;

            result.AssignorNationalityId = patentassignment.AssignorNationalityId;



            _contex.SaveChanges();
            return result;
        }

        public async Task<PatentInformation> SavePatentInformation(PatentInformation ptinfo)
        {
            string registrationnumber = "";
            
            _contex.PatentInformation.Add(ptinfo);
            _contex.SaveChanges();


            var retrieveApplication = (from c in _contex.PatentInformation where c.Id == ptinfo.Id select c).FirstOrDefault();

            if (retrieveApplication.PatentTypeID ==Convert.ToInt32( IPOPATENTTYPE.CONVENTIONAL))
            {
                registrationnumber =  "NG/PT/C/" + DateTime.Today.Date.ToString("yyyy") + "/" + retrieveApplication.Id;
            }

            else
            {
                registrationnumber = "NG/PT/NC/" + DateTime.Today.Date.ToString("yyyy") + "/" + retrieveApplication.Id;
            }

            retrieveApplication.RegistrationNumber = registrationnumber;

            _contex.SaveChanges();


            // var saveContent = await _markinforepository.InsertAsync(markinfo);
            // await _markinforepository.SaveChangesAsync();
            return retrieveApplication;


        }
        public async Task<PatentInformation> UpdatePatentInformation(PatentInformation patentinfo)
        {

            var retrieveApplication = (from c in _contex.PatentInformation where c.Id == patentinfo.Id select c).FirstOrDefault();
            retrieveApplication = patentinfo;
            _contex.SaveChanges();

            return retrieveApplication;
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
