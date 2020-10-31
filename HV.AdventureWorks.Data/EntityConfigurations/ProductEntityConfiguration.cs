using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HV.AdventureWorks.Data.Entities;

namespace HV.AdventureWorks.Data.EntityConfigurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable("Product");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Id).HasColumnName("ProductID");
            builder.Property(e => e.Id).IsRequired();

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(50);

            builder.Property(e => e.ProductNumber).IsRequired();
            builder.Property(e => e.ProductNumber).HasMaxLength(25);

            builder.Property(e => e.MakeFlag).IsRequired();

            builder.Property(e => e.FinishedGoodsFlag).IsRequired();

            builder.Property(e => e.Color).HasMaxLength(15);

            builder.Property(e => e.SafetyStockLevel).IsRequired();

            builder.Property(e => e.ReorderPoint).IsRequired();

            builder.Property(e => e.StandardCost).IsRequired();

            builder.Property(e => e.ListPrice).IsRequired();

            builder.Property(e => e.Size).HasMaxLength(5);

            builder.Property(e => e.DaysToManufacture).IsRequired();

            builder.Property(e => e.ProductLine).HasMaxLength(2);

            builder.Property(e => e.Class).HasMaxLength(2);

            builder.Property(e => e.Style).HasMaxLength(2);

            builder.Property(e => e.SellStartDate).IsRequired();

            builder.Property(e => e.RowGuid).HasColumnName("rowguid");
            builder.Property(e => e.RowGuid).IsRequired();

            builder.Property(e => e.ModifiedDate).IsRequired();

            builder.Property(e => e.ProductSubcategoryId).HasColumnName("ProductSubcategoryID");

            builder.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

            builder.Property(e => e.SizeUnitMeasureCode).HasMaxLength(3);

            builder.Property(e => e.WeightUnitMeasureCode).HasMaxLength(3);
        }
    }
}
