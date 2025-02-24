using Microsoft.AspNetCore.Http;
using Moq;
using Bewoning.Data.Service.Services;
using System;
using Test.RvigHaalCentraal.Util;
using Rvig.BrpApi.Bewoningen.ApiModels.Universal;
using Rvig.BrpApi.Bewoningen.Exceptions;
using Rvig.BrpApi.Bewoningen.Validation;

namespace Test.RvigHaalCentraal.Tests.ErrorHandling
{
    [TestClass]
    public class TestErrorResponseService
    {
        private IErrorResponseService? _errorResponseService;

        [TestMethod]
        public void CreateBadRequestFoutbericht()
        {
            // Arrange
            var responseStatusCode = 400;
            _errorResponseService = new ErrorResponseService(MockHelper.ArrangeLogger($"{responseStatusCode}_error_response_service_test_access_log", $"{responseStatusCode}_error_response_service_test_applicatie_log", $"{responseStatusCode}_error_response_service_test_trace_log", $"Test{responseStatusCode}ErrorResponseServiceLogFolder"));
            var httpContext = MockHelper.MockIHttpContextAccessor(responseStatusCode).HttpContext;
            var invalidParams = new List<InvalidParams>
            {
                new InvalidParams
                {
                        Name = "randomparam",
                        Code = nameof(InvalidParamCode.unknownParam),
                        Reason = ValidationErrorMessages.UnexpectedParam
                }
            };

            const string path = "/api/test/check";
            var exception = new InvalidParamsException(invalidParams);

            var expected = new BadRequestFoutbericht
            {
                Code = nameof(ErrorCode.paramsValidation),
                Detail = "De foutieve parameter(s) zijn: randomparam.",
                Title = "Een of meerdere parameters zijn niet correct.",
                Status = responseStatusCode,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Instance = path,
                InvalidParams = invalidParams
            };

            // Act
            var foutbericht = _errorResponseService.CreateBadRequestFoutbericht(httpContext!, exception, path);

            // Assert
            foutbericht.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void CreateFoutBericht()
            => SetupTest(401, "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1", "Niet correct geauthenticeerd.", ErrorCode.authentication, new AuthenticationException());

        [TestMethod]
        public void Create406FoutBericht()
            => SetupTest(406, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.6", "Gevraagde content type wordt niet ondersteund.", ErrorCode.notAcceptable, new NotAcceptableException());

        [TestMethod]
        public void Create415FoutBericht()
            => SetupTest(415, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.13", "Media Type wordt niet ondersteund.", ErrorCode.unsupportedMediaType, new UnsupportedMediaTypeException());

        [TestMethod]
        public void Create503FoutBericht()
            => SetupTest(503, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.4", "Bronservice is niet beschikbaar.", ErrorCode.sourceUnavailable, new ServiceUnavailableException());

        private void SetupTest(int responseStatusCode, string errorType, string errorTitle, ErrorCode code, Exception exception)
        {
            // Arrange
            var httpContextAccessor = MockHelper.MockIHttpContextAccessor(responseStatusCode);
            _errorResponseService = new ErrorResponseService(MockHelper.ArrangeLogger($"{responseStatusCode}_error_response_service_test_access_log", $"{responseStatusCode}_error_response_service_test_applicatie_log", $"{responseStatusCode}_error_response_service_test_trace_log", $"Test{responseStatusCode}ErrorResponseServiceLogFolder", httpContextAccessor: httpContextAccessor));
            var response = Mock.Of<HttpResponse>();

            var expected = new Foutbericht
            {
                Type = errorType,
                Title = errorTitle,
                Status = responseStatusCode,
                Instance = "/api/test/check",
                Code = code.ToString()
            };

            // Act
            var foutbericht = _errorResponseService.CreateFoutBericht(httpContextAccessor.HttpContext!, response, exception, httpContextAccessor.HttpContext!.Request.Path.Value!);

            // Assert
            foutbericht.Should().BeEquivalentTo(expected);
        }
    }
}
