//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Abstractions;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.Extensions.Primitives;
//using Rvig.HaalCentraalApi.RequestModels;
//using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
//using Rvig.HaalCentraalApi.Shared.Exceptions;
//using Rvig.HaalCentraalApi.Shared.Validation;

//namespace Test.Rvig.HaalCentraalApi.DeveloperTests
//{
//    public class TestValidationAttributes
//    {
//        [Fact]
//        public void ValidateQueryParamsRequired()
//        {
//            var actionContext = new ActionContext(
//                new DefaultHttpContext(),
//                Mock.Of<RouteData>(),
//                Mock.Of<ActionDescriptor>()
//            );

//            var actionExecutingContext = new ActionExecutingContext(
//                actionContext,
//                new List<IFilterMetadata>(),
//                new Dictionary<string, object?>(),
//                Mock.Of<Controller>()
//            );

//            var attribute = new ValidateQueryParamsRequiredAttribute();


//            Action OnActionExecutingWithoutQuery = () => attribute.OnActionExecuting(actionExecutingContext);
//            OnActionExecutingWithoutQuery.Should().Throw<ParamsRequiredException>("because request does not have query params.");

//            actionExecutingContext.HttpContext.Request.Query = new QueryCollection(new Dictionary<string, StringValues> { { "paramName", "paramValue" } });
//            Action OnActionExecutingWithQuery = () => attribute.OnActionExecuting(actionExecutingContext);
//            OnActionExecutingWithQuery.Should().NotThrow<ParamsRequiredException>("because request does have query params.");
//        }

//        [Fact]
//        public void ValidateUnusableQueryParamsAttribute()
//        {
//            var actionContext = new ActionContext(
//                new DefaultHttpContext(),
//                Mock.Of<RouteData>(),
//                Mock.Of<ActionDescriptor>()
//            );

//            var actionExecutingContext = new ActionExecutingContext(
//                 actionContext,
//                 new List<IFilterMetadata>(),
//                 new Dictionary<string, object?> { { "model", new IngeschrevenPersoonRaadpleegModel() } },
//                 Mock.Of<Controller>()
//             );

//            var attribute = new ValidateUnusableQueryParamsAttribute();

//            Action OnActionExecutingWithoutQuery = () => attribute.OnActionExecuting(actionExecutingContext);
//            actionExecutingContext.HttpContext.Request.Query = new QueryCollection(new Dictionary<string, StringValues> { { "randomName", "randomValue" } });

//            var exception = OnActionExecutingWithoutQuery.Should().Throw<InvalidParamsException>("because request does have an unknown query param.").Which;
//            exception.InvalidParams.Should().BeEquivalentTo(new List<InvalidParams> { new InvalidParams
//            {
//                Name = "randomName",
//                Code = InvalidParamCode.unknownParam.ToString(),
//                Reason = ValidationErrorMessages.UnexpectedParam
//            }});

//            actionExecutingContext.HttpContext.Request.Query = new QueryCollection(new Dictionary<string, StringValues> { { "burgerservicenummer", "12345678" } });
//            Action OnActionExecutingWithQuery = () => attribute.OnActionExecuting(actionExecutingContext);

//            OnActionExecutingWithQuery.Should().NotThrow<InvalidParamsException>("because request does have a known query params.");
//        }
//    }
//}
