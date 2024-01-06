using Samples.Common;

namespace Samples.Common.FakeServices
{
    internal class CurrentUser : ICurrentUser
    {
        public Guid UserId { get; }

        public Guid TenantId { get; }

        public CurrentUser()
        {
            // Resolve the current user from the current HTTP context

            UserId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            TenantId = Guid.Parse("00000000-0000-0000-0000-000000000002");
        }
    }
}
