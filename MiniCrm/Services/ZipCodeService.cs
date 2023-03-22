using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiniCrm.Services;

public class ZipCodeService
{
    private readonly IHttpClientAdapter _httpClientAdapter;

    public ZipCodeService(IHttpClientAdapter httpClientAdapter)
    {
        _httpClientAdapter = httpClientAdapter;
    }

    public async Task<string> GetZipCode(int customerId)
    {
        var uri = new Uri($"https://641b3a009b82ded29d4d70c3.mockapi.io/zipcode/{customerId}");

        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        var response = await _httpClientAdapter.SendAsync(request);

        var content = await response
            .EnsureSuccessStatusCode()
            .Content
            .ReadAsStringAsync();

        return JsonSerializer.Deserialize<Zip>(content)!.Zipcode;
    }

    private sealed record Zip([property:JsonPropertyName("zipcode")] string Zipcode);
}