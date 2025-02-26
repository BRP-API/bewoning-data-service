using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;
using Bewoning.Data.Service.Tests.Util;
using Bewoning.Api.Options;
using Bewoning.Api.Exceptions;
using Bewoning.Data.Helpers;

namespace Bewoning.Data.Service.Tests.Api
{
    [TestClass]
    public class TestAfnemerHelper
    {
        [TestMethod]
        public void TestGetAfnemerInfoFromBasicAuthenticatedUser()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = SetupTest("basic");

            // Act
            var afnemer = AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(httpContextAccessor);

            // Assert
            afnemer.Should().NotBeNull();
            afnemer.Gemeentecode.Should().BeNull();
            afnemer.Afnemerscode.Should().Be(990008);
        }

        [TestMethod]
        public void TestGetAfnemerInfoFromBasicAuthenticatedUserWithUsernameWithPipe()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = SetupTest("basic", "990008|0518");

            // Act
            var afnemer = AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(httpContextAccessor);

            // Assert
            afnemer.Should().NotBeNull();
            afnemer.Gemeentecode.Should().Be(518);
            afnemer.Afnemerscode.Should().Be(990008);
        }

        [TestMethod]
        public void TestGetAfnemerInfoFromUsernameWithInvalidUsername()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = SetupTest("basic", "invalidusername");

            // Act / Assert
            Assert.ThrowsException<AuthenticationException>(() => AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(httpContextAccessor));
        }

        [TestMethod]
        public void TestGetAfnemerInfoFromJwtBearerAuthenticatedUser()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = SetupTest("jwtbearer");

            // Act
            var afnemer = AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(httpContextAccessor);

            // Assert
            afnemer.Should().NotBeNull();
            afnemer.Gemeentecode.Should().BeNull();
            afnemer.Afnemerscode.Should().Be(990008);
        }

        [TestMethod]
        public void TestGetAfnemerInfoFromJwtBearerAuthenticatedUserWithGemeenteCodeClaim()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = SetupTest("jwtbearer");
            ((GenericIdentity)httpContextAccessor.HttpContext!.User!.Identity!).AddClaim(new Claim("claims", "gemeenteCode=0518"));

            // Act
            var afnemer = AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(httpContextAccessor);

            // Assert
            afnemer.Should().NotBeNull();
            afnemer.Gemeentecode.Should().Be(518);
            afnemer.Afnemerscode.Should().Be(990008);
        }

        [TestMethod]
        public void TestGetAfnemerInfoWithUnknownAuthenticationType()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = SetupTest();

            // Act / Assert
            Assert.ThrowsException<AuthenticationException>(() => AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(httpContextAccessor));
        }

        private static IHttpContextAccessor SetupTest(string? authenticationType = null, string? userName = "990008")
        {
            var httpContextAccessor = MockHelper.MockIHttpContextAccessor(userName: userName);
            var builder = WebApplication.CreateBuilder();
            if (!string.IsNullOrWhiteSpace(authenticationType))
            {
                builder.Configuration["AuthenticationTypes"] = authenticationType;
                AppSettingsManager.Configuration = builder.Configuration;
            }
            else
            {
                AppSettingsManager.Configuration = null;
            }
            return httpContextAccessor;
        }
    }
}
