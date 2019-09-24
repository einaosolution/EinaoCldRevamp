using IPORevamp.Data;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using EmailEngine.Base.Entities;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.DesignAssigment;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignInformation;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.DesignInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Data.Entity.Interface.Entities.DesignPriority;
using IPORevamp.Data.Entity.Interface.Entities.DesignCoApplicant;

namespace IPORevamp.Repository.DesignNewpplication
{
    public class DesignNewpplicationRepository : IDesignNewpplicationRepository
    {
        private readonly IPOContext _contex;

        public DesignNewpplicationRepository( IPOContext contex)
        {
            
            _contex = contex;



        }


        public async Task<DesignAssignment > UpdateDesignAssignment(int id, DesignAssignment designassignment)
        {
            var result = (from c in _contex.DesignAssignment where c.DesignApplicationID == id select c).FirstOrDefault();

            //  result = patentassignment;
            result.AssigneeAddress = designassignment.AssigneeAddress;
            result.AssigneeName = designassignment.AssigneeName;
            result.AssigneeNationalityId = designassignment.AssigneeNationalityId;
            result.AssignorName = designassignment.AssignorName;

            result.AssignorAddress = designassignment.AssignorAddress;

            result.AssignorNationalityId = designassignment.AssignorNationalityId;



            _contex.SaveChanges();
            return result;
        }
        public async Task<DesignInformation> GetDesignApplication(int id)
        {
            var designInformation = (from c in _contex.DesignInformation where c.DesignApplicationID == id select c).FirstOrDefault();


            return designInformation;
        }


        public async Task<DesignApplication> GetDesignApplicationById(int id)
        {
            var designApplication = (from c in _contex.DesignApplication where c.Id == id select c).Include(s => s.DesignAssignment).Include("DesignAssignment.AssigneeNationality2").Include("DesignAssignment.AssignorNationality2").Include(s => s.DesignInformation).Include("DesignInformation.DesignType").Include(s => s.DesignInvention).Include("DesignInvention.Country").Include(s => s.DesignPriority).Include("DesignPriority.Country").Include(s => s.DesignAddressOfService).Include("DesignAddressOfService.State").Include(s => s.DesignCoApplicant).FirstOrDefault();


            return designApplication;
        }


      

        public async Task<String> updateDesignTransactionById(string transactionid, string paymentid)
        {

            // check for user information before processing the request
            int id = Convert.ToInt32(transactionid);

            var result = (from c in _contex.DesignApplication where c.Id == id select c).FirstOrDefault();



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

        public async Task<DesignApplication> GetDesignApplicationByUserId(string userid)
        {
            var designApplication = (from c in _contex.DesignApplication where c.userid == userid && c.ApplicationStatus == STATUS.Pending select c).Include("DesignAssignment.AssigneeNationality2").Include("DesignAssignment.AssignorNationality2").Include(s => s.DesignInformation).Include("DesignInformation.DesignType").Include(s => s.DesignInformation).Include("DesignInformation.NationalClass").Include(s => s.DesignInvention).Include("DesignInvention.Country").Include(s => s.DesignPriority).Include("DesignPriority.Country").Include(s => s.DesignAddressOfService).Include("DesignAddressOfService.State").Include(s => s.DesignCoApplicant).FirstOrDefault();


            return designApplication;
        }

        public async Task<PatentPriorityInformationView[]> SavePriorityInformation(PatentPriorityInformationView[] PatentPriorityInformationView, int ApplicationId)
        {
            List<DesignPriority> PatentPriority = new List<DesignPriority>();

            foreach (var patentPriorityInformation in PatentPriorityInformationView)
            {
                DesignPriority designpriority = new DesignPriority();
                designpriority.CountryId = Convert.ToInt32(patentPriorityInformation.CountryId);
                designpriority.ApplicationNumber = patentPriorityInformation.ApplicationNumber;
                designpriority.DesignApplicationID = patentPriorityInformation.PatentApplicationID;
                designpriority.RegistrationDate = patentPriorityInformation.RegistrationDate;


                designpriority.IsActive = true;
                designpriority.IsDeleted = false;
                designpriority.DateCreated = DateTime.Now;


                PatentPriority.Add(designpriority);




            }

            var result = (from c in _contex.DesignPriority where c.DesignApplicationID == ApplicationId select c).ToList();

            if (result.Count() > 0)
            {
                _contex.DesignPriority.RemoveRange(result);
                _contex.SaveChanges();

            }

            _contex.DesignPriority.AddRangeAsync(PatentPriority);


            _contex.SaveChanges();


            return PatentPriorityInformationView;
        }

        public async Task<CoApplicantView[]> SaveCoApplicantInformation(CoApplicantView[] CoApplicantView, int ApplicationId)
        {
            List<DesignCoApplicant> DesignCoApplicantList = new List<DesignCoApplicant>();

            foreach (var coApplicantView in CoApplicantView)
            {
                DesignCoApplicant designcoaaplicant = new DesignCoApplicant();
                designcoaaplicant.Fullname = coApplicantView.Fullname;
                designcoaaplicant.email = coApplicantView.email;
                designcoaaplicant.DesignApplicationID = ApplicationId;
                designcoaaplicant.phonenumber = coApplicantView.phonenumber;


                designcoaaplicant.IsActive = true;
                designcoaaplicant.IsDeleted = false;
                designcoaaplicant.DateCreated = DateTime.Now;


                DesignCoApplicantList.Add(designcoaaplicant);




            }

            var result = (from c in _contex.DesignCoApplicant where c.DesignApplicationID == ApplicationId select c).ToList();

            if (result.Count() > 0)
            {
                _contex.DesignCoApplicant.RemoveRange(result);
                _contex.SaveChanges();

            }

            _contex.DesignCoApplicant.AddRangeAsync(DesignCoApplicantList);


            _contex.SaveChanges();


            return CoApplicantView;
        }
        public async Task<DesignAssignment> SaveDesignAssignment(DesignAssignment designAssignment)
        {
            _contex.DesignAssignment.Add(designAssignment);


            _contex.SaveChanges();


            return designAssignment;
        }

        public async Task<DesignInformation> SaveDesignInformation(DesignInformation ptinfo)
        {
            string registrationnumber = "";

            _contex.DesignInformation.Add(ptinfo);
            _contex.SaveChanges();


            var retrieveApplication = (from c in _contex.DesignInformation where c.Id == ptinfo.Id select c).FirstOrDefault();

            if (retrieveApplication.DesignTypeID == Convert.ToInt32(IPODESIGNTYPE.TEXTILE))
            {
                registrationnumber = "NG/DS/T/" + DateTime.Today.Date.ToString("yyyy") + "/" + retrieveApplication.Id;
            }

            else
            {
                registrationnumber = "NG/DS/NT/" + DateTime.Today.Date.ToString("yyyy") + "/" + retrieveApplication.Id;
            }

            retrieveApplication.RegistrationNumber = registrationnumber;

            _contex.SaveChanges();


            // var saveContent = await _markinforepository.InsertAsync(markinfo);
            // await _markinforepository.SaveChangesAsync();
            return retrieveApplication;


        }

        public async Task<DesignAddressOfService> SaveAddressOfService(DesignAddressOfService addressOfService)
        {
            try
            {
                var result = (from c in _contex.DesignAddressOfService where c.DesignApplicationID == addressOfService.DesignApplicationID select c).FirstOrDefault();

                _contex.DesignAddressOfService.Remove(result);
                _contex.SaveChanges();

            }

            catch (Exception ee)
            {

            }
            try
            {
                _contex.DesignAddressOfService.Add(addressOfService);


                _contex.SaveChanges();

            }
            catch (Exception ee)
            {
                var errmessage = ee.Message;
            }


            return addressOfService;
        }

        public async Task<PatentInventionView[]> SaveDesignInvention(PatentInventionView[] PatentInventionView, int ApplicationId)
        {
            List<DesignInvention> DsInvention = new List<DesignInvention>();

            foreach (var patentInventionview in PatentInventionView)
            {
                DesignInvention designInvention = new DesignInvention();
                designInvention.CountryId = patentInventionview.CountryId;
                designInvention.InventorAddress = patentInventionview.InventorAddress;
                designInvention.InventorEmail = patentInventionview.InventorEmail;
                designInvention.InventorMobileNumber = patentInventionview.InventorMobileNumber;
                designInvention.InventorName = patentInventionview.InventorName;
                designInvention.DesignApplicationID = patentInventionview.PatentApplicationID;

                designInvention.IsActive = true;
                designInvention.IsDeleted = false;
                designInvention.DateCreated = DateTime.Now;


                DsInvention.Add(designInvention);




            }

            var result = (from c in _contex.DesignInvention where c.DesignApplicationID == ApplicationId select c).ToList();

            if (result.Count() > 0)
            {
                _contex.DesignInvention.RemoveRange(result);
                _contex.SaveChanges();

            }


            _contex.DesignInvention.AddRangeAsync(DsInvention);


            _contex.SaveChanges();


            return PatentInventionView;
        }
        public async Task<DesignApplication> SaveDesignApplication(DesignApplication application)
        {
            _contex.DesignApplication.Add(application);

            _contex.SaveChanges();

            return application;
        }

        public async Task<DesignInformation> UpdatedesignInformation(DesignInformation patentinfo)
        {

            var retrieveApplication = (from c in _contex.DesignInformation where c.Id == patentinfo.Id select c).FirstOrDefault();
            retrieveApplication = patentinfo;
            _contex.SaveChanges();

            return retrieveApplication;
        }
    }
}
