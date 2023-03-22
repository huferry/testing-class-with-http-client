namespace MiniCrm.Services;

public class ZipCodeService
{
    private readonly IHttpClientAdapter _httpClientAdapter;

    public ZipCodeService(IHttpClientAdapter httpClientAdapter)
    {
        _httpClientAdapter = httpClientAdapter;
    }

    public async Task<string> GetZipCode(string customerId)
    {
        var uri = new Uri($"http://utomo.eu/api/zipcode/{customerId}");

        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        var response = await _httpClientAdapter.SendAsync(request);

        return await response
            .EnsureSuccessStatusCode()
            .Content
            .ReadAsStringAsync();
    }
}