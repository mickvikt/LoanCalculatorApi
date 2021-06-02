namespace LoanCalculatorApi.Domain
{
    public interface ILoanCalculator
    {
        PaymentOverview GetLoanPaymentOverview(LoanTerms loanTerms);
    }
}