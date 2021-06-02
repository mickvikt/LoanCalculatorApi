namespace LoanCalculatorApi
{
    using FluentValidation;

    public class LoanTermsValidator : AbstractValidator<LoanTerms>
    {
        public LoanTermsValidator()
        {
            this.RuleFor(terms => terms.LoanAmount).GreaterThan(0);
            this.RuleFor(terms => terms.YearlyInterestPercentage).GreaterThan(0);
            this.RuleFor(terms => terms.YearlyInterestPercentage).GreaterThan(0);
        }
    }
}