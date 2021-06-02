namespace LoanCalculatorApi
{
    using System;

    public class LoanCalculator : ILoanCalculator
    {
        public PaymentOverview GetLoanPaymentOverview(LoanTerms loanTerms)
        {
            var monthlyInterestRate = loanTerms.YearlyInterestPercentage / 100 / 12;
            var determinant = Math.Pow(1 + monthlyInterestRate, loanTerms.LoanDurationInMonths);
            var monthlyAmount = loanTerms.LoanAmount * monthlyInterestRate * determinant / (determinant - 1);
            var totalInterestPaid = (monthlyAmount * loanTerms.LoanDurationInMonths) - loanTerms.LoanAmount;

            return new PaymentOverview
            {
                YearlyCostAsPercentageOfLoanAmount = monthlyAmount * 12 / loanTerms.LoanAmount * 100,
                MonthlyAmount = Math.Round(monthlyAmount, 2),
                TotalInterestPaid = Math.Round(totalInterestPaid, 2),
                TotalAmountPaidInAdministrativeFees = Math.Min(
                    loanTerms.AdministrativeFeePercentage / 100 * loanTerms.LoanAmount,
                    loanTerms.MinimumAdministrativeFee),
            };
        }
    }
}