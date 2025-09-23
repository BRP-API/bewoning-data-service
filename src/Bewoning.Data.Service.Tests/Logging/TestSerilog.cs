using Bewoning.Data.Service.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using static Bewoning.Data.Service.Models.HttpLoggingModelSegment;
using static Bewoning.Data.Service.Models.RequestLoggingModelSegment;
using Bewoning.Data.Service.Tests.Util;
using Bewoning.Api.Helpers;
using Bewoning.Api.Exceptions;
using Bewoning.Api.ApiModels.Universal;

namespace Bewoning.Data.Service.Tests.Logging
{
    [TestClass]
    public class TestSerilog
    {
        private ILoggingHelper? _loggingHelper;

        [TestMethod]
        public async Task Test200Log()
        {
            // Arrange
            var message = "This is a debug message that something succeeded.";
            var logNameAccessLog = "200_test_access_log";
            var logNameApplicationLog = "200_test_applicatie_log";
            var logNameTraceLog = "200_test_trace_log";
            var logFolderName = "Test200LogFolder";
            _loggingHelper = MockHelper.ArrangeLogger(logNameAccessLog, logNameApplicationLog, logNameTraceLog, logFolderName);

            // Act
            _loggingHelper?.LogDebug(message);

            // Assert
            CommonAsserts(logNameAccessLog, logNameApplicationLog, logNameTraceLog, logFolderName);
            var logModel = await GetResultLogModel(logNameAccessLog, logNameApplicationLog, logNameTraceLog, logFolderName);
            var expectedLogModel = GenerateExpectedLogModel(logModel?.Timestamp, logLevel: "Debug", message: message);
            logModel.Should().BeEquivalentTo(expectedLogModel);
        }

        [TestMethod]
        public async Task TestBadRequestErrorLog()
        {
            // Arrange/Act/Assert
            int responseStatusCode = 400;
            var responseBody = new BadRequestFoutbericht
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "Een of meerdere parameters zijn niet correct.",
                Status = responseStatusCode,
                Detail = "De foutieve parameter(s) zijn: fields[0].",
                Instance = "/api/test/check",
                Code = nameof(ErrorCode.paramsValidation),
                InvalidParams = new List<InvalidParams>
                {
                    new InvalidParams
                    {
                        Code = "fields",
                        Name = "fields[0]",
                        Reason = "Parameter bevat een niet bestaande veldnaam."
                    }
                }
            };
            await ErrorLogArrangeActAssert(responseStatusCode, "400_test_access_log", "400_test_applicatie_log", "400_test_trace_log", "Test400LogFolder", responseBody);
        }

        [TestMethod]
        public async Task TestAuthenticatedErrorLog()
        {
            // Arrange
            int responseStatusCode = 401;
            var responseBody = new Foutbericht
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
                Title = "Niet correct geauthenticeerd.",
                Status = responseStatusCode,
                Instance = "/api/test/check",
                Code = nameof(ErrorCode.authentication)
            };
            await ErrorLogArrangeActAssert(responseStatusCode, "401_test_access_log", "401_test_applicatie_log", "401_test_trace_log", "Test401LogFolder", responseBody);
        }

        [TestMethod]
        public async Task TestUnauthorizedErrorLog()
        {
            // Arrange
            int responseStatusCode = 403;
            var responseBody = new Foutbericht
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
                Title = "U bent niet geautoriseerd voor het gebruik van deze API.",
                Status = responseStatusCode,
                Detail = "Niet geautoriseerd voor ad hoc bevragingen.",
                Instance = "/api/test/check",
                Code = "unauthorized"
            };
            await ErrorLogArrangeActAssert(responseStatusCode, "403_test_access_log", "403_test_applicatie_log", "403_test_trace_log", "Test403LogFolder", responseBody);
        }

        private static void CommonAsserts(string logNameAccessLog, string logNameApplicationLog, string logNameTraceLog, string logFolderName)
        {
            Directory.Exists($"./{logFolderName}").Should().BeTrue();
            File.Exists($"./{logFolderName}/{logNameAccessLog}").Should().BeTrue();
            File.Exists($"./{logFolderName}/{logNameApplicationLog}").Should().BeTrue();
            File.Exists($"./{logFolderName}/{logNameTraceLog}").Should().BeTrue();
        }

        private static async Task<CustomElasticLogModel?> GetResultLogModel(string logNameAccessLog, string logNameApplicationLog, string logNameTraceLog, string logFolderName)
        {
            File.Copy($"./{logFolderName}/{logNameAccessLog}", $"./{logFolderName}/copy_{logNameAccessLog}");

            var text = await File.ReadAllTextAsync($"./{logFolderName}/copy_{logNameAccessLog}");
            var logModel = JsonConvert.DeserializeObject<CustomElasticLogModel>(text);

            // Path is set in Serilog from unknown sources (doesn't seem to be from HttpContext Request) and therefore we set it manually just in case.
            logModel!.Url ??= new UrlLoggingModelSegment
            {
                Path = "/api/test/check"
            };

            return logModel;
        }

        private static CustomElasticLogModel GenerateExpectedLogModel(DateTimeOffset? timestamp, Foutbericht? responseBody = null, int responseStatusCode = 200, string logLevel = "Information", string? message = null)
        {
            return new CustomElasticLogModel
            {
                Timestamp = timestamp,
                Url = new UrlLoggingModelSegment
                {
                    Path = "/api/test/check"
                },
                Http = new HttpLoggingModelSegment
                {
                    Request = new HttpRequestLoggingModelSegment
                    {
                        Method = "POST"
                    },
                    Response = new HttpResponseLoggingModelSegment
                    {
                        StatusCode = responseStatusCode,
                        Body = responseBody
                    }
                },
                Log = new LogLoggingModelSegment
                {
                    Logger = "API",
                    Level = logLevel
                },
                Version = "2.0.0",
                Token = new List<string?>
                {
                    "OIN=000000099000000080000",
                    "afnemerID=000008",
                    "gemeenteCode=0800"
                },
                Request = new RequestLoggingModelSegment
                {
                    Body = new BodyLoggingModelSegment
                    {
                        Stringified = "{ \"fields\": [\"burgerservicenummer\"], \"type\": \"RaadpleegMetBurgerservicenummer\", \"burgerservicenummer\": [\"999994669\"] }"
                    }
                },
                Message = message
            };
        }

        private async Task ErrorLogArrangeActAssert(int responseStatusCode, string logNameAccessLog, string logNameApplicationLog, string logNameTraceLog, string logFolderName, Foutbericht responseBody)
        {
            _loggingHelper = MockHelper.ArrangeLogger(logNameAccessLog, logNameApplicationLog, logNameTraceLog, logFolderName, responseStatusCode, "Error");

            // Act
            _loggingHelper?.LogError(responseBody);

            // Assert
            await ErrorLogAssertion(responseStatusCode, responseBody, logNameAccessLog, logNameApplicationLog, logNameTraceLog, logFolderName);
        }

        private static async Task ErrorLogAssertion(int responseStatusCode, Foutbericht responseBody, string logNameAccessLog, string logNameApplicationLog, string logNameTraceLog, string logFolderName)
        {
            CommonAsserts(logNameAccessLog, logNameApplicationLog, logNameTraceLog, logFolderName);
            var logModel = await GetResultLogModel(logNameAccessLog, logNameApplicationLog, logNameTraceLog, logFolderName);

            var expectedLogModel = GenerateExpectedLogModel(logModel?.Timestamp, responseBody, responseStatusCode, "Error");
            logModel.Should().BeEquivalentTo(expectedLogModel);
        }
    }
}
