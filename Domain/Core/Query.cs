
namespace dotnetcqstemplate.Domain.Core;

public abstract class Query<TParameter, TStatus, TResult> : QueryBase<TStatus, TResult>
{
    protected Query(ILogger<QueryBase<TStatus, TResult>> log) : base(log)
    {
    }

    public virtual async Task<(TStatus status, TResult model)> Execute(TParameter parameter) => await Execute(() => ExecuteQuery(parameter), parameter);

    protected abstract Task<(TStatus status, TResult model)> ExecuteQuery(TParameter parameter);
}

public abstract class Query<TStatus, TResult> : QueryBase<TStatus, TResult>
{
    protected Query(ILogger<QueryBase<TStatus, TResult>> log) : base(log)
    {
    }

    public virtual async Task<(TStatus status, TResult model)> Execute() => await Execute(ExecuteQuery);

    protected abstract Task<(TStatus status, TResult model)> ExecuteQuery();
}