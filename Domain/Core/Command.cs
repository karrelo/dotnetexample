using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace dotnetcqstemplate.Domain.Core;

public abstract class Command<TParameter, TResult> : CommandBase<TResult>
{
    protected Command(ILogger log) : base(log) { }

    public async Task<TResult> Execute(TParameter parameter) => await Execute(() => ExecuteCommand(parameter), parameter);

    protected abstract Task<TResult> ExecuteCommand(TParameter parameter);
}

public abstract class Command<TParameter> : CommandBase
{
    protected Command(ILogger log) : base(log) { }

    public virtual async Task Execute(TParameter parameter) => await Execute(() => ExecuteCommand(parameter), parameter);

    protected abstract Task ExecuteCommand(TParameter parameter);
}