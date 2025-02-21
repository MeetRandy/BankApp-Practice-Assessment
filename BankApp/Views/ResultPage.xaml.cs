using BankApp.ViewModels;

namespace BankApp.Views
{
    public partial class ResultPage : ContentPage
    {
        public ResultPage()
        {
            InitializeComponent();
            BindingContext = new ResultViewModel();
        }
    }
}