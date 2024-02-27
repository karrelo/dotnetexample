using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace dotnetcqstemplate.Infrastructure;

public class HttpService : IHttpService
{
    public const string HttpClientName = "no-redirecting-client";

    private readonly ILogger<IHttpService> _log;

    private readonly HttpClient _httpClient;

    public HttpService(ILogger<IHttpService> log, IHttpClientFactory httpClientFactory)
    {
        _log = log;
        _httpClient = httpClientFactory.CreateClient(HttpClientName);
    }

    public async Task<(bool, TResponseBody body, IHttpService.Status statusCode)> SendAsync<TResponseBody>(string url, IHttpService.Header[] headers = null)
    {
        var reqMessage = CreateRequestMessage(url, headers, HttpMethod.Get);

        try
        {
            _log.LogInformation($"Making an HTTP {reqMessage.Method} request to {reqMessage.RequestUri}");
            var (json, response) = await SendWithTracing(reqMessage);
            _log.LogInformation($"Made an HTTP {reqMessage.Method} request to {reqMessage.RequestUri}");

            if (!response.IsSuccessStatusCode)
            {
                _log.LogError($"HTTP request: {reqMessage} failed with code: {response.StatusCode} to {reqMessage.RequestUri}");

                return (int)response.StatusCode switch
                {
                    StatusCodes.Status401Unauthorized => DefaultErrorResponse<TResponseBody>(IHttpService.Status.Unathourized),
                    StatusCodes.Status403Forbidden => DefaultErrorResponse<TResponseBody>(IHttpService.Status.Unathourized),
                    StatusCodes.Status400BadRequest => DefaultErrorResponse<TResponseBody>(IHttpService.Status.BadRequest),
                    >= 500 => DefaultErrorResponse<TResponseBody>(IHttpService.Status.Error),
                    _ => DefaultErrorResponse<TResponseBody>(IHttpService.Status.Error)
                };
            }

            return (true, CreateResult<TResponseBody>(json), IHttpService.Status.Ok);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"HTTP {reqMessage} failed, exception caught");
            return DefaultErrorResponse<TResponseBody>(IHttpService.Status.Error);
        }
    }

    private HttpRequestMessage CreateRequestMessage(string url, IHttpService.Header[] headers = null, HttpMethod method = null)
    {
        var reqMessage = new HttpRequestMessage(method ?? HttpMethod.Get, url);

        if (headers != null)
        {
            foreach (var h in headers)
            {
                reqMessage.Headers.Add(h.Key, h.Value);
            }
        }

        return reqMessage;
    }
    private async Task<(string JsonResult, HttpResponseMessage responseMessage)> SendWithTracing(HttpRequestMessage reqMessage)
    {
        try
        {
            _log.LogTrace($"TRACE: Making an HTTP {reqMessage.Method} request to {reqMessage.RequestUri}, RequestContent: {JsonConvert.Serialize(reqMessage.Content)}");
            var result = await _httpClient.SendAsync(reqMessage);
            var json = await result.Content.ReadAsStringAsync();
            _log.LogTrace($"TRACE: Made an HTTP {reqMessage.Method} request to {reqMessage.RequestUri}, Response: {JsonConvert.Serialize(json)}");
            return (json, result);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"Error while sending request to {reqMessage.RequestUri}");
            throw;
        }
    }

    private TJsonRoot CreateResult<TJsonRoot>(string json)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<TJsonRoot>(json);
            _log.LogInformation($"Result deserialized to {typeof(TJsonRoot).FullName} successfully");

            return result;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"something wnt wrong while deserialising {json} into type: {typeof(TJsonRoot)}");
            throw;
        }
    }

    private (bool success, T body, IHttpService.Status StatusCode) DefaultErrorResponse<T>(IHttpService.Status status)
    {
        var defaultErrorResponse = (false, (T)default, status);
        _log.LogInformation($"HttpService returning object {JsonConvert.Serialize(defaultErrorResponse)}");

        return defaultErrorResponse;
    }
}