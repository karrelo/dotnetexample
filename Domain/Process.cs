using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace dotnetcqstemplate.Domain.Core;


public abstract class Process<TStatus, TOut>
{
    protected readonly IConfiguration Configuration;

    protected Process(IConfiguration configuration) => Configuration = configuration;

    public abstract Task<(TStatus status, TOut model)> Run();
}