using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samples.Common;

namespace Samples.Entities
{
    public class AssetType : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation properties    

        public virtual ICollection<Asset> Assets { get; set; }

    }

    public class AssetTypeEntityTypeConfiguration : IEntityTypeConfiguration<AssetType>
    {
        public void Configure(EntityTypeBuilder<AssetType> builder)
        {
            builder.HasKey(assetType => assetType.Id);

            builder.Property(assetType => assetType.Id)
                .ValueGeneratedOnAdd();

            builder.Property(assetType => assetType.Name)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(assetType => assetType.IsDeleted)
                .HasDefaultValueSql("CONVERT(BIT,0)");
        }
    }
}
