using BankApp.Models;
using Refit;
namespace BankApp.Services;

public interface IBankApiService
{
        Task<ApiResponse<PaymentInitResponse>> GetBeneficiaries([Header("UserKey")] string userKey);
        Task<ApiResponse<PaymentReviewResponse>> SubmitPaymentReview([Body] PaymentReviewRequest request);
        Task<ApiResponse<PaymentExecuteResponse>> ExecutePayment([Body] PaymentExecuteRequest request);
}
