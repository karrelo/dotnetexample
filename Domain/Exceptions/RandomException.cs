using System.Runtime.Serialization;

namespace dotnetcqstemplate.Domain.Exceptions;

public class RandomException : Exception
{
    public RandomException() { }

    protected RandomException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}