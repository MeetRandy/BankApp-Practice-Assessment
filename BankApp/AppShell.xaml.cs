using BankApp.Views;

namespace BankApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(PaymentFormPage), typeof(PaymentFormPage));
        Routing.RegisterRoute(nameof(PaymentReviewPage), typeof(PaymentReviewPage));
        Routing.RegisterRoute(nameof(ResultPage), typeof(ResultPage));
    }
}