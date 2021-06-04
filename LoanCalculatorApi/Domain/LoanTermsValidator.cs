namespace LoanCalculatorApi.Domain
{
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;

    public class LoanTermsValidator : AbstractValidator<LoanTerms>, ILoanTermsValidator
    {
        [ExcludeFromCodeCoverage]
        public LoanTermsValidator()
        {
            this.RuleFor(terms => terms.LoanAmount).GreaterThan(0);
            this.RuleFor(terms => terms.YearlyInterestPercentage).GreaterThan(0);
            this.RuleFor(terms => terms.LoanDurationInMonths).GreaterThan(0);
        }
    }
}