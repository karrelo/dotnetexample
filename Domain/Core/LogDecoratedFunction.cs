using Microsoft.AspNetCore.Mvc;

namespace dotnetcqstemplate.Domain.Core;

public abstract class LogDecoratedFunction
{
    private readonly ILogger _log;

    protected LogDecoratedFunction(ILogger log) => _log = log;

    protected virtual async Task<IActionResult> Run(Func<Task<IActionResult>> func, [System.Runtime.CompilerServices.CallerMemberName] string functionName = DefaultCallerName) =>
        await ExecuteWithLogging(func, functionName);

    protected virtual async Task Run(Func<Task> action, [System.Runtime.CompilerServices.CallerMemberName] string functionName = DefaultCallerName) =>
        await ExecuteWithLogging(action, functionName);

    private T ExecuteWithLogging<T>(Func<T> func, string functionName)
    {
        try
        {
            _log.LogInformation($"Executing func {functionName} [returning from {nameof(LogDecoratedFunction)}]");
            return func();
        }
        finally
        {
            _log.LogInformation($"Executed func {functionName} [returning from {nameof(LogDecoratedFunction)}]");
        }
    }

    private const string DefaultCallerName = "unknown";
}