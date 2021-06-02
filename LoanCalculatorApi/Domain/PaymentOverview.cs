namespace LoanCalculatorApi.Domain
{
    public class PaymentOverview
    {
        public double MonthlyAmount { get; set; }

        public double TotalInterestPaid { get; set; }

        public double TotalAmountPaidInAdministrativeFees { get; set; }

        public double YearlyCostAsPercentageOfLoanAmount { get; set; }
    }
}