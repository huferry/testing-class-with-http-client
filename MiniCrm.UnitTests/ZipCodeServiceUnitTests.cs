using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MiniCrm.Services;
using NUnit.Framework;

namespace MiniCrm.UnitTests;

public class ZipCodeServiceUnitTests
{
    private SendAsync? _sendAsync;
    private ZipCodeService Sut => new(_sendAsync!);

    [Test]
    public async Task GetZipCode_WhenSuccessful_ReturnsFromBody()
    {
        // arrange
        var customerId = 12;
        var expectation = "12391";

        _sendAsync = _ => Task.FromResult(new HttpResponseMessage
        {
            Content = new StringContent($"{{\"zipcode\": \"{expectation}\"}}")
        });

        // act
        var actual = await Sut.GetZipCode(customerId);

        // assert
        Assert.AreEqual(expectation, actual);
    }

    [Test]
    public void GetZipCode_WhenUnsuccessful_Throws()
    {
        // arrange
        var customerId = 31;

        _sendAsync = _ => Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest
        });

        // act
        // assert
        Assert.ThrowsAsync<HttpRequestException>(() => Sut.GetZipCode(customerId));
    }
}