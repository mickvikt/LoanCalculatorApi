namespace LoanCalculatorApi.Domain
{
    using FluentValidation.Results;

    public interface ILoanTermsValidator
    {
        ValidationResult Validate(LoanTerms instance);
    }
}