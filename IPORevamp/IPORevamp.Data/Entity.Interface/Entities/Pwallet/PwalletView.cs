using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Pwallet
{
  public   class PwalletView
    {
        public int Applicationtypeid { get; set; }

        public string transactionid { get; set; }

        public string userid { get; set; }

        public string application_status { get; set; }

        public string data_status { get; set; }

        public DateTime data_created { get; set; }


        public string logo_descriptionID { get; set; }

        public string nation_classID { get; set; }
      

        public string tm_typeID { get; set; }

        public string product_title { get; set; }

        public string nice_class { get; set; }

    }
}
