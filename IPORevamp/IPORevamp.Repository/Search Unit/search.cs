using IPORevamp.Data;
using IPORevamp.Repository.PTApplicationStatus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.Search;

namespace IPORevamp.Repository.Search_Unit
{
    public  class search : Isearch
    {
        private readonly IPOContext _contex;

        public search(IPOContext contex)
        {
            _contex = contex;
           

        }

        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication()
        {
            var details = (from p in _contex.Pwallet
                           join c in _contex.Mark_Info
                            on p.Id equals c.pwalletid
                           join d in _contex.ApplicationUsers
                            on Convert.ToInt32(p.userid) equals d.Id
                         
                           where p.application_status == "Fresh"

                           select new DataResult
                           {
                               FilingDate = p.DateCreated,
                               Filenumber =c.reg_number,
                               ApplicantName = d.FirstName + " " + d.LastName,
                               ProductTitle = c.product_title,
                               Applicationclass = c.nice_class ,
                               status = p.application_status,
                               Transactionid = p.transactionid,
                               trademarktype = c.tm_typeID ,
                               classdescription = c.logo_descriptionID ,
                               phonenumber = d.MobileNumber ,
                               email = d.UserName ,
                               logo_pic = c.logo_pic ,
                               auth_doc = c.auth_doc ,
                               sup_doc1 = c.sup_doc1,
                               sup_doc2 = c.sup_doc2,
                               pwalletid = p.Id
                           }).ToList();
            return details;
        }

       




    }
}
