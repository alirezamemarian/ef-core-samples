using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samples.Common.Builders;

namespace QueryFilter
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            SampleConsoleApp
                .CreateBuilder(args)
                .AddFakeDbContext<SampleContext>()
                .Build()
                .WaitForExit(RunDemo);
        }

        private static void RunDemo(SampleConsoleApp app)
        {
            using var scope = app.ServiceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<SampleContext>();

            app.Logger.LogInformation($"Assets:{Environment.NewLine}{context.Assets.ToQueryString()}");
            app.Logger.LogInformation($"AssetTypes:{Environment.NewLine}{context.AssetTypes.ToQueryString()}");
        }
    }
}
