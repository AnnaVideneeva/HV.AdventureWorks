using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HV.AdventureWorks.Data.Entities;

namespace HV.AdventureWorks.Data.EntityConfigurations
{
    public class DocumentEntityConfiguration : IEntityTypeConfiguration<DocumentEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentEntity> builder)
        {
            builder.HasKey(e => e.DocumentNode);
            builder.ToTable("Document");

            builder.Property(e => e.DocumentNode).ValueGeneratedOnAdd();
            builder.Property(e => e.DocumentNode).IsRequired();

            builder.Property(e => e.DocumentLevel).ValueGeneratedOnAdd();

            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Title).HasMaxLength(50);

            builder.Property(e => e.Owner).IsRequired();

            builder.Property(e => e.FolderFlag).IsRequired();

            builder.Property(e => e.FileName).IsRequired();
            builder.Property(e => e.FileName).HasMaxLength(400);

            builder.Property(e => e.FileExtension).IsRequired();
            builder.Property(e => e.FileExtension).HasMaxLength(8);

            builder.Property(e => e.Revision).IsRequired();
            builder.Property(e => e.Revision).HasMaxLength(5);

            builder.Property(e => e.ChangeNumber).IsRequired();

            builder.Property(e => e.Status).IsRequired();

            builder.Property(e => e.File).HasColumnName("Document");

            builder.Property(e => e.RowGuid).HasColumnName("rowguid");
            builder.Property(e => e.RowGuid).IsRequired();

            builder.Property(e => e.ModifiedDate).IsRequired();
        }
    }
}