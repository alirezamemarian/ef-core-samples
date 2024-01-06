using Microsoft.EntityFrameworkCore.Metadata;

namespace Samples.Common
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if the specified entity type is soft deletable.
        /// </summary>
        /// <param name="entityType">The entity type to check.</param>
        /// <returns>True if the entity type is soft deletable; otherwise, false.</returns>
        public static bool IsSoftDeleteType(this IMutableEntityType entityType)
            => entityType.ClrType.IsAssignableTo(typeof(ISoftDelete));

        /// <summary>
        /// Checks if the specified type is soft deletable.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is soft deletable; otherwise, false.</returns>
        public static bool IsSoftDeleteType(this Type type)
            => type.IsAssignableTo(typeof(ISoftDelete));

        /// <summary>
        /// Checks if the specified entity type is tenant-related.
        /// </summary>
        /// <param name="entityType">The entity type to check.</param>
        /// <returns>True if the entity type is tenant-related; otherwise, false.</returns>
        public static bool IsTenantRelatedType(this IMutableEntityType entityType)
            => entityType.ClrType.IsAssignableTo(typeof(ITenantRelated));

        /// <summary>
        /// Checks if the specified type is tenant-related.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is tenant-related; otherwise, false.</returns>
        public static bool IsTenantRelatedType(this Type type)
            => type.IsAssignableTo(typeof(ITenantRelated));
    }
}
