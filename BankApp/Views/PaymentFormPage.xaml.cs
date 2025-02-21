using BankApp.Services;
using BankApp.Services.HttpService;
using BankApp.ViewModels;

namespace BankApp.Views
{
    public partial class PaymentFormPage : ContentPage
    {
        private IBankApiService _bankApiService;
        
        public PaymentFormPage(IBankApiService bankApiService)
        {
            InitializeComponent();
            _bankApiService = bankApiService;
            BindingContext = new PaymentFormViewModel(_bankApiService);
        }   
    
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}