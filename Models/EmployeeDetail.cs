using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [DataType(DataType.Text)]
        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string EmployeeAddress { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string EmployeeDepName { get; set; }

        [Required]
        public string EmployeeProfilePicUrl { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime? DateOfJoining { get; set; }

        [Required]
        public decimal? EmployeeSalary { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public long? EmployeeMobileNo { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmployeeEmailId { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<AssetsAssignedDetail> AssetsAssignedDetails { get; set; }
    }
}
