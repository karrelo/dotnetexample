namespace dotnetcqstemplate.Domain.Core;

public class ProcessFactory : IProcessFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ProcessFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
    public TProcess CreateProcess<TProcess>() => ActivatorUtilities.CreateInstance<TProcess>(_serviceProvider);
}