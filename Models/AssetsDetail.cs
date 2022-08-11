using System;
using System.Collections.Generic;

#nullable disable

namespace AssetsManager.Models
{
    public partial class AssetsDetail
    {
        public int AssetsId { get; set; }
        public string AsstsName { get; set; }
        public string AssetsSerialNo { get; set; }
        public string AssetsCompanyName { get; set; }
        public string AssetModel { get; set; }
        public bool AssetAsigned { get; set; }
        public DateTime? AssetPurchaseDate { get; set; }
        public int? AssetsWarrenty { get; set; }
        public string AssetImageUrl { get; set; }
        public decimal? AssetPrice { get; set; }
    }
}
