using dotnetcqstemplate.Domain.Queries;
using Microsoft.Extensions.Configuration;

namespace dotnetcqstemplate;

public static class ConfigurationRegistrations
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services) =>
        services
            .AddConfig<GetLocalDateTimeQuery.Configuration>();


    private static IServiceCollection AddConfig<T>(this IServiceCollection services) where T : class => services.AddSingleton(provider => provider.GetService<IConfiguration>().Get<T>());
}