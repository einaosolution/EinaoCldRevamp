using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.TMApplicationStatus
{
	public  class TMApplicationStatusViewModel
	{
        public int Id { get; set; }
		public int RoleId { get; set; }
		public string StatusDescription { get; set; }
		public int CreatedBy { get; set; }
	}
}
