using dotnetcqstemplate;
using dotnetcqstemplate.Domain;
using dotnetcqstemplate.Infrastructure;
using dotnetcqstemplate.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(Startup))]
namespace dotnetcqstemplate;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddLogging();
        builder.Services.RegisterServices();
        builder.Services.RegisterDomain();
        builder.Services.RegisterInfrastructure();
        builder.Services.AddConfigurations();
    }
}