using System;
using System.Collections.Generic;

#nullable disable

namespace AssetsManager.Models
{
    public partial class AssetsAssignedDetail
    {
        public int AssignedId { get; set; }
        public int AssetsId { get; set; }
        public string AsstsName { get; set; }
        public string AssetsSerialNo { get; set; }
        public string AssetsCompanyName { get; set; }
        public string AssetModel { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DateOfAssigned { get; set; }

        public virtual AssetsDetail Assets { get; set; }
        public virtual EmployeeDetail Employee { get; set; }
    }
}
