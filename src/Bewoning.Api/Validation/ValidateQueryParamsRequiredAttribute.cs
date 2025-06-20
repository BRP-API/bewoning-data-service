using Bewoning.Api.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bewoning.Api.Validation;
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