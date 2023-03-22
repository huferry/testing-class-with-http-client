namespace MiniCrm.Services;

public delegate Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

public static class HttpClientBridge
{
    public static SendAsync SendAsync => new HttpClient().SendAsync;
}