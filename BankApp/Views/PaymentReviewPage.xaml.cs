using BankApp.Services;
using BankApp.ViewModels;

namespace BankApp.Views;

public partial class PaymentReviewPage : ContentPage
{
    private IBankApiService _bankApiService;

    public PaymentReviewPage(IBankApiService bankApiService)
    {
        InitializeComponent();
        _bankApiService = bankApiService;
        BindingContext = new PaymentReviewViewModel(_bankApiService);
    }
    

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
