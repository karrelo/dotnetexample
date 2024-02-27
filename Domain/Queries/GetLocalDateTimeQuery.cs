using dotnetcqstemplate.Domain.Core;

namespace dotnetcqstemplate.Domain.Queries;

public class GetLocalDateTimeQuery : Query<GetLocalDateTimeQuery.Status, DateTime>
{
    public GetLocalDateTimeQuery(ILogger<GetLocalDateTimeQuery> log) : base(log) { }
    protected override Task<(Status status, DateTime model)> ExecuteQuery() => Task.FromResult((Status.Ok, DateTime.Now));

    public enum Status
    {
        Ok
    }

    public class Configuration
    {
        public string x { get; set; }
    }
}