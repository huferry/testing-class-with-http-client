namespace MiniCrm.Services;

public class ZipCodeService
{
    private readonly SendAsync _sendAsync;

    public ZipCodeService(SendAsync sendAsync)
    {
        _sendAsync = sendAsync;
    }

    public async Task<string> GetZipCode(string customerId)
    {
        var uri = new Uri($"http://utomo.eu/api/zipcode/{customerId}");
        
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        var response = await _sendAsync(request);
        
        return await response
            .EnsureSuccessStatusCode()
            .Content
            .ReadAsStringAsync();
    }
}