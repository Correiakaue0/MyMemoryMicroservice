using Lamar.Microsoft.DependencyInjection;
using System.Reflection;

namespace Harmonit.Microservice.MyMemory.Api;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).UseLamar((context, registry) =>
            {
                registry.Scan(scanner =>
                {
                    var SolutionName = Assembly.GetExecutingAssembly().GetName().Name!.Replace(".Api", "");
                    scanner.Assembly($"{SolutionName}.Application");

                    scanner.WithDefaultConventions();
                });
            });
}