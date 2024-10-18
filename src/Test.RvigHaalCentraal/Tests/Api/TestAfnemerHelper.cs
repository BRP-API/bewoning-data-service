using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Rvig.Data.Base.Helpers;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Options;
using System;
using System.Security.Claims;
using System.Security.Principal;
using Test.RvigHaalCentraal.Util;

namespace Test.RvigHaalCentraal.Tests.Api
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

		// [TestMethod]
		// public void TestGetAfnemerInfoFromUsernameWithoutUsername()
		// {
		// 	// Arrange
		// 	IHttpContextAccessor httpContextAccessor = SetupTest("basic", null);

		// 	// Act / Assert
		// 	Assert.ThrowsException<ArgumentException>(() => AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(httpContextAccessor));
		// }

		[TestMethod]
		public void TestGetAfnemerInfoFromUsernameWithInvalidUsername()
		{
			// Arrange
			IHttpContextAccessor httpContextAccessor = SetupTest("basic", "invalidusername");

			// Act / Assert
			Assert.ThrowsException<AuthenticationException>(() => AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(httpContextAccessor));
		}

		// [TestMethod]
		// public void TestGetAfnemerInfoFromBasicUsernameWithInvalidGemeenteCode()
		// {
		// 	// Arrange
		// 	IHttpContextAccessor httpContextAccessor = SetupTest("basic", "990008|invalidGemCode");

		// 	// Act / Assert
		// 	Assert.ThrowsException<ArgumentException>(() => AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(httpContextAccessor));
		// }

		// [TestMethod]
		// public void TestGetAfnemerInfoFromJwtBearerUsernameWithInvalidGemeenteCode()
		// {
		// 	// Arrange
		// 	IHttpContextAccessor httpContextAccessor = SetupTest("jwtbearer");
		// 	((GenericIdentity)httpContextAccessor.HttpContext!.User!.Identity!).AddClaim(new Claim("claims", "gemeenteCode=invalidGemCode"));

		// 	// Act / Assert
		// 	Assert.ThrowsException<ArgumentException>(() => AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(httpContextAccessor));
		// }

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
