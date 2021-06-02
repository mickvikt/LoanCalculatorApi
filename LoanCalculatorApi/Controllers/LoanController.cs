namespace LoanCalculatorApi.Controllers
{
    using System.ComponentModel;
    using LoanCalculatorApi.Domain;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanCalculator loanCalculator;

        public LoanController(ILoanCalculator loanCalculator)
        {
            this.loanCalculator = loanCalculator;
        }

        [HttpPost]
        public IActionResult Post(
            [DefaultValue(25_000)] int loanAmount,
            [DefaultValue(5.0)]double yearlyInterestPercentage,
            [DefaultValue(12)] int loanDurationInMOnths,
            [DefaultValue(1)]double administrativeFeePercentage,
            [DefaultValue(5000)]int minimumAdministrativeFee)
        {
            var loanTerms = new LoanTerms
            {
                LoanAmount = loanAmount,
                YearlyInterestPercentage = yearlyInterestPercentage,
                LoanDurationInMonths = loanDurationInMOnths,
                AdministrativeFeePercentage = 1,
                MinimumAdministrativeFee = 1000,
            };

            var loanTermsValidator = new LoanTermsValidator();
            var validationResult = loanTermsValidator.Validate(loanTerms);

            if (!validationResult.IsValid)
            {
                var errorsAsString = string.Join(";\n", validationResult.Errors);
                return this.BadRequest(errorsAsString);
            }

            return this.Ok(this.loanCalculator.GetLoanPaymentOverview(loanTerms));
        }
    }
}