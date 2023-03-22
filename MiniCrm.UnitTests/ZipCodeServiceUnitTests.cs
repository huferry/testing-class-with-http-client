using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MiniCrm.Services;
using Moq;
using NUnit.Framework;

namespace MiniCrm.UnitTests;

public class ZipCodeServiceUnitTests
{
    private readonly Mock<IHttpClientAdapter> _httpClientAdapterMock = new();
    private ZipCodeService Sut => new(_httpClientAdapterMock.Object);

    [Test]
    public async Task GetZipCode_WhenSuccessful_ReturnsFromBody()
    {
        // arrange
        var expectation = "73091";
        var customerId = 12;

        var response = new HttpResponseMessage
        {
            Content = new StringContent($"{{\"zipcode\": \"{expectation}\"}}")
        };

        _httpClientAdapterMock
            .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(response);

        // act
        var actual = await Sut.GetZipCode(customerId);

        // assert
        Assert.AreEqual(expectation, actual);
    }
    
    [Test]
    public void GetZipCode_WhenUnsuccessful_Throws()
    {
        // arrange
        var customerId = 15;

        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Forbidden 
        };

        _httpClientAdapterMock
            .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(response);

        // act
        // assert
        Assert.ThrowsAsync<HttpRequestException>(() => Sut.GetZipCode(customerId));
    }
}