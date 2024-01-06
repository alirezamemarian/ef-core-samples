using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Samples.Common.FakeServices;

namespace Samples.Common.Builders
{
    public sealed class SampleConsoleAppBuilder
    {
        public IServiceCollection Services { get; }

        internal SampleConsoleAppBuilder(string[] args)
        {
            Services = new ServiceCollection()
                .AddScoped<ICurrentUser, CurrentUser>();
        }


        public SampleConsoleAppBuilder AddFakeDbContext<T>()
            where T : DbContext
        {
            Services.AddDbContext<T>(options => 
            { 
                options.UseSqlServer("Server=localhost;Database=FakeDb"); 
            });

            return this;
        }

        public SampleConsoleApp Build()
        {
            var serviceProvider = Services.BuildServiceProvider();

            return new SampleConsoleApp(serviceProvider);
        }
    }
}
