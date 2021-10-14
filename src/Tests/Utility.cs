using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class Utility
{
    public static IHost CreateHost()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                var startupAssemblyName = typeof(Utility).Assembly.GetName().Name;
                webBuilder.UseSetting(WebHostDefaults.ApplicationKey, startupAssemblyName);
            })
            .ConfigureServices(services =>
            {
                services.AddMvcCore()
                    .AddRazorViewEngine();

                services.AddRazorTemplateRendering();
            })
            .Build();
    }
}