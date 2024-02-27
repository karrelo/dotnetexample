namespace dotnetcqstemplate.Domain.Core;

public class CommandProvider : ICommandProvider
{
    private readonly IServiceProvider _serviceProvider;

    public CommandProvider(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public TCommand Get<TCommand>() where TCommand : ICommandMarkerInterface => _serviceProvider.GetService<TCommand>();
}