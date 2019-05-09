using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities.Setting
{
   public class Setting : EntityBase
    {
        public string ItemName { get; set; }
        public string ItemValue { get; set; }

        // the code 
        public string SettingCode { get; set; }
    }
}
