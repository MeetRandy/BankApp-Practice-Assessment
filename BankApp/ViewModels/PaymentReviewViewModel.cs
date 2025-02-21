using BankApp.Models;
using BankApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace BankApp.ViewModels;

[QueryProperty(nameof(Beneficiary), nameof(Beneficiary))]
[QueryProperty(nameof(Account), nameof(Account))]
[QueryProperty(nameof(Amount), nameof(Amount))]
[QueryProperty(nameof(Fee), nameof(Fee))]
[QueryProperty(nameof(PaymentReviewId), nameof(PaymentReviewId))]
public partial class PaymentReviewViewModel : BaseViewModel
{
    [ObservableProperty]
    private Beneficiary _beneficiary;

    [ObservableProperty]
    private Account _account;

    [ObservableProperty]
    private decimal _amount;

    [ObservableProperty]
    private decimal _fee;

    [ObservableProperty]
    private string _paymentReviewId;

    private readonly IBankApiService _bankApiService;

    public PaymentReviewViewModel(IBankApiService bankApiService)
    {
        _bankApiService = bankApiService;
        _beneficiary = new Beneficiary();
        _account = new Account();
        _amount = 0;
        _fee = 0;
        _paymentReviewId = string.Empty;
    }

    [RelayCommand]
    private async Task ExecutePayment()
    {
        try
        {
            var request = new PaymentExecuteRequest
            {
                InstructionIdentifier = PaymentReviewId
            };

            var response = await _bankApiService.ExecutePayment(request);
            if (response.IsSuccessStatusCode)
            {
               var parameters = new Dictionary<string, object>
               {
                 ["PaymentReference"] = response.Content?.InstructionReference ?? string.Empty 
               };

                await Shell.Current.GoToAsync("ResultPage", parameters);
            }
            else
            {
                await HandleApiError(response.Error);
            }
            
        }
        finally
        {
           IsBusy = false;
        }
    }
}