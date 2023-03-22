namespace MiniCrm.Services;

public interface IHttpClientAdapter
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
}

public class HttpClientAdapter : HttpClient, IHttpClientAdapter
{
}