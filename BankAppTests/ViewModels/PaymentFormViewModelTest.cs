using BankApp.Models;
using BankApp.Services;
using BankApp.ViewModels;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace BankAppTests.ViewModels
{
    public class PaymentFormViewModelTest
    {
        private readonly Mock<IBankApiService> _mockBankApiService;
        private readonly PaymentFormViewModel _viewModel;

        public PaymentFormViewModelTest()
        {
            _mockBankApiService = new Mock<IBankApiService>();
            _viewModel = new PaymentFormViewModel(_mockBankApiService.Object);
        }

        [Fact]
        public void Constructor_InitializesProperties()
        {
            Assert.NotNull(_viewModel.Accounts);
            Assert.Equal(0, _viewModel.Amount);
            Assert.NotNull(_viewModel.SelectedAccount);
            Assert.NotNull(_viewModel.Beneficiary);
        }

        [Fact]
        public async Task SubmitPayment_InvalidAccount_ShowsError()
        {
            _viewModel.SelectedAccount = new Account(); 
            _viewModel.Amount = 100;

            await _viewModel.SubmitPaymentCommand.ExecuteAsync(null);

            _mockBankApiService.Verify(x => x.SubmitPaymentReview(It.IsAny<PaymentReviewRequest>()), Times.Never);
        }
    }
}