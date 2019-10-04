using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Role
{
  public  class UserRoleCount
    {
        public int SearchOfficerCount { get; set; }
        public int ExaminerOfficerCount { get; set; }
        public int PublicationOfficerCount { get; set; }
        public int CertificateOfficerCount { get; set; }
        public int AppealOfficerCount { get; set; }
    }
}
