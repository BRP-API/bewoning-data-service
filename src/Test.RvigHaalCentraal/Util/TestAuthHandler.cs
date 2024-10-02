//using Microsoft.AspNetCore.Authentication;
////using Microsoft.Extensions.Options;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Text.Encodings.Web;

//namespace Test.Rvig.HaalCentraalApi.Util
//{
//    internal class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
//    {
//        public TestAuthHandler(
//            IOptionsMonitor<AuthenticationSchemeOptions> options,
//            ILoggerFactory logger,
//            UrlEncoder encoder,
//            ISystemClock clock
//        ) : base(options, logger, encoder, clock) { }

//        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
//        {
//            var authenticatedUser = new GenericIdentity("Test User", "Test");
//            var claimsPrincipal = new ClaimsPrincipal(authenticatedUser);

//            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
//        }
//    }
//}
