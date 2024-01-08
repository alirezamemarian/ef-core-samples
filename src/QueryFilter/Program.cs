using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using QueryFilter.Abstractions;
using Samples.Common;

namespace QueryFilter
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Create a builder for the sample console app and configure it

            var builder = SampleConsoleApp.CreateBuilder(args);

            builder.Services
                .AddDbContext<SampleContext>(options => options.UseSqlServer("Server=localhost"))
                .AddScoped<ICurrentUser>(sp =>
                {
                    var mock = new Mock<ICurrentUser>();
                    mock.Setup(x => x.UserId).Returns(Guid.Parse("00000000-0000-0000-0000-000000000001"));
                    mock.Setup(x => x.TenantId).Returns(Guid.Parse("00000000-0000-0000-0000-000000000002"));

                    return mock.Object;
                });

            var app = builder.Build();

            // Start the application and wait for it to exit, running the demo

            app.WaitForExit(RunDemo);
        }

        private static void RunDemo(IApp app)
        {
            using var scope = app.ServiceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<SampleContext>();

            using (var _ = app.Logger.BeginScope("Tenants Query"))
            {
                app.Logger.LogInformation($"{Environment.NewLine}{context.Tenants.ToQueryString()}{Environment.NewLine}");
            }

            using (var _ = app.Logger.BeginScope("AssetTypes Query"))
            {
                app.Logger.LogInformation($"{Environment.NewLine}{context.AssetTypes.ToQueryString()}{Environment.NewLine}");
            }

            using (var _ = app.Logger.BeginScope("Assets Query"))
            {
                app.Logger.LogInformation($"{Environment.NewLine}{context.Assets.ToQueryString()}{Environment.NewLine}");
            }

            using (var _ = app.Logger.BeginScope("Assets Query (with Include)"))
            {
                app.Logger.LogInformation($"{Environment.NewLine}{context.Assets.Include(x => x.AssetType).Include(x => x.Tenant).ToQueryString()}{Environment.NewLine}");
            }
        }
    }
}
