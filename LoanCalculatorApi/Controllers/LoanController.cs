namespace LoanCalculatorApi.Controllers
{
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
        public IActionResult Post(LoanTerms loanTerms)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Kazkokia suduva!");
            }

            return this.Ok(this.loanCalculator.GetLoanPaymentOverview(loanTerms));
        }
    }
}