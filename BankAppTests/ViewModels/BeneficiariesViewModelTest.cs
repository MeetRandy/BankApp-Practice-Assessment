using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BankApp.Models;
using BankApp.Services;
using BankApp.ViewModels;
using Moq;
using Refit;
using Xunit;

namespace BankAppTests.ViewModels
{
    public class BeneficiariesViewModelTest
    {
        private readonly Mock<IBankApiService> _mockBankApiService;
        private readonly BeneficiariesViewModel _viewModel;

        public BeneficiariesViewModelTest()
        {
            _mockBankApiService = new Mock<IBankApiService>();
            _viewModel = new BeneficiariesViewModel(_mockBankApiService.Object);
        }

        [Fact]
        public async Task LoadBeneficiaries_Success()
        {
            var response = new ApiResponse<PaymentInitResponse>(new HttpResponseMessage(HttpStatusCode.OK), new PaymentInitResponse
            {
                Beneficiaries = new ObservableCollection<Beneficiary>
                {
                    new Beneficiary { Name = "John Doe" },
                    new Beneficiary { Name = "Jane Doe" }
                }
            }, new RefitSettings());

            _mockBankApiService.Setup(x => x.GetBeneficiaries(It.IsAny<string>())).ReturnsAsync(response);

            await _viewModel.LoadBeneficiariesCommand.ExecuteAsync(null);

            Assert.NotNull(_viewModel.Data);
            Assert.Equal(2, _viewModel.FilteredBeneficiaries.Count);
            Assert.Equal("John Doe", _viewModel.FilteredBeneficiaries[0].Name);
        }

        [Fact]
        public async Task LoadBeneficiaries_Failure()
        {
            var response = new ApiResponse<PaymentInitResponse>(new HttpResponseMessage(HttpStatusCode.BadRequest), null, new RefitSettings());

            _mockBankApiService.Setup(x => x.GetBeneficiaries(It.IsAny<string>())).ReturnsAsync(response);

            await _viewModel.LoadBeneficiariesCommand.ExecuteAsync(null);

            Assert.Null(_viewModel.Data);
            Assert.Empty(_viewModel.FilteredBeneficiaries);
        }

        [Fact]
        public void FilterBeneficiaries_EmptySearch()
        {
            _viewModel.Data = new PaymentInitResponse
            {
                Beneficiaries = new ObservableCollection<Beneficiary>
                {
                    new Beneficiary { Name = "John Doe" },
                    new Beneficiary { Name = "Jane Doe" }
                }
            };

            _viewModel.Search = string.Empty;

            Assert.Equal(2, _viewModel.FilteredBeneficiaries.Count);
        }

        [Fact]
        public void FilterBeneficiaries_WithSearch()
        {
            _viewModel.Data = new PaymentInitResponse
            {
                Beneficiaries = new ObservableCollection<Beneficiary>
                {
                    new Beneficiary { Name = "John Doe" },
                    new Beneficiary { Name = "Jane Doe" }
                }
            };

            _viewModel.Search = "Jane";

            Assert.Single(_viewModel.FilteredBeneficiaries);
            Assert.Equal("Jane Doe", _viewModel.FilteredBeneficiaries[0].Name);
        }
    }
}