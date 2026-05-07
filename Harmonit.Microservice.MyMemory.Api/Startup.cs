using Microsoft.AspNetCore.Localization;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Globalization;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Harmonit.Microservice.Base.Library.BaseController;
using Harmonit.Notification.Library.Dictionary;
using System.Reflection;
using Harmonit.Microservice.MyMemory.CrossCutting.Ioc;

namespace Harmonit.Microservice.MyMemory.Api;

public class Startup(IConfiguration configuration)
{
    private readonly string SolutionName = Assembly.GetExecutingAssembly().GetName().Name!.Replace(".Api", "");

    public void ConfigureServices(IServiceCollection services)
    {
        NotificationDictionary.Seed();

        services.ConfigureDependencyInjection(configuration, SolutionName);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        var supportedCultures = new[] { new CultureInfo("pt-BR") };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        bool IsDevelopment = env.IsDevelopment();
        app.UseDeveloperExceptionPage();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DocExpansion(DocExpansion.None);
            c.SwaggerEndpoint("/swagger/pt-br/swagger.json", $"{SolutionName}.Api PT-BR");
            c.SwaggerEndpoint("/swagger/en/swagger.json", $"{SolutionName}.Api EN");
            c.SwaggerEndpoint("/swagger/es/swagger.json", $"{SolutionName}.Api ES");
        });

        app.UseAuthentication();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseMiddleware<HttpContextRequestBodyMiddleware>();

        app.UseHealthChecks("/status", new HealthCheckOptions
        {
            Predicate = p => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseHealthChecksUI(options => {
            options.UIPath = "/status-dashboard";
            options.AddCustomStylesheet("wwwroot/style/harmonitStyle.css");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}