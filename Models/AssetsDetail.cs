using System;
using System.Collections.Generic;

#nullable disable

namespace AssetsManager.Models
{
    public partial class AssetsDetail
    {
        public AssetsDetail()
        {
            AssetsAssignedDetails = new HashSet<AssetsAssignedDetail>();
        }

        public int AssetsId { get; set; }
        public string AsstsName { get; set; }
        public string AssetsSerialNo { get; set; }
        public string AssetsCompanyName { get; set; }
        public string AssetModel { get; set; }
        public bool AssetAsigned { get; set; }

        public virtual ICollection<AssetsAssignedDetail> AssetsAssignedDetails { get; set; }
    }
}
