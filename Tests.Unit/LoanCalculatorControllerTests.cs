using FluentAssertions;
using FluentValidation.Results;
using LoanCalculatorApi;
using LoanCalculatorApi.Controllers;
using LoanCalculatorApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.Unit
{
    [TestClass]
    public class LoanCalculatorControllerTests
    {
        private readonly Mock<ILoanCalculator> loanCalculator;
        private readonly Mock<ILoanTermsValidator> loanTermsValidator;
        
        private LoanCalculatorController controller;
        public LoanCalculatorControllerTests()
        {
            this.loanCalculator = new Mock<ILoanCalculator>();
            this.loanTermsValidator = new Mock<ILoanTermsValidator>();
            
            this.controller = new LoanCalculatorController(loanCalculator.Object, loanTermsValidator.Object);
        }

        [TestMethod]
        public void GivenValidParametersShouldReturnHttpOk()
        {
            // Arrange
            var mockedPaymentOverview = new PaymentOverview
            {
                MonthlyAmount = 1234,
                TotalInterestPaid = 454545,
                TotalAmountPaidInAdministrativeFees = 455,
                YearlyCostAsPercentageOfLoanAmount = 1                
            };
            
            this.loanCalculator
                .Setup(mock => mock.GetLoanPaymentOverview(It.IsAny<LoanTerms>()))
                .Returns(PaymentOverviewResult.CreateSuccessResult(mockedPaymentOverview));

            this.loanTermsValidator
                .Setup(mock => mock.Validate(It.IsAny<LoanTerms>()))
                .Returns(new ValidationResult());

            // Act
            var result = controller.GetPaymentOverview(
                12000,
                4.3,
                12,
                1,
                1000);
            
            // Assert
            result
                .Should()
                .BeOfType<OkObjectResult>();

            ((OkObjectResult)result)
                .Value
                .Should()
                .Be(mockedPaymentOverview);
        }

        [TestMethod]
        public void GivenInvalidParameterSetShouldReturnValidationFailed()
        {
            // Arrange
            this.loanTermsValidator
                .Setup(mock => mock.Validate(It.IsAny<LoanTerms>()))
                .Returns(new ValidationResult(new[] {new ValidationFailure("Property", "Validation failed")}));
            
            // Act
            var result = controller.GetPaymentOverview(
                12000,
                4.3,
                12,
                1,
                1000);
            
            // Assert
            result
                .Should()
                .BeOfType<ObjectResult>();
            
            ((ObjectResult)result).Value
                .Should()
                .BeEquivalentTo(new ValidationProblemDetails
                {
                    Detail = "Validation failed",
                }, 
                    options => options.Excluding(o => o.Title));
        }

        [TestMethod]
        public void GivenLoanCalculatorReturnsFailureResultShouldReturnHttpBadRequest()
        {
            // Arrange
            var calculatorErrorMessage = "Some error";
            
            this.loanCalculator
                .Setup(mock => mock.GetLoanPaymentOverview(It.IsAny<LoanTerms>()))
                .Returns(PaymentOverviewResult.CreateFailureResult(calculatorErrorMessage));
            
            this.loanTermsValidator
                .Setup(mock => mock.Validate(It.IsAny<LoanTerms>()))
                .Returns(new ValidationResult());            
            
            // Act
            var result = controller.GetPaymentOverview(
                12000,
                4.3,
                12,
                1,
                1000);
            
            // Assert
            result
                .Should()
                .BeOfType<BadRequestObjectResult>();

            ((BadRequestObjectResult) result).Value
                .Should()
                .Be(calculatorErrorMessage);
        }
    }
}