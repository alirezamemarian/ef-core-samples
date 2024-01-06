using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samples.Common;

namespace Samples.Entities
{
    public class Asset : ISoftDelete, ITenantRelated
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int AssetTypeId { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation properties

        public virtual AssetType AssetType { get; set; }
        public virtual Tenant Tenant { get; set; }
    }

    public class AssetEntityTypeConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(asset => asset.Id);

            builder.Property(asset => asset.Id)
                .ValueGeneratedOnAdd();

            builder.Property(asset => asset.Name)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(asset => asset.SerialNumber)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(asset => asset.IsDeleted)
                .HasDefaultValueSql("CONVERT(BIT,0)");

            // Navigation properties

            builder.HasOne(asset => asset.AssetType)
                .WithMany(assetType => assetType.Assets)
                .HasForeignKey(asset => asset.AssetTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(asset => asset.Tenant)
                .WithMany(tenant => tenant.Assets)
                .HasForeignKey(asset => asset.TenantId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes

            var isDeletedColumnName = builder
                .Property(asset => asset.IsDeleted)
                .Metadata
                .GetColumnName();

            builder.HasIndex(asset => new { asset.SerialNumber, asset.IsDeleted })
                .IsUnique()
                .HasFilter($"[{isDeletedColumnName}] = CONVERT(BIT,0)");
        }
    }
}
