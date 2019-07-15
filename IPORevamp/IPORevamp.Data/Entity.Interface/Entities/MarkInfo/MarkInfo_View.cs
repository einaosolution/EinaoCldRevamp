using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.MarkInfo
{
  public   class MarkInfo_View
    {
        public string reg_number { get; set; }

        public string logo_descriptionID { get; set; }

        public string nation_classID { get; set; }
        public string userid { get; set; }
        public int pwalletid { get; set; }

        public string tm_typeID { get; set; }

        public string product_title { get; set; }

        public string nice_class { get; set; }

        public string logo_pic { get; set; }

        public string nice_class_description { get; set; }

        public string auth_doc { get; set; }
        public string sup_doc1 { get; set; }
        public string sup_doc2 { get; set; }
    }
}
