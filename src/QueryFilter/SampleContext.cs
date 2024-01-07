using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using QueryFilter.Entities;
using Samples.Common;
using System.Linq.Expressions;

namespace QueryFilter
{
    public class SampleContext : DbContext
    {
        private Guid CurrentTenantId => this.GetService<ICurrentUser>().TenantId;

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<Asset> Assets { get; set; }

        public SampleContext(DbContextOptions<SampleContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SampleContext).Assembly);

            // Retrieve entities that implement ISoftDelete or ITenantRelated

            var entityClrTypes = modelBuilder.Model
                .GetEntityTypes()
                .Where(e =>  e.IsSoftDeleteType() || e.IsTenantRelatedType())
                .Select(e => e.ClrType);

            foreach (var entityClrType in entityClrTypes)
            {
                modelBuilder.Entity(entityClrType)
                    .HasQueryFilter(GetQueryFilter(entityClrType));
            }
        }

        /// <summary>
        /// Gets the query filter expression for soft delete and tenant-related entities.
        /// </summary>
        /// <param name="entityClrType">The type of the entity.</param>
        /// <returns>The query filter expression.</returns>
        private LambdaExpression? GetQueryFilter(Type entityClrType)
        {
            var parameter = Expression.Parameter(entityClrType, "e");
            var defaultExpression = Expression
                .MakeBinary(ExpressionType.Equal, Expression.Constant(1), Expression.Constant(1));

            // Apply soft delete filter

            if (entityClrType.IsSoftDeleteType())
            {
                var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
                var body = Expression.Equal(property, Expression.Constant(false));

                defaultExpression = Expression.AndAlso(defaultExpression, body);
            }

            // Apply tenant-related filter

            if (entityClrType.IsTenantRelatedType())
            {
                var property = Expression.Property(parameter, nameof(ITenantRelated.TenantId));
                var constant = Expression.PropertyOrField(Expression.Constant(this), nameof(CurrentTenantId));
                var body = Expression.Equal(property, constant);

                defaultExpression = Expression.AndAlso(defaultExpression, body);
            }

            return Expression.Lambda(defaultExpression, parameter);
        }
    }
}
