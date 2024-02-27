namespace dotnetcqstemplate.Domain.Core;

public interface IProcessFactory
{
    TProcess CreateProcess<TProcess>();
}