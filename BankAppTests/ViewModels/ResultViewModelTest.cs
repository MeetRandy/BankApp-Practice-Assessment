using BankApp.ViewModels;
using Xunit;
using Moq;

namespace BankAppTests.ViewModels
{
    public class ResultViewModelTest
    {
        private ResultViewModel _viewModel;

        public ResultViewModelTest()
        {
            _viewModel = new ResultViewModel();
        }

        [Fact]
        public void PaymentReference_DefaultValue_ShouldBeEmptyString()
        {
            Assert.Equal(string.Empty, _viewModel.PaymentReference);
        }

        [Fact]
        public void PaymentReference_SetValue_ShouldUpdateValue()
        {
            var expectedValue = "TestReference";
            _viewModel.PaymentReference = expectedValue;
            Assert.Equal(expectedValue, _viewModel.PaymentReference);
        }
    }
}