using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AssetsManager.Models
{
    public partial class AssetsDetail
    {
        public int AssetsId { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string AsstsName { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string AssetsSerialNo { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string AssetsCompanyName { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string AssetModel { get; set; }

        [Required]
        public bool AssetAsigned { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime? AssetPurchaseDate { get; set; }


        [Required]
        public int? AssetsWarrenty { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string AssetImageUrl { get; set; }

        [Required]
        public decimal? AssetPrice { get; set; }
    }
}
