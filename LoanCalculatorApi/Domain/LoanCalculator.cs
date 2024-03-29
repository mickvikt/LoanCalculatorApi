﻿namespace LoanCalculatorApi.Domain
{
    using System;

    public class LoanCalculator : ILoanCalculator
    {
        public PaymentOverviewResult GetLoanPaymentOverview(LoanTerms loanTerms)
        {
            if (loanTerms == null)
            {
                return PaymentOverviewResult.CreateFailureResult("Loan terms parameter is not given!");
            }

            var monthlyInterestRate = loanTerms.YearlyInterestPercentage / 100 / 12;
            var determinant = Math.Pow(1 + monthlyInterestRate, loanTerms.LoanDurationInMonths);
            var monthlyAmount = loanTerms.LoanAmount * monthlyInterestRate * determinant / (determinant - 1);
            var totalInterestPaid = (monthlyAmount * loanTerms.LoanDurationInMonths) - loanTerms.LoanAmount;

            var paymentOverview = new PaymentOverview
            {
                YearlyCostAsPercentageOfLoanAmount = Math.Round(monthlyAmount * 12 / loanTerms.LoanAmount * 100, 2),
                MonthlyAmount = Math.Round(monthlyAmount, 2),
                TotalInterestPaid = Math.Round(totalInterestPaid, 2),
                TotalAmountPaidInAdministrativeFees = Math.Min(
                    loanTerms.AdministrativeFeePercentage / 100 * loanTerms.LoanAmount,
                    loanTerms.MinimumAdministrativeFee),
            };

            return PaymentOverviewResult.CreateSuccessResult(paymentOverview);
        }
    }
}