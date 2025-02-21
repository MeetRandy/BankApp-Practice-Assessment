using BankApp.ViewModels;
using BankApp.Services;

namespace BankApp.Views
{
    public partial class BeneficiariesPage : ContentPage
    {
        public BeneficiariesPage(BeneficiariesViewModel viewModel)
        {
            InitializeComponent();      
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Preferences.Clear();
            SearchBar.Text = string.Empty;
            BeneficiariesViewModel viewModel = (BeneficiariesViewModel)BindingContext;
            viewModel.LoadBeneficiariesCommand.Execute(null);
            RootLayout.Opacity = 0;
            RootLayout.FadeTo(1, 500, Easing.CubicInOut);
        }
    }
}