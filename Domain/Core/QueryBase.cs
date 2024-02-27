namespace dotnetcqstemplate.Domain.Core;

public abstract class QueryBase<TStatus, TResult> : IQueryMarkerInterface
{
    private readonly ILogger<QueryBase<TStatus, TResult>> _log;

    private readonly string _queryTypeName;

    protected QueryBase(ILogger<QueryBase<TStatus, TResult>> log)
    {
        _log = log;
        _queryTypeName = GetType().Name;
    }

    protected (TStatus status, TResult model) QueryFailedResult(TStatus status) => (status, default);

    protected async Task<(TStatus status, TResult model)> Execute(Func<Task<(TStatus status, TResult model)>> queryFunc, object parameter = null)
    {
        try
        {
            _log.LogInformation($"Executing query {_queryTypeName}, using {nameof(QueryBase<TStatus, TResult>)}");
            return await ExecuteWithTracing(queryFunc, parameter);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"{_queryTypeName} failed with exception {ex.GetType().Name}");
            return default;
        }
        finally
        {
            _log.LogInformation($"Executed query {_queryTypeName}, using {nameof(QueryBase<TStatus, TResult>)}");
        }
    }

    private async Task<(TStatus status, TResult model)> ExecuteWithTracing(Func<Task<(TStatus status, TResult model)>> queryFunc, object parameter)
    {
        _log.LogTrace($"TRAFE: Executing query {_queryTypeName}, {AppendParameter(parameter)}");
        var result = await queryFunc();
        _log.LogTrace($"TRACE: Executed query {_queryTypeName}, {AppendResult(result)}");
        return result;
    }

    private static string AppendResult((TStatus status, TResult model) result) => $"result={JsonConvert.Serialize(new { result.model, result.status })}";

    private static string AppendParameter(object parameter) => $"{(parameter == null ? "without parameter" : $"with parameter={JsonConvert.Serialize(parameter)}")}";
}