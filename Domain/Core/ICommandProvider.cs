namespace dotnetcqstemplate.Domain.Core;

public interface ICommandProvider
{
    TCommand Get<TCommand>() where TCommand : ICommandMarkerInterface;
}