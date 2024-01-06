using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Samples.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation properties

        public virtual ICollection<Asset> Assets { get; set; }
    }

    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(tenant => tenant.Id);

            builder.Property(tenant => tenant.Id)
                .ValueGeneratedOnAdd();

            builder.Property(tenant => tenant.Name)
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
