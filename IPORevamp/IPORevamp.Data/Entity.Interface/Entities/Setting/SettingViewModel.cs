using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.SetupViewModel
{
   public class SettingViewModel
    {

        public string ItemName { get; set; }
        public string ItemValue { get; set; }

        // the code 
        public string SettingCode { get; set; }
        public int CreatedBy { get; set; }      
        public int IsActive { get; set; }
        public int SettingId { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}


