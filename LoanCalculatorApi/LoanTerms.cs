using System.ComponentModel;

namespace LoanCalculatorApi
{
    using FluentValidation;

    public class LoanTerms
    {
        [DefaultValue(25_000)]
        public int LoanAmount { get; set; }

        public double YearlyInterestPercentage { get; set; }

        public int LoanDurationInMonths { get; set; }

        public double AdministrativeFeePercentage { get; set; } = 1;

        public int MinimumAdministrativeFee { get; set; } = 10_000;
    }

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