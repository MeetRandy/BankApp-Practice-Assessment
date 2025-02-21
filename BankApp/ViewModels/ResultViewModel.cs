using BankApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace BankApp.ViewModels;

[QueryProperty(nameof(PaymentReference), nameof(PaymentReference))]
public partial class ResultViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _paymentReference;

    public ResultViewModel()
    {
        _paymentReference = string.Empty;
    }
    [RelayCommand]
    private async Task Done()
    {
        await Shell.Current.GoToAsync("///BeneficiariesPage");    
    }
}