using Microsoft.AspNetCore.Mvc.Filters;
using Rvig.BrpApi.Bewoningen.Exceptions;

namespace Rvig.BrpApi.Bewoningen.Validation;
public class ValidateQueryParamsRequiredAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Query.Count == 0)
        {
            throw new ParamsRequiredException();
        }

        base.OnActionExecuting(context);
    }
}