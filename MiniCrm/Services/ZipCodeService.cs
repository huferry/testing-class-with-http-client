namespace MiniCrm.Services;

public class ZipCodeService
{
    public async Task<string> GetZipCode(string customerId)
    {
        var uri = new Uri($"http://utomo.eu/api/zipcode/{customerId}");
        
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        HttpClient client = new();

        var response = await client.SendAsync(request);
        
        return await response
            .EnsureSuccessStatusCode()
            .Content
            .ReadAsStringAsync();
    }
}