using dotnetcqstemplate.Domain.Core;
using System.Collections.Generic;
using System.Linq;

namespace dotnetcqstemplate.Domain;

public static class Configure
{
    private static readonly Type QueryMarkerType = typeof(IQueryMarkerInterface);
    private static readonly Type CommandMarkerType = typeof(ICommandMarkerInterface);

    public static IServiceCollection RegisterDomain(this IServiceCollection services) => services.AddProcesses()
            .AddSingleton<IProcessFactory, ProcessFactory>()
            .ReqisterCommands()
            .ReqisterQueries();

    private static IServiceCollection ReqisterQueries(this IServiceCollection services)
    {
        GetAllClassesImplementingInterface(QueryMarkerType).ForEach(query => services.AddTransient(query));
        services.AddSingleton<Core.IQueryProvider, QueryProvider>();
        return services;
    }

    private static IServiceCollection ReqisterCommands(this IServiceCollection services)
    {
        GetAllClassesImplementingInterface(CommandMarkerType).ForEach(command => services.AddTransient(command));
        services.AddSingleton<ICommandProvider, CommandProvider>();
        return services;
    }

    public static List<Type> GetAllClassesImplementingInterface(Type type) =>
        AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(x => x.GetTypes())
        .Where(t => t.GetInterfaces().Contains(type) && !t.IsAbstract).ToList();

    private static IServiceCollection AddProcesses(this IServiceCollection services)
    {
        return services;
    }

    // private static IServiceCollection completetProcesses(this IServiceCollection services) =>
    //     services
    //         .AddTransient<Process1>()
    //         .AddTransient<Proceess2>();

}



// private static IServiceCollection ReqisterQueries(this IServiceCollection services)
// {
//     AppDomain.CurrentDomain
//     .GetAssemblies().SelectMany(x => x.GetTypes())
//     .Where(type => type.GetInterfaces().Contains(QueryMarkerType) && !type.IsAbstract).ToList()
//     .ForEach(query => services.AddScoped(query));

//     return services.AddSingleton<Core.IQueryProvider, Core.QueryProvider>();
// }
