using Microsoft.Extensions.Logging;

namespace Samples.Common
{
    public interface IApp
    {
        IServiceProvider ServiceProvider { get; }
        ILogger Logger { get; }

        IApp WaitForExit(Action<IApp> runDemo = null);
    }
}
