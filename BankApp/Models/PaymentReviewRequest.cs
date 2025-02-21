namespace BankApp.Models
{
    public class PaymentReviewRequest
    {
        public string? BeneficiaryId { get; set; }
        public string? AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
