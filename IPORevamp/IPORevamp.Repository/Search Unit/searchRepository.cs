using IPORevamp.Data;
using IPORevamp.Repository.PTApplicationStatus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.Search;
using Microsoft.EntityFrameworkCore;

namespace IPORevamp.Repository.Search_Unit
{
    public  class searchRepository : IsearchRepository
    {
        private readonly IPOContext _contex;

        public searchRepository(IPOContext contex)
        {
            _contex = contex;
           

        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication()
        {
            var details = await  (from p in _contex.Application
                           join c in _contex.MarkInformation
                            on p.Id equals c.applicationid
                           join d in _contex.ApplicationUsers
                            on Convert.ToInt32(p.userid) equals d.Id

                           join e in _contex.TrademarkType
                           on  c.TradeMarkTypeID equals e.Id

                          

                                  where
                                   p.ApplicationStatus == "Fresh" && p.DataStatus == "Search"

                                  select new DataResult
                           {
                               FilingDate = p.DateCreated,
                               Filenumber = c.RegistrationNumber,
                               ApplicantName = d.FirstName + " " + d.LastName,
                               ProductTitle = c.ProductTitle,
                               Applicationclass = c.NiceClass,
                               status = p.ApplicationStatus,
                               Transactionid = p.TransactionID,
                               trademarktype = e.Description,
                               classdescription = c.NiceClassDescription,
                               phonenumber = d.MobileNumber,
                               email = d.UserName,
                               logo_pic = c.LogoPicture,
                               auth_doc = c.ApprovalDocument,
                               sup_doc1 = c.SupportDocument1,
                               sup_doc2 = c.SupportDocument2,
                               pwalletid = p.Id
                           }).ToListAsync();
            return details;
           // return null;
        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetKivApplication()
        {
            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.TrademarkApplicationHistory
                                  on p.Id equals f.ApplicationID


                                 where p.ApplicationStatus == "Kiv" && p.DataStatus == "Search" && f.ToStatus == "Kiv"

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = d.FirstName + " " + d.LastName,
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,
                                     pwalletid = p.Id ,
                                     comment = f.trademarkcomment ,
                                     commentby = d.FirstName + " " + d.LastName
                                 }).ToListAsync();
            return details;
            // return null;
        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetTreatedApplication()
        {
            var details = await (from p in _contex.Application
                                 join c in _contex.MarkInformation
                                  on p.Id equals c.applicationid
                                 join d in _contex.ApplicationUsers
                                  on Convert.ToInt32(p.userid) equals d.Id

                                 join e in _contex.TrademarkType
                                 on c.TradeMarkTypeID equals e.Id

                                 join f in _contex.TrademarkApplicationHistory
                                  on p.Id equals f.ApplicationID


                                 where f.FromDataStatus == "Search" && f.ToDataStatus == "Examiner" 

                                 select new DataResult
                                 {
                                     FilingDate = p.DateCreated,
                                     Filenumber = c.RegistrationNumber,
                                     ApplicantName = d.FirstName + " " + d.LastName,
                                     ProductTitle = c.ProductTitle,
                                     Applicationclass = c.NiceClass,
                                     status = p.ApplicationStatus,
                                     Transactionid = p.TransactionID,
                                     trademarktype = e.Description,
                                     classdescription = c.NiceClassDescription,
                                     phonenumber = d.MobileNumber,
                                     email = d.UserName,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,
                                     pwalletid = p.Id,
                                     comment = f.trademarkcomment,
                                     commentby = d.FirstName + " " + d.LastName
                                 }).ToListAsync();
            return details;
            // return null;
        }
        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.TrademarkType.TrademarkType>> GetTradeMarkType()
        {

            var details =  await _contex.TrademarkType.ToListAsync();

          
            return details;
            // return null;
        }






    }
}
