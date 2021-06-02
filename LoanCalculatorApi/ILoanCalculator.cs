namespace LoanCalculatorApi
{
    public interface ILoanCalculator
    {
        PaymentOverview GetLoanPaymentOverview(LoanTerms loanTerms);
    }
}