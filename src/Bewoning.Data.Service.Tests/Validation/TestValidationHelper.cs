using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using Bewoning.Data.Service.Tests.Util;
using Bewoning.Api.Validation;
using Bewoning.Api.Exceptions;
using Bewoning.Data.Repositories;

namespace Bewoning.Data.Service.Tests.Validation
{
    [TestClass]
    public class TestValidationHelper
    {
        [TestMethod]
        public async Task TestValidateGemeenteVanInschrijvingNonNumeric()
        {
            // Arrange
            var gemeenteVanInschrijving = "This isn't a municipality code.";
            await DbDomeinTabellenRepoTestMethodBase(gemeenteVanInschrijving, ValidationHelperBase.ValidateGemeenteInschrijving);
        }

        [TestMethod]
        public async Task TestValidateUnknownGemeenteVanInschrijving()
        {
            // Arrange
            const long gemeenteVanInschrijving = 4949;
            var setupActions = new List<(Expression<Func<DbDomeinTabellenRepo, object?>> expression, object? expectedResult)>
            {
                (_ => _.GetGemeenteNaam(gemeenteVanInschrijving), Task.FromResult<string?>(null))
            };
            await DbDomeinTabellenRepoTestMethodBase(gemeenteVanInschrijving.ToString(), ValidationHelperBase.ValidateGemeenteInschrijving, setupActions);
        }

        [TestMethod]
        public async Task TestValidateTooLongGemeenteVanInschrijving()
        {
            // Arrange
            const string gemeenteVanInschrijving = "934298342879023";
            await DbDomeinTabellenRepoTestMethodBase(gemeenteVanInschrijving, ValidationHelperBase.ValidateGemeenteInschrijving);
        }

        [TestMethod]
        public async Task TestValidateTooShortGemeenteVanInschrijving()
        {
            // Arrange
            const string gemeenteVanInschrijving = "123";
            await DbDomeinTabellenRepoTestMethodBase(gemeenteVanInschrijving, ValidationHelperBase.ValidateGemeenteInschrijving);
        }

        [TestMethod]
        public async Task TestValidateZeroGemeenteVanInschrijving()
        {
            // Arrange
            const string gemeenteVanInschrijving = "0000";
            await DbDomeinTabellenRepoTestMethodBase(gemeenteVanInschrijving, ValidationHelperBase.ValidateGemeenteInschrijving);
        }

        [TestMethod]
        public async Task TestInvalidVoorvoegsel()
        {
            // Arrange
            const string voorvoegsel = "123";
            var setupActions = new List<(Expression<Func<DbDomeinTabellenRepo, object?>> expression, object? expectedResult)>
            {
                (_ => _.VoorvoegselExist(voorvoegsel), Task.FromResult(false))
            };

            await DbDomeinTabellenRepoTestMethodBase(voorvoegsel, ValidationHelperBase.ValidateVoorvoegsel, setupActions);
        }

        [TestMethod]
        public void TestInvalidWildcareQuestionMarkUsage()
        {
            // Arrange
            const string value = "jans?sen";

            // Act // Assert
            Assert.ThrowsException<InvalidParamsException>(() => ValidationHelperBase.IsWildcardCorrectlyUsed(value, "geslachtsnaam"));
        }

        [TestMethod]
        public void TestInvalidWildcareAsterixUsage()
        {
            // Arrange
            const string value = "jans*sen";

            // Act // Assert
            Assert.ThrowsException<InvalidParamsException>(() => ValidationHelperBase.IsWildcardCorrectlyUsed(value, "geslachtsnaam"));
        }

        [TestMethod]
        public void TestValidateBurgerservicenummersWithListContainingInvalidBsn()
        {
            // Arrange
            var bsns = new List<string>
            {
                "invalid_bsn"
            };

            // Act // Assert
            Assert.ThrowsException<InvalidParamsException>(() => ValidationHelperBase.ValidateBurgerservicenummers(bsns));
        }

        private static async Task DbDomeinTabellenRepoTestMethodBase(string? searchValue, Func<string?, DbDomeinTabellenRepo, Task> validateFunc, List<(Expression<Func<DbDomeinTabellenRepo, object?>> expression, object? expectedResult)>? setupActions = null)
        {
            DbDomeinTabellenRepo mockRepo = CreateMockRepo(setupActions);

            // Act // Assert
            await Assert.ThrowsExceptionAsync<InvalidParamsException>(() => validateFunc(searchValue, mockRepo));
        }

        private static DbDomeinTabellenRepo CreateMockRepo(List<(Expression<Func<DbDomeinTabellenRepo, object?>> expression, object? expectedResult)>? setupActions = null)
        {
            DbDomeinTabellenRepo mockRepo;
            if (setupActions?.Any() == true)
            {
                mockRepo = MockHelper.GetRepoMock(setupActions);
            }
            else
            {
                // No setup actions required as it is expected that the validation in ValidateGemeenteInschrijving will stop the process purely based on the given long input.
                mockRepo = MockHelper.GetRepoMock<DbDomeinTabellenRepo>();
            }

            return mockRepo;
        }
    }
}
