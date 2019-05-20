using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DSApplicationStatus
{
	public  class DSApplicationStatusViewModel
	{
		public int RoleId { get; set; }
		public string StatusDescription { get; set; }
		public int CreatedBy { get; set; }
	}
}
