using BankApp.Models;
using BankApp.Services;
using BankApp.Services.HttpService;
using Moq;
using Refit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BankAppTests.Services
{
    public class BankApiServiceTest
    {
        private readonly Mock<IHttpService> _mockHttpService;
        private readonly Mock<IConnectivity> _mockConnectivity;
        private readonly Mock<IBankApi> _mockBankApi;
        private readonly BankApiService _bankApiService;

        public BankApiServiceTest()
        {
            _mockHttpService = new Mock<IHttpService>();
            _mockConnectivity = new Mock<IConnectivity>();
            _mockBankApi = new Mock<IBankApi>();

            var httpClient = new HttpClient();
            _mockHttpService.Setup(x => x.Client).Returns(httpClient);

            _bankApiService = new BankApiService(_mockHttpService.Object, _mockConnectivity.Object);
        }

        [Fact]
        public async Task GetBeneficiaries_NoInternet_ReturnsServiceUnavailable()
        {
            _mockConnectivity.Setup(x => x.NetworkAccess).Returns(NetworkAccess.None);

            var response = await _bankApiService.GetBeneficiaries("userKey");

            Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        }

        // [Fact]
        // public async Task GetBeneficiaries_ApiException_ReturnsErrorResponse()
        // {
        //     _mockConnectivity.Setup(x => x.NetworkAccess).Returns(NetworkAccess.Internet);
        //     _mockBankApi.Setup(x => x.GetBeneficiaries(It.IsAny<string>()))
        //         .ThrowsAsync(new ApiException(new HttpRequestMessage(), HttpMethod.Get, new HttpResponseMessage(HttpStatusCode.BadRequest), null, null));

        //     var response = await _bankApiService.GetBeneficiaries("userKey");

        //     Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // }

        // [Fact]
        // public async Task SubmitPaymentReview_NoInternet_ReturnsServiceUnavailable()
        // {
        //     _mockConnectivity.Setup(x => x.NetworkAccess).Returns(NetworkAccess.None);

        //     var response = await _bankApiService.SubmitPaymentReview(new PaymentReviewRequest());

        //     Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        // }

        // [Fact]
        // public async Task SubmitPaymentReview_ApiException_ReturnsErrorResponse()
        // {
        //     _mockConnectivity.Setup(x => x.NetworkAccess).Returns(NetworkAccess.Internet);
        //     _mockBankApi.Setup(x => x.SubmitPaymentReview(It.IsAny<PaymentReviewRequest>()))
        //         .ThrowsAsync(new ApiException(new HttpRequestMessage(), HttpMethod.Post, new HttpResponseMessage(HttpStatusCode.BadRequest), null, null));

        //     var response = await _bankApiService.SubmitPaymentReview(new PaymentReviewRequest());

        //     Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // }

        // [Fact]
        // public async Task ExecutePayment_NoInternet_ReturnsServiceUnavailable()
        // {
        //     _mockConnectivity.Setup(x => x.NetworkAccess).Returns(NetworkAccess.None);

        //     var response = await _bankApiService.ExecutePayment(new PaymentExecuteRequest());

        //     Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        // }

        // [Fact]
        // public async Task ExecutePayment_ApiException_ReturnsErrorResponse()
        // {
        //     _mockConnectivity.Setup(x => x.NetworkAccess).Returns(NetworkAccess.Internet);
        //     _mockBankApi.Setup(x => x.ExecutePayment(It.IsAny<PaymentExecuteRequest>()))
        //         .ThrowsAsync(new ApiException(new HttpRequestMessage(), HttpMethod.Post, new HttpResponseMessage(HttpStatusCode.BadRequest), null, null));

        //     var response = await _bankApiService.ExecutePayment(new PaymentExecuteRequest());

        //     Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // }
    }
}