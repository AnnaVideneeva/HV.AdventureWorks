using System;
using System.ComponentModel.DataAnnotations;

namespace HV.AdventureWorks.Services.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        public string ProductNumber { get; set; }

        [Required]
        public bool MakeFlag { get; set; }

        [Required]
        public bool FinishedGoodsFlag { get; set; }

        [StringLength(15)]
        public string Color { get; set; }

        [Required]
        public short SafetyStockLevel { get; set; }

        [Required]
        public short ReorderPoint { get; set; }

        [Required]
        public decimal StandardCost { get; set; }

        [Required]
        public decimal ListPrice { get; set; }

        [StringLength(5)]
        public string Size { get; set; }

        [Required]
        public decimal? Weight { get; set; }

        [Required]
        public int DaysToManufacture { get; set; }

        [StringLength(2)]
        public string ProductLine { get; set; }

        [StringLength(2)]
        public string Class { get; set; }

        [StringLength(2)]
        public string Style { get; set; }

        [Required]
        public DateTime SellStartDate { get; set; }

        [Required]
        public DateTime? SellEndDate { get; set; }

        [Required]
        public DateTime? DiscontinuedDate { get; set; }

        [Required]
        public Guid RowGuid { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        public int? ProductSubcategoryId { get; set; }

        [Required]
        public int? ProductModelId { get; set; }

        [StringLength(3)]
        public string SizeUnitMeasureCode { get; set; }

        [StringLength(3)]
        public string WeightUnitMeasureCode { get; set; }
    }
}
