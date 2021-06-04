namespace LoanCalculatorApi.Controllers
{
    using System.ComponentModel;
    using LoanCalculatorApi.Domain;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class LoanCalculatorController : ControllerBase
    {
        private readonly ILoanCalculator loanCalculator;
        private readonly ILoanTermsValidator loanTermsValidator;

        public LoanCalculatorController(
            ILoanCalculator loanCalculator,
            ILoanTermsValidator loanTermsValidator)
        {
            this.loanCalculator = loanCalculator;
            this.loanTermsValidator = loanTermsValidator;
        }

        [HttpPost("loan-payment-overview")]
        public IActionResult GetPaymentOverview(
            [DefaultValue(25_000)] int loanAmount,
            [DefaultValue(5.0)]double yearlyInterestPercentage,
            [DefaultValue(12)]int loanDurationInMonths,
            [DefaultValue(1)]double administrativeFeePercentage,
            [DefaultValue(5_000)]int minimumAdministrativeFee)
        {
            var loanTerms = new LoanTerms
            {
                LoanAmount = loanAmount,
                YearlyInterestPercentage = yearlyInterestPercentage,
                LoanDurationInMonths = loanDurationInMonths,
                AdministrativeFeePercentage = administrativeFeePercentage,
                MinimumAdministrativeFee = minimumAdministrativeFee,
            };

            var validationResult = this.loanTermsValidator.Validate(loanTerms);

            if (!validationResult.IsValid)
            {
                var errorsAsString = string.Join(";\n", validationResult.Errors);
                return this.ValidationProblem(errorsAsString);
            }

            var paymentOverviewResult = this.loanCalculator.GetLoanPaymentOverview(loanTerms);
            if (paymentOverviewResult.IsFailure)
            {
                return this.BadRequest(paymentOverviewResult.ErrorMessage);
            }

            return this.Ok(paymentOverviewResult.PaymentOverview);
        }
    }
}