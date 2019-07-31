using IPORevamp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using EmailEngine.Base.Entities;

namespace IPORevamp.Repository.Publication
{
   public  class PublicationJob : IPublicationJob
    {
        private readonly IPOContext _contex;
        public PublicationJob(IPOContext contex)
       
        {
            _contex = contex;
        }
       
        public   void PrintTime()
        {
            Console.WriteLine($"{DateTime.Now}");
        }


        public void CheckPublicationStatus()
        {
            var settings = (from c in _contex.Settings where c.SettingCode == IPOCONSTANT.PublicationMaxDay  select c).FirstOrDefault();

          
                //  settings.ItemValue
                var result = (from p in _contex.Application
                          join f in _contex.TrademarkApplicationHistory
                               on p.Id equals f.ApplicationID

                          where f.ToDataStatus == "Publication" && f.ToStatus == "Batch" && p.DataStatus == "Publication" && p.ApplicationStatus == "Batch"
                              select f).ToList();

            // System.Console.Write("result count = " + result.Count);
            foreach (var results in result)
            {

                if ((results.DateCreated.AddDays(Convert.ToInt32(settings.ItemValue))) < DateTime.Now)
                {


                    var vpwallet = (from c in _contex.Application where c.Id == results.ApplicationID select c).FirstOrDefault();


                    string prevappstatus = vpwallet.ApplicationStatus;
                    string prevDatastatus = vpwallet.DataStatus;

                    vpwallet.ApplicationStatus = "Fresh";
                    vpwallet.DataStatus = "Certificate";
                    var appid = results.ApplicationID;

                    _contex.SaveChanges();


                    _contex.AddAsync(new TrademarkApplicationHistory
                    {
                        ApplicationID = appid,
                        DateCreated = DateTime.Now,
                        TransactionID = "",
                        FromDataStatus = prevDatastatus,
                        trademarkcomment = "Auto Move To Certificate By Admin",
                        description = "",

                        ToDataStatus = "Certificate",
                        FromStatus = prevappstatus,
                        ToStatus = "Fresh",
                        UploadsPath1 = "",
                        userid = 39,
                        Role = "1"
                    });

                    //  if ((results.DateCreated.AddDays(Convert.ToInt32(PublicationDuration))) > DateTime.Now) {

                    _contex.SaveChanges();

                    //  }

                }

            }




            //   return "success";
            // return null;
        }



    }
}
