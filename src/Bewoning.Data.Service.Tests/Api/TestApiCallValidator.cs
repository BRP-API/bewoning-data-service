using System.IO;
using System.Text;
using System.Threading.Tasks;
using Bewoning.Data.Service.Tests.Util;
using Bewoning.Api.Validation;
using Bewoning.Api.Exceptions;
using Xunit;

namespace Bewoning.Data.Service.Tests.Api;

public class TestApiCallValidator
{
    [Fact]
    public async Task TestInvalidRequestBody()
    {
        // Arrange
        var httpContext = MockHelper.FakeHttpContext();
        var request = MockHelper.CreateDefaultRvigRequest();
        httpContext.Request.Body = new MemoryStream(Encoding.ASCII.GetBytes("this is not a valid json object."));

        // Act/Assert
        await Assert.ThrowsAsync<InvalidRequestBodyException>(() => ApiCallValidator.ValidateUnusableQueryParams(request, httpContext));
    }

    [Fact]
    public async Task TestParamUnknownToTestType()
    {
        // Arrange
        var httpContext = MockHelper.FakeHttpContext();
        var request = MockHelper.CreateDefaultRvigRequest();
        httpContext.Request.Body = new MemoryStream(Encoding.ASCII.GetBytes("{ \"fields\": [\"burgerservicenummer\"], \"type\": \"RaadpleegMetBurgerservicenummer\", \"burgerservicenummer\": [\"999994669\"], \"testje\": \"test\" }"));

        // Act/Assert
        await Assert.ThrowsAsync<InvalidParamsException>(() => ApiCallValidator.ValidateUnusableQueryParams(request, httpContext));
    }
}
