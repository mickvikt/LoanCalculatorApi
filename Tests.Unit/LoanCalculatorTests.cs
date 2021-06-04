using FluentAssertions;
using LoanCalculatorApi;
using LoanCalculatorApi.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unit
{
    [TestClass]
    public class LoanCalculatorTests
    {
        private LoanCalculator loanCalculator;

        public LoanCalculatorTests()
        {
            loanCalculator = new LoanCalculator();
        }
        
        [TestMethod]
        public void GivenValidLoanTermsShouldPopulatePaymentOverviewCorrectly()
        {
            // Arrange
            var loanTerms = new LoanTerms
            {
                LoanAmount = 500_000,
                YearlyInterestPercentage = 5,
                LoanDurationInMonths = 120,
                AdministrativeFeePercentage = 1,
                MinimumAdministrativeFee = 10_000,
            };

            var expectedResult = PaymentOverviewResult.CreateSuccessResult(
                new PaymentOverview
                {
                    YearlyCostAsPercentageOfLoanAmount = 12.73,
                    MonthlyAmount = 5_303.28,
                    TotalInterestPaid = 136_393.09,
                    TotalAmountPaidInAdministrativeFees = 5_000,
                }
            );
            
            // Act
            var actualResult = this.loanCalculator.GetLoanPaymentOverview(loanTerms);
            
            // Assert
            actualResult
                .Should()
                .BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public void GivenANullValueForLoanTermsShouldProduceAFailureResult()
        {
            // Arrange
            var expectedResult = PaymentOverviewResult.CreateFailureResult("Loan terms parameter is not given!");
            
            // Act
            var actualResult = this.loanCalculator.GetLoanPaymentOverview(null);
            
            // Assert
            actualResult
                .Should()
                .BeEquivalentTo(expectedResult);
        }
    }
}