using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Bewoning.Data.Service.Tests.Util;
using Bewoning.Api.Helpers;
using Bewoning.Api.Services;
using Bewoning.Api.ApiModels.Bewoning;
using Bewoning.Api.RequestModels.Bewoning;
using Bewoning.Api.Options;
using Bewoning.Api.Interfaces;
using AutoMapper;

namespace Bewoning.Data.Service.Tests.Api;

public class TestGbaBewoningen
{

    private static GbaBewoningenApiService CreateServiceWithMockedDependencies(out Mock<IGetAndMapGbaBewoningenService> getAndMapBewoningenMock)
    {
        // Mock the dependencies for GetAndMapGbaBewoningenService
        getAndMapBewoningenMock = new Mock<IGetAndMapGbaBewoningenService>();
        var domeinTabellenRepoMock = new Mock<IDomeinTabellenRepo>();
        var protocolleringServiceMock = new Mock<IProtocolleringService>();
        var loggingHelperMock = new Mock<ILoggingHelper>();
        var filterServiceMock = new Mock<IFilterService>();
        var protocolleringAuthorizationOptionsMock = new Mock<IOptions<ProtocolleringAuthorizationOptions>>();
        var mapper = new Mock<IMapper>();

        return new GbaBewoningenApiService(
            getAndMapBewoningenMock.Object,
            domeinTabellenRepoMock.Object,
            protocolleringServiceMock.Object,
            loggingHelperMock.Object,
            protocolleringAuthorizationOptionsMock.Object,
            filterServiceMock.Object,
            mapper.Object
        );
    }

    [Fact]
    public async Task GetBewoningen_ShouldReturn_BewonerMetGeboorte()
    {
        // Arrange
        var service = CreateServiceWithMockedDependencies(out var getAndMapBewoningenMock);

        var bewoner = GbaBewonerFixture.CreateDefaultMetGeboorte("19900101");
        var adresseerbaarObjectIdentificatie = "00000_0000_0000_0001";
        var formattedObjectId = adresseerbaarObjectIdentificatie.Replace("_", "");
        var bewoning = GbaBewoningFixture.CreateDefaultWithAdresseerbaarObjectAndBewoner(formattedObjectId, bewoner);

        getAndMapBewoningenMock.Setup(x => x.GetBewoningen(It.IsAny<BewoningenQuery>()))
            .ReturnsAsync(new List<GbaBewoning> { bewoning });

        // Act
        var response = await service.GetBewoningen(new BewoningMetPeildatum
        {
            adresseerbaarObjectIdentificatie = formattedObjectId,
            peildatum = "20240101"
        });

        // Assert
        response.Should().NotBeNull();
        response.bewoningenResponse.Bewoningen.Should().HaveCount(1);
        response.bewoningenResponse?.Bewoningen?[0].Bewoners?[0].Geboorte?.Datum.Should().Be("19900101");
    }

    [Fact]
    public async Task GetBewoningen_ShouldReturn_BewonerMetNaam()
    {
        // Arrange
        var service = CreateServiceWithMockedDependencies(out var getAndMapBewoningenMock);
        var bewoner = GbaBewonerFixture.CreateDefaultMetNaam("John", "Doe");
        var adresseerbaarObjectIdentificatie = "00000_0000_0000_0001";
        var formattedObjectId = adresseerbaarObjectIdentificatie.Replace("_", "");
        var bewoning = GbaBewoningFixture.CreateDefaultWithAdresseerbaarObjectAndBewoner(formattedObjectId, bewoner);

        getAndMapBewoningenMock.Setup(x => x.GetBewoningen(It.IsAny<BewoningenQuery>()))
            .ReturnsAsync(new List<GbaBewoning> { bewoning });

        // Act
        var response = await service.GetBewoningen(new BewoningMetPeildatum
        {
            adresseerbaarObjectIdentificatie = formattedObjectId,
            peildatum = "20240101"
        });

        // Assert
        response.Should().NotBeNull();
        response.bewoningenResponse.Bewoningen.Should().HaveCount(1);
        var bewonerInResponse = response.bewoningenResponse.Bewoningen?[0].Bewoners?[0];
        bewonerInResponse?.Naam.Should().NotBeNull();
        bewonerInResponse?.Naam?.Voornamen.Should().Be("John");
        bewonerInResponse?.Naam?.Geslachtsnaam.Should().Be("Doe");
    }
}
