using BankApp.Models;
using BankApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BankApp.ViewModels;

[QueryProperty(nameof(Accounts), nameof(Accounts))]
[QueryProperty(nameof(Beneficiary), nameof(Beneficiary))]
public partial class PaymentFormViewModel : BaseViewModel
{

    private readonly IBankApiService _bankApiService;

    [ObservableProperty]
    private List<Account> _Accounts;

    [ObservableProperty]
    private Beneficiary _Beneficiary;

    [ObservableProperty]
    private Account _SelectedAccount;

    [ObservableProperty]
    private decimal _amount;

    public PaymentFormViewModel(IBankApiService bankApiService)
    {
        _bankApiService = bankApiService;
        _Accounts = [];
        _amount = 0;
        _SelectedAccount = new Account();
        _Beneficiary = new Beneficiary();
    }

    [RelayCommand]
    private async Task SubmitPayment()
    {
        try
        {
            if (SelectedAccount == null || Amount <= 0)
            {
                await Shell.Current.DisplayAlert("Error", "Please select account and enter valid amount", "OK");
                return;
            }

            var request = new PaymentReviewRequest
            {
                BeneficiaryId = Beneficiary.Id,
                AccountNumber = SelectedAccount.Number,
                Amount = Amount
            };

            var response = await _bankApiService.SubmitPaymentReview(request);
            if (response.IsSuccessStatusCode)
            {
                var navParams = new Dictionary<string, object>
                {
                    ["Beneficiary"] = Beneficiary,
                    ["Account"] = SelectedAccount,
                    ["Amount"] = Amount,
                    ["Fee"] = response.Content?.Fees ?? 0,
                    ["PaymentReviewId"] = response.Content?.InstructionIdentifier ?? string.Empty
                };

                await Shell.Current.GoToAsync("PaymentReviewPage", navParams);
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