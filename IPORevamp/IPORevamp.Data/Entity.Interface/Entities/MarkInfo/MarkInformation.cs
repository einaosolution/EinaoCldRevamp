using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.MarkInfo
{
  public   class MarkInformation : EntityBase
    {
        [ForeignKey("Id")]
        public int applicationid { get; set; }

        [ForeignKey("Id")]
        public int TradeMarkTypeID { get; set; }

        public string RegistrationNumber { get; set; }

        public string logo_descriptionID  { get; set; }

        public string NationClassID  { get; set; }

       
        public string userid { get; set; }
        [Required]
        public string ProductTitle  { get; set; }

        public string NiceClass { get; set; }

        public string LogoPicture { get; set; }
        public string NiceClassDescription { get; set; }

        public string ApprovalDocument { get; set; }
        public string SupportDocument1 { get; set; }
        public string SupportDocument2 { get; set; }

       

        public IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application application { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.TrademarkType.TrademarkType trademarktype { get; set; }
    }
}
