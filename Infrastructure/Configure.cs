using System.Net.Http;

namespace dotnetcqstemplate.Infrastructure;

public static class Configure
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services) =>
        services
            .AddHttpClient()
            .AddSingleton<IHttpService, HttpService>();

    private static IServiceCollection AddHttpClient(this IServiceCollection services)
        => services
            .AddHttpClient(HttpService.HttpClientName)
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = false }).Services;
}