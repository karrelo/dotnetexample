namespace dotnetcqstemplate.Domain.Core;

public interface IQueryProvider
{
    TQuery Get<TQuery>() where TQuery : IQueryMarkerInterface;
}