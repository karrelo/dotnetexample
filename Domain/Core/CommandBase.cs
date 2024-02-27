namespace dotnetcqstemplate.Domain.Core;

public abstract class CommandBase<TResult> : ICommandMarkerInterface
{
    private readonly ILogger _log;
    private readonly string _commandTypeName;

    protected CommandBase(ILogger log)
    {
        _log = log;
        _commandTypeName = GetType().Name;
    }

    protected async Task<TResult> Execute(Func<Task<TResult>> commandFunc, object parameter = null)
    {
        try
        {
            _log.LogInformation("Executing command {_commandTypeName}, using {commandName}", _commandTypeName, nameof(CommandBase<TResult>));
            return await ExecuteWithTracing(commandFunc, parameter);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "{_commandTypeName} failed with exception {exception}, Message: {message}, Inner Exception: {innerException}", _commandTypeName, ex.GetType().Name, ex.Message, ex.InnerException);
            throw;
        }
        finally
        {
            _log.LogInformation($"Executed command {GetType().Name}, using {nameof(CommandBase<TResult>)}");
        }
    }

    private async Task<TResult> ExecuteWithTracing(Func<Task<TResult>> commandFunc, object parameter)
    {
        _log.LogTrace("TRACE: Executing command {_commandTypeName}, {parameter}", _commandTypeName, parameter == null ? "without parameter" : "with parameter" + JsonConvert.Serialize(parameter));
        var result = await commandFunc();
        _log.LogTrace("TRACE: Executed command {_commandTypeName}, result={result}", _commandTypeName, JsonConvert.Serialize(result));
        return result;
    }

}

public abstract class CommandBase : ICommandMarkerInterface
{
    private readonly ILogger _log;
    private readonly string _commandTypeName;

    protected CommandBase(ILogger log)
    {
        _log = log;
        _commandTypeName = GetType().Name;
    }

    protected async Task Execute(Func<Task> commandFunc, object parameter = null)
    {
        try
        {
            _log.LogInformation("Executing command {_commandTypeName}, using {commandName}", _commandTypeName, nameof(CommandBase));
            await ExecuteWithTracing(commandFunc, parameter);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "{_commandTypeName} failed with exception {exception}, Message: {message}, Inner Exception: {innerException}", _commandTypeName, ex.GetType().Name, ex.Message, ex.InnerException);
            throw;
        }
        finally
        {
            _log.LogInformation($"Executed command {GetType().Name}, using {nameof(CommandBase)}");
        }
    }

    private async Task ExecuteWithTracing(Func<Task> commandFunc, object parameter)
    {
        _log.LogTrace("TRACE: Executing command {_commandTypeName}, {parameter}", _commandTypeName, parameter == null ? "without parameter" : "with parameter" + JsonConvert.Serialize(parameter));
        var task = commandFunc();
        await task;


    }

}