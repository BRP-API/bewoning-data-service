using System.IO;
using System.Text;
using System.Threading.Tasks;
using Bewoning.Data.Service.Tests.Util;
using Bewoning.Api.Validation;
using Bewoning.Api.Exceptions;

namespace Bewoning.Data.Service.Tests.Api;

[TestClass]
public class TestApiCallValidator
{
    [TestMethod]
    public async Task TestInvalidRequestBody()
    {
        // Arrange
        var httpContext = MockHelper.FakeHttpContext();
        var request = MockHelper.CreateDefaultRvigRequest();
        httpContext.Request.Body = new MemoryStream(Encoding.ASCII.GetBytes("this is not a valid json object."));

        // Act/Assert
        await Assert.ThrowsExceptionAsync<InvalidRequestBodyException>(() => ApiCallValidator.ValidateUnusableQueryParams(request, httpContext));
    }

    [TestMethod]
    public async Task TestParamUnknownToTestType()
    {
        // Arrange
        var httpContext = MockHelper.FakeHttpContext();
        var request = MockHelper.CreateDefaultRvigRequest();
        httpContext.Request.Body = new MemoryStream(Encoding.ASCII.GetBytes("{ \"fields\": [\"burgerservicenummer\"], \"type\": \"RaadpleegMetBurgerservicenummer\", \"burgerservicenummer\": [\"999994669\"], \"testje\": \"test\" }"));

        // Act/Assert
        await Assert.ThrowsExceptionAsync<InvalidParamsException>(() => ApiCallValidator.ValidateUnusableQueryParams(request, httpContext));
    }
}
