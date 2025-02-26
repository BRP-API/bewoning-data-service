using Microsoft.AspNetCore.Http;
using Rvig.BrpApi.Bewoningen.Exceptions;
using Rvig.BrpApi.Bewoningen.Interfaces;
using Rvig.Data.Bewoningen.DatabaseModels;
using Rvig.Data.Bewoningen.Helpers;
using Rvig.Data.Bewoningen.Repositories;

namespace Rvig.Data.Bewoningen.Services
{
    public class GetAndMapGbaServiceBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IProtocolleringService _protocolleringService;
        protected readonly IAutorisationRepo _autorisationRepo;

        public GetAndMapGbaServiceBase(IHttpContextAccessor httpContextAccessor, IAutorisationRepo autorisationRepo, IProtocolleringService protocolleringService)
        {
            _httpContextAccessor = httpContextAccessor;
            _autorisationRepo = autorisationRepo;
            _protocolleringService = protocolleringService;
        }

        /// <summary>
        /// Get afnemer and autorisatie and validate autorisatie.
        /// </summary>
        /// <exception cref="AuthorizationException"></exception>
        protected async Task<(Afnemer, DbAutorisatie)> GetAfnemerAutorisatie()
        {
            var afnemer = GetAfnemer();
            var autorisatie = await _autorisationRepo.GetByAfnemerCode(afnemer.Afnemerscode);

            if (autorisatie == null || autorisatie.ad_hoc_medium != "A" && autorisatie.ad_hoc_medium != "N")
            {
                throw new UnauthorizedException("Niet geautoriseerd voor ad hoc bevragingen.");
            }

            return (afnemer, autorisatie);
        }

        /// <summary>
        /// Get afnemer.
        /// </summary>
        /// <exception cref="AuthorizationException"></exception>
        protected Afnemer GetAfnemer() => AfnemerHelper.GetAfnemerInfoFromAuthenticatedUser(_httpContextAccessor);
    }
}