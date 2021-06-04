namespace LoanCalculatorApi
{
    using LoanCalculatorApi.Domain;

    public class PaymentOverviewResult
    {
        private PaymentOverviewResult()
        {
        }

        public bool IsSuccess { get; set; }

        public bool IsFailure => !this.IsSuccess;

        public string ErrorMessage { get; set; }

        public PaymentOverview PaymentOverview { get; set; }

        public static PaymentOverviewResult CreateSuccessResult(PaymentOverview paymentOverview)
            => new PaymentOverviewResult
            {
                PaymentOverview = paymentOverview,
                IsSuccess = true,
            };

        public static PaymentOverviewResult CreateFailureResult(string errorMessage)
            => new PaymentOverviewResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
            };
    }
}