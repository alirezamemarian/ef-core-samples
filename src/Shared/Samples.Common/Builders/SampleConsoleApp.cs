using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Samples.Common.Builders
{
    public sealed class SampleConsoleApp
    {
        public static SampleConsoleAppBuilder CreateBuilder(string[] args)
        {
            return new SampleConsoleAppBuilder(args);
        }

        public IServiceProvider ServiceProvider { get; }
        public ILogger Logger { get; }

        internal SampleConsoleApp(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            var loggerFactory = LoggerFactory.Create(builder => builder.AddSimpleConsole());
            Logger = loggerFactory.CreateLogger("SampleConsoleApp");
        }

        public void WaitForExit(Action<SampleConsoleApp> runDemo = null)
        {
            try
            {
                runDemo?.Invoke(this);
            }
            catch(Exception exception)
            {
                Logger.LogError(exception, "An error occurred while running the demo.");

                Thread.Sleep(500);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
