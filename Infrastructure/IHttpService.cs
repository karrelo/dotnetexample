namespace dotnetcqstemplate.Infrastructure;

public interface IHttpService
{
    Task<(bool, TResponseBody body, Status statusCode)> SendAsync<TResponseBody>(string url, Header[] header = null);

    public enum Status
    {
        Ok,
        BadRequest,
        Unathourized,
        Error
    }

    public record Header(string Key, string Value);
}