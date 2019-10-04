using IPORevamp.Data;
using IPORevamp.Repository.Registra;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.Search;
using EmailEngine.Base.Entities;

namespace IPORevamp.Repository.Publication
{
    class registraRepository : IregistraRepository
    {
        private readonly IPOContext _contex;

        public registraRepository(IPOContext contex)
        {
            _contex = contex;


        }

        public int GetAppealCount()
        {
            var model = (from c in _contex.Application where c.ApplicationStatus == STATUS.Registra && c.DataStatus == DATASTATUS.Examiner select c).Count();
           

            return model;
        }


        public int GetReceiveAppealCount()
        {
            var model = (from c in _contex.Application
                         join p in _contex.TrademarkApplicationHistory on new { a = c.Id } equals new { a = p.ApplicationID }


                         where c.ApplicationStatus == STATUS.Registra && c.DataStatus == DATASTATUS.Examiner  && p.ToStatus== STATUS.Registra && p.FromStatus == STATUS.Appeal
                         select c).Count();


            return model;
        }
        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetUserAppeal()
        {

            var details = _contex.DataResult
              .FromSql($"GetUserAppeal    @p0, @p1 ", parameters: new[] { DATASTATUS.Examiner, STATUS.Registra })
             .ToList();

           
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetAppeal()
        {

            var details = _contex.DataResult
             .FromSql($"GetAppeal    @p0, @p1 , @p2 ", parameters: new[] { DATASTATUS.Examiner, STATUS.Registra , STATUS.Appeal })
            .ToList();

           
            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> TreatUserAppeal()
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


                                 where p.ApplicationStatus == STATUS.Appeal  && p.DataStatus == DATASTATUS.Examiner  && f.ToStatus == STATUS.Appeal

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
                                     userid = p.userid,
                                     logo_pic = c.LogoPicture,
                                     auth_doc = c.ApprovalDocument,
                                     sup_doc1 = c.SupportDocument1,
                                     sup_doc2 = c.SupportDocument2,
                                     attach_doc = f.UploadsPath1,
                                     pwalletid = p.Id

                                 }).ToListAsync();
            return details;
            // return null;
        }
    }
}
