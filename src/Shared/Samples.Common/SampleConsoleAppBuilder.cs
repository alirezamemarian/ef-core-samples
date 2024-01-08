using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Samples.Common
{
    public sealed class SampleConsoleAppBuilder
    {
        public IServiceCollection Services { get; }

        internal SampleConsoleAppBuilder(string[] args)
        {
            Services = new ServiceCollection();
        }

        public SampleConsoleApp Build()
        {
            var serviceProvider = Services.BuildServiceProvider();

            return new SampleConsoleApp(serviceProvider);
        }
    }
}
