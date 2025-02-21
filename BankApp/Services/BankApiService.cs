namespace BankApp.Services;
using System.Net;
using BankApp.Models;
using BankApp.Services.HttpService;
using Refit;

public class BankApiService : IBankApiService
{
    private readonly IHttpService _httpService;
    private readonly IConnectivity _connectivity;

    public BankApiService(IHttpService httpService, IConnectivity connectivity)
    {
        _httpService = httpService;
        _connectivity = connectivity;
    }

    public async Task<ApiResponse<PaymentInitResponse>> GetBeneficiaries(string userKey)
    {
        if (!IsInternetAvailable())
            return CreateNoInternetResponse<PaymentInitResponse>();

        try
        {
            var service = _httpService.Client;
            service.DefaultRequestHeaders.Add("UserKey", userKey);

            var bankApi = RestService.For<IBankApi>(service);
            return await bankApi.GetBeneficiaries(userKey);
        }
        catch (ApiException apiEx)
        {
            return HandleApiException<PaymentInitResponse>(apiEx);
    
        }
        catch (Exception ex)
        {
            return CreateErrorResponse<PaymentInitResponse>(
                HttpStatusCode.InternalServerError,
                $"Execution failed: {ex.Message}");
        }
    }

    public async Task<ApiResponse<PaymentReviewResponse>> SubmitPaymentReview(PaymentReviewRequest request)
    {
        if (!IsInternetAvailable())
            return CreateNoInternetResponse<PaymentReviewResponse>();

        try
        {
            var userKey = Preferences.Get("UserKey", string.Empty);
            var service = _httpService.Client;
            service.DefaultRequestHeaders.Clear();
            service.DefaultRequestHeaders.Add("UserKey", userKey);
            var bankApi = RestService.For<IBankApi>(service);
            return await bankApi.SubmitPaymentReview(request);
        }
        catch (ApiException apiEx)
        {
            return HandleApiException<PaymentReviewResponse>(apiEx);
        }
        catch (Exception ex)
        {
            return CreateErrorResponse<PaymentReviewResponse>(
                HttpStatusCode.InternalServerError, 
                $"Submission failed: {ex.Message}");
        }
    }

    public async Task<ApiResponse<PaymentExecuteResponse>> ExecutePayment(PaymentExecuteRequest request)
    {
        if (!IsInternetAvailable())
            return CreateNoInternetResponse<PaymentExecuteResponse>();

        try
        {
            var userKey = Preferences.Get("UserKey", string.Empty);
            var service = _httpService.Client;
            service.DefaultRequestHeaders.Clear();
            service.DefaultRequestHeaders.Add("UserKey", userKey);
            var bankApi = RestService.For<IBankApi>(service);
            return await bankApi.ExecutePayment(request);
        }
        catch (ApiException apiEx)
        {
            return HandleApiException<PaymentExecuteResponse>(apiEx);
        }
        catch (Exception ex)
        {
            return CreateErrorResponse<PaymentExecuteResponse>(
                HttpStatusCode.InternalServerError, 
                $"Execution failed: {ex.Message}");
        }
    }

    private bool IsInternetAvailable()
    {
        return _connectivity.NetworkAccess == NetworkAccess.Internet;
    }

    private static ApiResponse<T> CreateNoInternetResponse<T>()
    {
        return new ApiResponse<T>(
            new HttpResponseMessage(HttpStatusCode.ServiceUnavailable),
            default,
            new RefitSettings { 
                ExceptionFactory = _ => Task.FromResult<Exception?>(null) 
            });
    }

    private static ApiResponse<T> HandleApiException<T>(ApiException ex)
    {
        var response = new ApiResponse<T>(
            new HttpResponseMessage(ex.StatusCode),
            default,
            new RefitSettings { 
                ExceptionFactory = _ => Task.FromResult<Exception?>(null) 
            });

        return response;
    }

    private static ApiResponse<T> CreateErrorResponse<T>(HttpStatusCode statusCode, string message)
    {
        return new ApiResponse<T>(
            new HttpResponseMessage(statusCode),
            default,
            new RefitSettings { 
                ExceptionFactory = _ => Task.FromResult<Exception?>(null) 
            });
    }
}