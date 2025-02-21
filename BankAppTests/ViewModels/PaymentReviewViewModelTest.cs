using Xunit;
using Moq;
using BankApp.Services;
using BankApp.ViewModels;

namespace BankAppTests.ViewModels
{
    public class PaymentReviewViewModelTest
    {
        [Fact]
        public void TestPaymentReviewViewModelInitialization()
        {
            var mockBankApiService = new Mock<IBankApiService>();
            var viewModel = new PaymentReviewViewModel(mockBankApiService.Object);
            Assert.NotNull(viewModel);
        }
    }
}