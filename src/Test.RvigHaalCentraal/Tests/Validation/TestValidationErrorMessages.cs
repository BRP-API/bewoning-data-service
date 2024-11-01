//using Rvig.BrpApi.Shared.Exceptions;
//using Rvig.BrpApi.Shared.Validation;

//namespace Test.Rvig.HaalCentraalApi.DeveloperTests
//{
//    public class TestValidationErrorMessages
//    {
//        [Theory]
//        [InlineData("random", null)]
//        [InlineData("Waarde is langer dan maximale lengte 16.", "maxLength")]
//        public void GetInvalidParamCode(string errorMessage, string expectedCode)
//        {
//            var result = ValidationErrorMessages.GetInvalidParamCode(errorMessage)?.ToString();

//            result.Should().Be(expectedCode);
//        }

//        [Theory]
//        [InlineData(null, null)]
//        [InlineData("_enum", "Waarde heeft geen geldige waarde uit de enumeratie.")]
//        public void GetParseErrorMessage(string enumString, string expectedError)
//        {
//            var enumCode = (InvalidParamCode?)(enumString != null ? Enum.Parse(typeof(InvalidParamCode), enumString) : null);
//            var result = ValidationErrorMessages.GetParseErrorMessage(enumCode)?.ToString();

//            result.Should().Be(expectedError);
//        }
//    }
//}
