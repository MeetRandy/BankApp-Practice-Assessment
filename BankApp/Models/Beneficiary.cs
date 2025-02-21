using System.Text.Json.Serialization;

namespace BankApp.Models
{
    public class Beneficiary
    {
        [JsonPropertyName("id")] // 
        public string? Id { get; set; }

        [JsonPropertyName("name")] // 
        public string? Name { get; set; }

        [JsonPropertyName("bank")] // 
        public string? Bank { get; set; }

        [JsonPropertyName("bankCode")] // 
        public string? BankCode { get; set; }

        [JsonPropertyName("branchName")] // 
        public string? BranchName { get; set; }

        [JsonPropertyName("branchCode")] // 
        public string? BranchCode { get; set; }
    }
}
