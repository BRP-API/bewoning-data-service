using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Validation;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Test.RvigHaalCentraal.Util;

namespace Test.RvigHaalCentraal.Tests.Api;

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
