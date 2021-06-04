namespace LoanCalculatorApi.Domain
{
    public interface ILoanCalculator
    {
        PaymentOverviewResult GetLoanPaymentOverview(LoanTerms loanTerms);
    }
}