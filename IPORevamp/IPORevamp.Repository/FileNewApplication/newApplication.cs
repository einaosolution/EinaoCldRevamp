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


            try
            {
                result = (from c in _contex.Application where c.ApplicationStatus == STATUS.Pending && c.DataStatus == DATASTATUS.Migration select c).Count();
                ApplicationCount.trademarkmigrationcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.trademarkmigrationcount = 0;
            }

            try
            {
                result = (from c in _contex.PatentApplication where c.ApplicationStatus == STATUS.Pending && c.DataStatus == DATASTATUS.Migration select c).Count();
                ApplicationCount.patentmigrationcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.patentmigrationcount = 0;
            }


            try
            {
                result = (from c in _contex.DesignApplication where c.ApplicationStatus == STATUS.Pending && c.DataStatus == DATASTATUS.Migration select c).Count();
                ApplicationCount.designmigrationcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.designmigrationcount = 0;
            }

            return ApplicationCount;


        }

        public async Task<DashBoardDesignCount> AllSearchDesignCount()
        {
            DashBoardDesignCount ApplicationCount = new DashBoardDesignCount();
            var result = 0;
            try
            {
                result = (from c in _contex.DesignApplication where c.ApplicationStatus == STATUS.Fresh && c.DataStatus == DATASTATUS.Search select c).Count();
                ApplicationCount.newapplicationcount = result;




            }

            catch (Exception ee)
            {
                ApplicationCount.newapplicationcount = 0;
            }
            try
            {
                result = (from c in _contex.DesignApplicationHistory where c.FromDataStatus == DATASTATUS.Search && c.ToDataStatus == DATASTATUS.Examiner select c).Count();
                ApplicationCount.treatedApplicationcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.treatedApplicationcount = 0;
            }

            try
            {
                result = (from c in _contex.DesignApplication where c.ApplicationStatus == STATUS.ReconductSearch && c.DataStatus == DATASTATUS.ReconductSearch select c).Count();
                ApplicationCount.ResearchApplicationcount = result;



            }

            catch (Exception ee)
            {
                ApplicationCount.ResearchApplicationcount = 0;
            }


            try
            {
                result = (from c in _contex.DesignApplication where c.ApplicationStatus == STATUS.Fresh && c.DataStatus == DATASTATUS.Examiner select c).Count();
                ApplicationCount.DesignExaminerFreshcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.DesignExaminerFreshcount = 0;
            }

            try
            {
                result = (from c in _contex.DesignApplicationHistory where c.FromDataStatus == DATASTATUS.Examiner && c.ToDataStatus == DATASTATUS.Acceptance select c).Count();
                ApplicationCount.DesignExaminerTreatedcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.DesignExaminerTreatedcount = 0;
            }



            try
            {
                result = (from p in _contex.DesignApplication
                          join f in _contex.DesignApplicationHistory
                               on p.Id equals f.DesignApplicationID

                          where f.ToStatus == STATUS.Paid && p.DataStatus == DATASTATUS.Certificate && p.ApplicationStatus == STATUS.Paid
                          select f).Count(); ;
                ApplicationCount.DesignPaidCertificatecount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.DesignPaidCertificatecount = 0;
            }



            try
            {
                result = (from c in _contex.DesignApplication where c.ApplicationStatus == STATUS.Paid && c.DataStatus == DATASTATUS.Certificate && c.CertificatePayReference != null select c).Count();
                ApplicationCount.DesignIssuedCertificatecount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.DesignIssuedCertificatecount = 0;
            }


            try
            {
                result = (from p in _contex.DesignApplication
                          join f in _contex.DesignApplicationHistory
                               on p.Id equals f.DesignApplicationID

                          where f.ToDataStatus == DATASTATUS.Opposition && p.DataStatus == DATASTATUS.Opposition && p.ApplicationStatus == STATUS.Fresh
                          select f).Count(); ;
                ApplicationCount.DesignOppositionFreshCount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.DesignOppositionFreshCount = 0;
            }

            try
            {
                result = (from p in _contex.DesignApplication
                          join f in _contex.DesignApplicationHistory
                               on p.Id equals f.DesignApplicationID

                          where f.ToDataStatus == DATASTATUS.Opposition && f.ToStatus == STATUS.Judgement && p.DataStatus == DATASTATUS.Opposition && p.ApplicationStatus == STATUS.Judgement
                          select f).Count(); ;
                ApplicationCount.DesignOppositionJudgementCount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.DesignOppositionJudgementCount = 0;
            }


            try
            {
                result = (from p in _contex.DesignApplication
                          

                          where p.DataStatus == DATASTATUS.Publication && p.ApplicationStatus == STATUS.Pending 
                          select p).Count(); ;
                ApplicationCount.DesignPublicationFreshcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.DesignPublicationFreshcount = 0;
            }


            try
            {
                result = (from c in _contex.DesignApplicationHistory where c.FromDataStatus == DATASTATUS.Publication && c.ToDataStatus == DATASTATUS.Publication && c.FromStatus == STATUS.Pending && c.ToStatus == STATUS.Fresh select c).Count();
                ApplicationCount.DesignPublicationPublishcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.DesignExaminerTreatedcount = 0;
            }


            try
            {
                result = (from c in _contex.RecordalDesignRenewal where c.Status == STATUS.Paid select c).Count();
                ApplicationCount.DesignRenewedCertificatecount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.DesignRenewedCertificatecount = 0;
            }



            return ApplicationCount;


        }

        public async Task<DashBoardPatentCount> AllSearchPatentCount()
        {
            DashBoardPatentCount ApplicationCount = new DashBoardPatentCount();
            var result = 0;
            try
            {
                result = (from c in _contex.PatentApplication where c.ApplicationStatus == STATUS.Fresh && c.DataStatus == DATASTATUS.Search select c).Count();
                ApplicationCount.newapplicationcount = result;




            }

            catch (Exception ee)
            {
                ApplicationCount.newapplicationcount = 0;
            }
            try
            {
                result = (from c in _contex.PatentApplicationHistory where c.FromDataStatus == DATASTATUS.Search && c.ToDataStatus == DATASTATUS.Examiner select c).Count();
                ApplicationCount.treatedApplicationcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.treatedApplicationcount = 0;
            }

            try
            {
                result = (from c in _contex.PatentApplication where c.ApplicationStatus == STATUS.ReconductSearch && c.DataStatus == DATASTATUS.ReconductSearch select c).Count();
                ApplicationCount.ResearchApplicationcount = result;



            }

            catch (Exception ee)
            {
                ApplicationCount.ResearchApplicationcount = 0;
            }


            try
            {
                result = (from c in _contex.PatentApplication where c.ApplicationStatus == STATUS.Fresh && c.DataStatus == DATASTATUS.Examiner select c).Count();
                ApplicationCount.PatentExaminerFreshcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.PatentExaminerFreshcount = 0;
            }

            try
            {
                result = (from c in _contex.PatentApplicationHistory where c.FromDataStatus == DATASTATUS.Examiner && c.ToDataStatus == DATASTATUS.Acceptance select c).Count();
                ApplicationCount.PatentExaminerTreatedcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.PatentExaminerTreatedcount = 0;
            }



            try
            {
                result = (from p in _contex.PatentApplication
                          join f in _contex.PatentApplicationHistory
                               on p.Id equals f.PatentApplicationID

                          where f.ToStatus == STATUS.Paid && p.DataStatus == DATASTATUS.Certificate && p.ApplicationStatus == STATUS.Paid
                          select f).Count(); ;
                ApplicationCount.PatentPaidCertificatecount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.PatentPaidCertificatecount = 0;
            }



            try
            {
                result = (from c in _contex.PatentApplication where c.ApplicationStatus == STATUS.Paid && c.DataStatus == DATASTATUS.Certificate && c.CertificatePayReference != null select c).Count();
                ApplicationCount.PatentIssuedCertificatecount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.PatentIssuedCertificatecount = 0;
            }


            try
            {
                result = (from p in _contex.Application
                          join c in _contex.MarkInformation
                           on p.Id equals c.applicationid
                          join d in _contex.ApplicationUsers
                           on Convert.ToInt32(p.userid) equals d.Id

                          join e in _contex.TrademarkType
                          on c.TradeMarkTypeID equals e.Id

                          join f in _contex.TrademarkApplicationHistory
                          on p.Id equals f.ApplicationID


                          where p.ApplicationStatus == STATUS.Appeal && p.DataStatus == DATASTATUS.Examiner && f.ToStatus == STATUS.Appeal
                          select p).Count();
                ApplicationCount.TrademarkAppealcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.TrademarkAppealcount = 0;
            }

            try
            {
                result = (from c in _contex.PatentApplication where c.ApplicationStatus == STATUS.Appeal && c.DataStatus == DATASTATUS.Examiner  select c).Count();
                ApplicationCount.PatentAppealcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.PatentAppealcount = 0;
            }


            try
            {
                result = (from c in _contex.DesignApplication where c.ApplicationStatus == STATUS.Delegate && c.DataStatus == DATASTATUS.Appeal select c).Count();
                ApplicationCount.DesignAppealcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.DesignAppealcount = 0;
            }

            try
            {
                result = (from c in _contex.ApplicationUsers where c.IsDeleted != true  select c).Count();
                ApplicationCount.Usercount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.Usercount = 0;
            }




            return ApplicationCount;


        }

        public async Task<DashBoardCount> AllSearchApplicationCount()
        {
            DashBoardCount ApplicationCount = new DashBoardCount();
            var result = 0;
            try
            {
                result = (from c in _contex.Application where c.ApplicationStatus == STATUS.Fresh && c.DataStatus == DATASTATUS.Search  select c).Count();
                ApplicationCount.newapplicationcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.newapplicationcount = 0;
            }
            try
            {
                result = (from c in _contex.TrademarkApplicationHistory where c.FromDataStatus == DATASTATUS.Search && c.ToDataStatus == DATASTATUS.Examiner  select c).Count();
                ApplicationCount.treatedApplicationcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.treatedApplicationcount = 0;
            }

            try
            {
                result = (from c in _contex.Application where c.ApplicationStatus == STATUS.ReconductSearch && c.DataStatus == DATASTATUS.ReconductSearch select c).Count();
                ApplicationCount.ResearchApplicationcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.ResearchApplicationcount = 0;
            }


            try
            {
                result = (from c in _contex.Application where c.ApplicationStatus == STATUS.Fresh && c.DataStatus == DATASTATUS.Examiner select c).Count();
                ApplicationCount.TrademarkExaminerFreshcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.TrademarkExaminerFreshcount = 0;
            }

            try
            {
                result = (from c in _contex.TrademarkApplicationHistory where c.FromDataStatus == DATASTATUS.Examiner && c.ToDataStatus == DATASTATUS.Publication select c).Count();
                ApplicationCount.TrademarkExaminerTreatedcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.TrademarkExaminerTreatedcount = 0;
            }


            try
            {
                result = (from p in _contex.Application
                          join f in _contex.TrademarkApplicationHistory
                               on p.Id equals f.ApplicationID

                          where f.ToDataStatus == DATASTATUS.Publication &&  p.DataStatus == DATASTATUS.Publication && p.ApplicationStatus == STATUS.Fresh
                          select f).Count(); ;
                ApplicationCount.TrademarkPublicationFreshcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.TrademarkPublicationFreshcount = 0;
            }


            try
            {
                result = (from c in _contex.TrademarkApplicationHistory where c.FromDataStatus == DATASTATUS.Publication && c.ToDataStatus == DATASTATUS.Publication  && c.FromStatus ==STATUS.Fresh  && c.ToStatus == STATUS.Batch  select c).Count();
                ApplicationCount.TrademarkPublicationPublishcount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.TrademarkExaminerTreatedcount = 0;
            }


            try
            {
                result = (from p in _contex.Application
                          join f in _contex.TrademarkApplicationHistory
                               on p.Id equals f.ApplicationID

                          where f.ToStatus == STATUS.Paid && p.DataStatus == DATASTATUS.Certificate && p.ApplicationStatus == STATUS.Paid
                          select f).Count(); ;
                ApplicationCount.TrademarkPaidCertificatecount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.TrademarkPaidCertificatecount = 0;
            }



            try
            {
                result = (from c in _contex.Application where c.ApplicationStatus == STATUS.Paid && c.DataStatus == DATASTATUS.Certificate  && c.CertificatePayReference !=null select c).Count();
                ApplicationCount.TrademarkIssuedCertificatecount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.TrademarkIssuedCertificatecount = 0;
            }


            try
            {
                result = (from c in _contex.RecordalRenewal where c.Status == STATUS.Paid select c).Count();
                ApplicationCount.TrademarkRenewedCertificatecount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.TrademarkRenewedCertificatecount = 0;
            }


            try
            {
                result = (from p in _contex.Application
                          join f in _contex.TrademarkApplicationHistory
                               on p.Id equals f.ApplicationID

                          where f.ToDataStatus == DATASTATUS.Opposition && p.DataStatus == DATASTATUS.Opposition && p.ApplicationStatus == STATUS.Fresh
                          select f).Count(); ;
                ApplicationCount.TrademarkOppositionFreshCount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.TrademarkOppositionFreshCount = 0;
            }

            try
            {
                result = (from p in _contex.Application
                          join f in _contex.TrademarkApplicationHistory
                               on p.Id equals f.ApplicationID

                          where f.ToDataStatus == DATASTATUS.Opposition && f.ToStatus == STATUS.Judgement &&  p.DataStatus == DATASTATUS.Opposition && p.ApplicationStatus == STATUS.Judgement
                          select f).Count(); ;
                ApplicationCount.TrademarkOppositionJudgementCount = result;

            }

            catch (Exception ee)
            {
                ApplicationCount.TrademarkOppositionJudgementCount = 0;
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
            var patentApplication = (from c in _contex.PatentApplication where c.userid == userid && c.ApplicationStatus ==STATUS.Pending && c.DataStatus != DATASTATUS.Migration select c).Include(s => s.PatentAssignment).Include("PatentAssignment.AssigneeNationality").Include("PatentAssignment.AssignorNationality").Include(s => s.PatentInformation).Include("PatentInformation.PatentType").Include(s => s.PatentInvention).Include("PatentInvention.Country").Include(s => s.PatentPriorityInformation).Include("PatentPriorityInformation.Country").Include(s => s.AddressOfService).Include("AddressOfService.State").FirstOrDefault();


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


        public async Task<PatentInformation> SavePatentInformation2(PatentInformation ptinfo)
        {
            string registrationnumber = "";

            _contex.PatentInformation.Add(ptinfo);
            _contex.SaveChanges();


            var retrieveApplication = (from c in _contex.PatentInformation where c.Id == ptinfo.Id select c).FirstOrDefault();

           


            // var saveContent = await _markinforepository.InsertAsync(markinfo);
            // await _markinforepository.SaveChangesAsync();
            return retrieveApplication;


        }

        public async Task<PatentApplication> UpdatePatentApplication(PatentApplication patentinfo)
        {

            var retrieveApplication = (from c in _contex.PatentApplication where c.Id == patentinfo.Id select c).FirstOrDefault();
            retrieveApplication = patentinfo;
            _contex.SaveChanges();

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

        public async Task<Application> SaveApplication2(Application application)
        {
            _contex.Application.Add(application);
            _contex.SaveChanges();

          

            return application;
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

        public async Task<Application> UpdateApplication2(Application application)
        {

            var saveContent = (from c in _contex.Application where c.Id == application.Id select c).FirstOrDefault();
            saveContent.DateCreated = application.DateCreated;
            _contex.SaveChanges();
            return application;
        }

        public async Task<MarkInformation> UpdateMarkInfo(MarkInformation markinfo)
        {
            var saveContent = await _markinforepository.UpdateAsync(markinfo);
            await _markinforepository.SaveChangesAsync();

            return saveContent.Entity;
        }
    }
}
