namespace BankApp.Services.HttpService
{
    public interface IHttpService
    {
        HttpClient Client { get; }
        public HttpClient CreateHttpClient(Uri baseAddress, string contentType = "application/json");
    }
}