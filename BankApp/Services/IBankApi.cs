using BankApp.Models;
using Refit;

namespace BankApp.Services
{
    public interface IBankApi
    {
        [Get("/PaymentInitialise")]
        Task<ApiResponse<PaymentInitResponse>> GetBeneficiaries([Header("UserKey")] string userKey);

        [Post("/PaymentReview")]
        Task<ApiResponse<PaymentReviewResponse>> SubmitPaymentReview(
            [Body] PaymentReviewRequest request);

        [Post("/PaymentExecute")]
        Task<ApiResponse<PaymentExecuteResponse>> ExecutePayment(
            [Body] PaymentExecuteRequest request);
    }
}
