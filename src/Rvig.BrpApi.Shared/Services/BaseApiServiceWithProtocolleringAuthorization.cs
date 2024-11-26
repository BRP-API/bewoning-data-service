using Microsoft.Extensions.Options;
using Rvig.BrpApi.Shared.Exceptions;
using Rvig.BrpApi.Shared.Helpers;
using Rvig.BrpApi.Shared.Interfaces;
using Rvig.BrpApi.Shared.Options;

namespace Rvig.BrpApi.Shared.Services
{
	public abstract class BaseApiServiceWithProtocolleringAuthorization : BaseApiService
	{
		protected readonly IOptions<ProtocolleringAuthorizationOptions> _protocolleringAuthorizationOptions;
		private readonly IProtocolleringService _protocolleringService;
		private readonly ILoggingHelper _loggingHelper;

		protected BaseApiServiceWithProtocolleringAuthorization(IDomeinTabellenRepo domeinTabellenRepo, IProtocolleringService protocolleringService, ILoggingHelper loggingHelper, IOptions<ProtocolleringAuthorizationOptions> protocolleringAuthorizationOptions) : base (domeinTabellenRepo)
		{
			_protocolleringService = protocolleringService;
			_loggingHelper = loggingHelper;
			_protocolleringAuthorizationOptions = protocolleringAuthorizationOptions;
		}

		protected async Task LogProtocolleringInDb(int afnemerCode, long? pl_id, List<string> searchedRubrieken, List<string> gevraagdeRubrieken)
		{
			try
			{
				_loggingHelper.LogDebug("Inserting protocollering.");
				// autorisatie is already validated in GetAfnemerAutorisatie as autorisatie.
				await _protocolleringService.Insert(afnemerCode, pl_id, string.Join(", ", searchedRubrieken.Distinct()), string.Join(", ", gevraagdeRubrieken.Distinct()));
				_loggingHelper.LogDebug("Inserted protocollering.");
			}
			catch (Exception e)
			{
				_loggingHelper.LogError("Failed to insert protocollering.");
				throw new CustomInvalidOperationException("Interne server fout.", new CustomInvalidOperationException("Protocollering is mislukt. Request is beëindigd.", e));
			}
		}

		protected async Task LogProtocolleringInDb(int afnemerCode, List<long>? pl_ids, List<string> searchedRubrieken, List<string> gevraagdeRubrieken)
		{
			if (pl_ids?.Any() == true)
			{
				pl_ids = pl_ids!.Where(pl_id => pl_id != 0).ToList();
				try
				{
					_loggingHelper.LogDebug("Inserting protocollering.");
					// autorisatie is already validated in GetAfnemerAutorisatie as autorisatie.
					if (pl_ids!.Count == 1)
					{
						await _protocolleringService.Insert(afnemerCode, pl_ids!.Single(), string.Join(", ", searchedRubrieken.Distinct()), string.Join(", ", gevraagdeRubrieken.Distinct()));
					}
					else
					{
						await _protocolleringService.Insert(afnemerCode, pl_ids!, string.Join(", ", searchedRubrieken.Distinct()), string.Join(", ", gevraagdeRubrieken.Distinct()));
					}
					_loggingHelper.LogDebug("Inserted protocollering.");
				}
				catch (Exception e)
				{
					_loggingHelper.LogError("Failed to insert protocollering.");
					throw new CustomInvalidOperationException("Interne server fout.", new CustomInvalidOperationException("Protocollering is mislukt. Request is beëindigd.", e));
				}
			}
		}
	}
}