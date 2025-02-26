using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.IO;
using System.Text;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Runtime.Serialization;
using System.Security.Principal;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Bewoning.Data.Service.Util;
using Rvig.BrpApi.Bewoningen.Helpers;
using Rvig.BrpApi.Bewoningen.Options;
using Rvig.BrpApi.Bewoningen.Util;
using Rvig.Data.Bewoningen.Repositories;

namespace Test.RvigHaalCentraal.Util;

public static class MockHelper
{
	[DataContract]
	public class TestRaadpleegMetBurgerservicenummer
	{
		/// <summary>
		/// Gets or Sets Burgerservicenummer
		/// </summary>
		[DataMember(Name = "burgerservicenummer", EmitDefaultValue = false)]
		[JsonConverter(typeof(JsonArrayConverter))]
		// Null forgiving operator required because of validation. A user may possibly send a request without this list.
		// That should not be accepted but specs require us to throw a validation exception regarding this requirement.
		public List<string> burgerservicenummer { get; set; } = null!;

		/// <summary>
		/// Gets or Sets Type
		/// </summary>
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public string? type { get; set; }

		/// <summary>
		/// Hiermee kun je de inhoud van de resource naar behoefte aanpassen door een lijst van paden die verwijzen naar de gewenste velden op te nemen ([zie functionele specificaties &#39;fields&#39; properties](https://raw.githubusercontent.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/develop/features/fields.feature)).  De te gebruiken paden zijn beschreven in [fields-Persoon.csv](https://raw.githubusercontent.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/develop/features/fields-Persoon.csv) (voor gebruik fields bij raadplegen) en [fields-PersoonBeperkt.csv](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/develop/features/fields-PersoonBeperkt.csv) (voor gebruik fields bij zoeken) waarbij in de eerste kolom het fields-pad staat en in de tweede kolom het volledige pad naar het gewenste veld.
		/// </summary>
		/// <value>Hiermee kun je de inhoud van de resource naar behoefte aanpassen door een lijst van paden die verwijzen naar de gewenste velden op te nemen ([zie functionele specificaties &#39;fields&#39; properties](https://raw.githubusercontent.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/develop/features/fields.feature)).  De te gebruiken paden zijn beschreven in [fields-Persoon.csv](https://raw.githubusercontent.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/develop/features/fields-Persoon.csv) (voor gebruik fields bij raadplegen) en [fields-PersoonBeperkt.csv](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/develop/features/fields-PersoonBeperkt.csv) (voor gebruik fields bij zoeken) waarbij in de eerste kolom het fields-pad staat en in de tweede kolom het volledige pad naar het gewenste veld. </value>
		[DataMember(Name = "fields", EmitDefaultValue = false)]
		[JsonConverter(typeof(JsonArrayConverter))]
		public List<string> fields { get; set; } = new List<string>();

		/// <summary>
		/// Een code die aangeeft in welke gemeente de persoon woont, of de laatste gemeente waar de persoon heeft gewoond, of de gemeente waar de persoon voor het eerst is ingeschreven.
		/// </summary>
		/// <value>Een code die aangeeft in welke gemeente de persoon woont, of de laatste gemeente waar de persoon heeft gewoond, of de gemeente waar de persoon voor het eerst is ingeschreven. </value>
		[DataMember(Name = "gemeenteVanInschrijving", EmitDefaultValue = false)]
		public virtual string? gemeenteVanInschrijving { get; set; }
	}

	public static IHttpContextAccessor MockIHttpContextAccessor(int responseStatusCode = 200, string? userName = "990008")
	{
		//Mock IHttpContextAccessor
		var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
		var context = FakeHttpContext(responseStatusCode, true, userName);
		mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

		return mockHttpContextAccessor.Object;
	}

	public static HttpContext FakeHttpContext(int responseStatusCode = 200, bool serializeResponseAsString = false, string? userName = "990008")
	{
		var postBody = "{ \"fields\": [\"burgerservicenummer\"], \"type\": \"RaadpleegMetBurgerservicenummer\", \"burgerservicenummer\": [\"999994669\"] }";
		var headers = new Dictionary<string, StringValues>
		{
		   { "Accept", "application/json" },
		   { "Content-Type", "application/json" },
		   { "Authorization", "Bearer eyJraWQiOiI1NTMwMDk2NDM1NTI0NzA1MTU4NjA1NTMyODgwOTI2NzMxNTkwODI5NTE1NzY3OTAiLCJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpc3MiOiJodHRwczovL2xvZ2luLmRldi5pZHNlY3VyZS5ubC9uaWRwL29hdXRoL25hbSIsImp0aSI6IjAwODdkMjIyLTBlNjQtNDc1OC1iZjVhLTVkNzNiNDc1YjZlOCIsImF1ZCI6IjVkZGZlNjc4LTkyOGItNDI3MC1hMjBhLTBkNmNkNTk3OWYzMSIsImV4cCI6MTY4MzUzMjEwMywiaWF0IjoxNjgzNTI0OTAzLCJuYmYiOjE2ODM1MjQ4NzMsInN1YiI6IjVkZGZlNjc4LTkyOGItNDI3MC1hMjBhLTBkNmNkNTk3OWYzMSIsIl9wdnQiOiIxbFlGL3YwUmxlUVpzQmp5SkpQVTE2eFNwQkdTeHBMVUlmbzh6ZWFyNURyRkxyRHVWelM5a29UNy9oNHdqSFVsUjFNd25Wckg0WGtXVURodE8vYitrUEFONkpjTUtzeEZLNUZiMENhWFMxZG05OFUyYkJIVXBjOHVuVG9xYUpHM0V0T0RITTh0cUlnQmpjWFBFQWN1aUo3NUE1MTV5ajg4cVk3MGQ1akFWNHJxMEFCWk1jWjdXUHg2ZG4ycExpWHA2S3QxbXZCV2FYUy9jRTJUZG1MaTZHQ05qVFgxNTFRSXFLaTZnekFGSFU0PS44Iiwic2NvcGUiOlsiMDAwMDAwMDk5MDAwMDAwMDgwMDAwIl0sImNsYWltcyI6WyJPSU49MDAwMDAwMDk5MDAwMDAwMDgwMDAwIiwiYWZuZW1lcklEPTAwMDAwOCIsImdlbWVlbnRlQ29kZT0wODAwIl19.5E44_B2X_T4ZIbMYzeia-xwGVOYPWKswnXvYEMLi2Dhe8ASCDRKDlInKyDZDMnzn-2SOBMn1TBx5I8mci9XPG43hZB4ORmwXDhzeG5R8lYTM_WicJQSxpVlRogjaiXVjekrTOIpfkOzKw_D2xGPerCZ9qFPYUdj280s7lGyd1ywG9CuikPDdN7nG83vVdwAQ5Sn-mB7IzG-sMZgLxb9N5v18eDdB7tgEa6tUg5jouV8vqv_okNQIgduTfa_K_ZLeQn9LA2JHq83uf5rZ_hoJDFuSEF8zEBX7u_M8TTEfctEqf9aBq9i6R8kSZ5qSHz4w-lM9zLL-BkzGhbk-kjfHf1LeQYBhQORC6i5w24J6kflNEZ9rjo0yQjN4RsAYWF71PJ9Ao0Qi8b2AOoxjAK3DnS3YomPP5A89ht58_pspXIXPS4BTFujOz6sgrk2TiDt_5AvtFaUKZHGaewk-gmxC_Am8UqIVDN0YnlJVyrrDRYKXLREMOJBQ5N9nF1YSiur99B8WHE-hdrER671uFWD6OuxDN7KyRnah-ed0SDkc-oxw6fLGI2C7c81CNxqMe8_vOBl-ccnBT-ED7ydj9p3Cr585lmySfxfAHzs7LdX1NFn1sZOiI1B2HdoDXg0fIb9KNcs3thXrfZO_6ru-UaAljNHmmd7_302i23LLXSsDp2M" }
		};
		var headerDictionary = new HeaderDictionary(headers);
		var requestFeature = new HttpRequestFeature()
		{
			Headers = headerDictionary,
			Scheme = "https",
			Path = "/api/test/check",
			Method = "POST",
			PathBase = "localhost:44331",
			Body = new MemoryStream(Encoding.ASCII.GetBytes(postBody))
		};
		var features = new FeatureCollection();
		features.Set<IHttpRequestFeature>(requestFeature);

		var responseFeature = new HttpResponseFeature()
		{
			StatusCode = responseStatusCode
		};

		switch (responseFeature.StatusCode)
		{
			case 400:
				responseFeature.Body = new MemoryStream(Encoding.ASCII.GetBytes("{ \"invalidParams\": [ { \"name\": \"fields[0]\", \"code\": \"fields\", \"reason\": \"Parameter bevat een niet bestaande veldnaam.\" } ], \"type\": \"https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1\", \"title\": \"Een of meerdere parameters zijn niet correct.\", \"status\": 400, \"detail\": \"De foutieve parameter(s) zijn: fields[0].\", \"instance\": \"/api/test/check\", \"code\": \"paramsValidation\" }"));
				break;
			case 401:
				responseFeature.Body = new MemoryStream(Encoding.ASCII.GetBytes("{ \"type\": \"https://datatracker.ietf.org/doc/html/rfc7235#section-3.1\", \"title\": \"Niet correct geauthenticeerd.\", \"status\": 401, \"instance\": \"/api/test/check\", \"code\": \"authentication\" }"));
				break;
			case 403:
				responseFeature.Body = new MemoryStream(Encoding.ASCII.GetBytes("{ \"type\": \"https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3\", \"title\": \"U bent niet geautoriseerd voor het gebruik van deze API.\", \"status\": 403, \"instance\": \"/api/test/check\", \"code\": \"unauthorized\" }"));
				break;
			case 200:
			default:
				// No logging of response in 200. This comment is here explicitly to document this requirement.
				//responseFeature.Body = new MemoryStream(Encoding.ASCII.GetBytes("{ \"personen\": [ { \"burgerservicenummer\": \"999994669\" } ], \"type\": \"RaadpleegMetBurgerservicenummer\" }"))
				break;
		}

		features.Set<IHttpResponseFeature>(responseFeature);

		var context = new DefaultHttpContext(features);
		if (!string.IsNullOrWhiteSpace(userName))
		{
			context.User = new ClaimsPrincipal(new GenericIdentity(userName, "OpenIdConnectAuthentication"));
		}
		context.Request.ContentType = "application/json";
		context.Items ??= new Dictionary<object, object?>();
		if (serializeResponseAsString)
		{
			context.Items.Add("RequestBodySerialized", postBody);
		}

		return context;
	}

	public static TestRaadpleegMetBurgerservicenummer CreateDefaultRvigRequest()
	{
		return new TestRaadpleegMetBurgerservicenummer
		{
			burgerservicenummer = new List<string> { "999994669" },
			fields = new List<string> { "burgerservicenummer" },
			type = nameof(TestRaadpleegMetBurgerservicenummer)
		};
	}

	/// <summary>
	/// Makes it possible to create a mock of a repository to fake data to use during unit testing of helpers and services.
	/// </summary>
	/// <typeparam name="T">Type of repo instance. For example DbDomeinTabellenRepo.</typeparam>
	/// <param name="setupActions">
	///		List of actions that require mocked results in the repo mock.
	///		For example a valid value would be:
	///		var setupActions = new List<(Expression<Func<DbDomeinTabellenRepo, object?>> expression, object? something)>
	/// 	{
	/// 		// Here _ is the repo instance and GetGemeenteNaam is the method that must be run whilst null is the expected mocked result.
	/// 		(_ => _.GetGemeenteNaam(gemeenteVanInschrijving), null)
	///
	///			The GetGemeenteNaam method must be a virtual (overridable) method for this to work..
	/// 	};
	/// </param>
	/// <returns></returns>
	public static T GetRepoMock<T>(List<(Expression<Func<T, object?>> expression, object? expectedResult)>? setupActions = null) where T : PostgresRepoBase
	{
		var repository = new Mock<T>(Options.Create(new DatabaseOptions()), new LoggingHelper(MockIHttpContextAccessor()));
		setupActions?.ForEach(action => repository.Setup(action.expression).Returns(action.expectedResult));
		return repository.Object;
	}

	public static LoggingHelper ArrangeLogger(string logNameAccessLog, string logNameApplicationLog, string logNameTraceLog, string logFolderName, int statusCode = 200, string? logLevel = "Debug", IHttpContextAccessor? httpContextAccessor = null)
	{
		var loggingHelper = new LoggingHelper(httpContextAccessor ?? MockIHttpContextAccessor(statusCode));
		var builder = WebApplication.CreateBuilder();
		builder.Configuration["Serilog:LogFilePath"] = logFolderName;
		builder.Configuration["Serilog:MinimumLevel:Default"] = logLevel;
		if (Directory.Exists($"./{logFolderName}"))
		{
			Directory.Delete($"./{logFolderName}", true);
		}
		LoggingInitializer.SetupLogging("2.0.0", builder, logNameAccessLog, logNameApplicationLog, logNameTraceLog);

		return loggingHelper;
	}
}
