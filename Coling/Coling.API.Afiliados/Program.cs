using Coling.API.Afiliados;
using Coling.API.Afiliados.interfaces;
using Coling.API.Afiliados.services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables()
           .Build();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<Contexto>(options => options.UseSqlServer(
                     configuration.GetConnectionString("conexionDB")));
        services.AddScoped<PersonaServices>();
        services.AddScoped<TelefonoServices>();
        services.AddScoped<TipoSocialService>();
        services.AddScoped<PersonaTipoSocialService>();
        services.AddScoped<AfiliadoServices>();
    })
    .Build();

host.Run();
