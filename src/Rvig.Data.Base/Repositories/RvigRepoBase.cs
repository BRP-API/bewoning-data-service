using Rvig.HaalCentraalApi.Shared.Helpers;

namespace Rvig.Data.Base.Repositories;

public abstract class RvigRepoBase<T> where T : class, new()
{
	protected RvigRepoBase(ILoggingHelper loggingHelper)
	{
	}
}