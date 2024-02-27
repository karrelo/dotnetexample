namespace dotnetcqstemplate.Domain.Core;

public class QueryProvider : IQueryProvider
{
    private readonly IServiceProvider _serviceProvider;

    public QueryProvider(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public TQuery Get<TQuery>() where TQuery : IQueryMarkerInterface => _serviceProvider.GetService<TQuery>();
}