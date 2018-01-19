using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace NetCoreSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration(x => x.AddUserSecrets())
                .UseStartup<Startup>()
                //.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning))
                .Build();
    }
}
