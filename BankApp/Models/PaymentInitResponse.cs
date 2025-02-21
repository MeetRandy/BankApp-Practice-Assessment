using System.Collections.ObjectModel;

namespace BankApp.Models
{
    public class PaymentInitResponse
    {
        public List<Account>? Accounts { get; set; }
        public ObservableCollection<Beneficiary>? Beneficiaries { get; set; }

    }
}