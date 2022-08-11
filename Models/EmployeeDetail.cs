using System;
using System.Collections.Generic;

#nullable disable

namespace AssetsManager.Models
{
    public partial class EmployeeDetail
    {
        public EmployeeDetail()
        {
            AssetsAssignedDetails = new HashSet<AssetsAssignedDetail>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeeDepName { get; set; }
        public string EmployeeProfilePicUrl { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public decimal? EmployeeSalary { get; set; }
        public long? EmployeeMobileNo { get; set; }
        public string EmployeeEmailId { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<AssetsAssignedDetail> AssetsAssignedDetails { get; set; }
    }
}
