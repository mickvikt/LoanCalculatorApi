namespace LoanCalculatorApi.Domain
{
    public class LoanTerms
    {
        public int LoanAmount { get; set; }

        public double YearlyInterestPercentage { get; set; }

        public int LoanDurationInMonths { get; set; }

        public double AdministrativeFeePercentage { get; set; } = 1;

        public int MinimumAdministrativeFee { get; set; } = 10_000;
    }
}