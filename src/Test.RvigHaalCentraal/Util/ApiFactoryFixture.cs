//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.AspNetCore.TestHost;
//using Microsoft.Extensions.DependencyInjection;
//using Rvig.BrpApi.Shared.Interfaces;
//using Rvig.HaalCentraalApi;
//using Rvig.BrpApi.Shared.ApiModels.BRP;
//using Rvig.BrpApi.Shared.Options;
//using Rvig.BrpApi.Shared.RepoModels;

//namespace Test.Rvig.HaalCentraalApi.Util;

//#pragma warning disable S3881 // "IDisposable" should be implemented correctly
//public class ApiFactoryFixture : IDisposable
//#pragma warning restore S3881 // "IDisposable" should be implemented correctly
//{
//	public readonly WebApplicationFactory<Startup> WebApplicationFactory;

//	public ApiFactoryFixture()
//	{
//		var getAndMapMock = new Mock<IGetAndMapGbaPersonenService>();
//		getAndMapMock.Setup(x => x.GetPersonenMapByBsns(It.IsAny<IEnumerable<string>>(), It.IsAny<string>(), It.IsAny<List<string>>())).Returns((string value) => value switch
//		{
//			"999994669" => Task.FromResult<IEnumerable<(GbaPersoon, long)>?>(
//				new List<(GbaPersoon, long)>
//				{
//					(new GbaPersoon
//					{
//						Burgerservicenummer = "999994669",
//						Naam = new()
//						{
//							Geslachtsnaam = "Mock"
//						}
//					},
//					0)
//				}
//			 ),
//			_ => Task.FromResult<IEnumerable<(GbaPersoon, long)>?>(null),
//		});
//		//getAndMapMock.Setup(x => x.GetMapBySearchModel(It.IsAny<RepoPersoonSearchModel>())).Returns(Task.FromResult(
//		//new GbaPersoonBeperkt
//		//{
//		//	Burgerservicenummer = "999994669",
//		//	Naam = new() { Geslachtsnaam = "Mock" }
//		//}));

//		var domeinTabellenRepoMock = new Mock<IDomeinTabellenRepo>();
//		domeinTabellenRepoMock.Setup(x => x.GetGemeenteNaam(It.IsAny<long>())).Returns((long value) => value switch
//		{
//			503 => Task.FromResult<string?>("Delft"),
//			599 => Task.FromResult<string?>("Rotterdam"),
//			14 => Task.FromResult<string?>("Groningen"),
//			_ => Task.FromResult<string?>(null),
//		});

//		domeinTabellenRepoMock.Setup(x => x.VoorvoegselExist(It.IsAny<string>())).Returns((string value) => value switch
//		{
//			"\'S" => Task.FromResult(true),
//			_ => Task.FromResult(false)
//		});

//		WebApplicationFactory = new WebApplicationFactory<Startup>()
//			.WithWebHostBuilder(builder =>
//			{
//				builder.ConfigureTestServices(services =>
//				{
//					services.Configure<HaalcentraalApiOptions>(opts => opts.BrpHostName = "{brphost}");

//					services.AddSingleton(getAndMapMock.Object);
//					services.AddSingleton(domeinTabellenRepoMock.Object);

//					services.AddAuthentication("Test")
//					.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
//						"Test", _ => { });
//					services.AddAuthorization(options => options.DefaultPolicy = new AuthorizationPolicyBuilder("Test").RequireAuthenticatedUser().Build());
//				});
//			});
//	}

//	public void Dispose()
//	{
//		// Method intentionally left empty?
//	}
//}
