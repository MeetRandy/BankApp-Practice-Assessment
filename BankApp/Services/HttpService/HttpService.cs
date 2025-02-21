using BankApp.Services;
namespace BankApp.Services.HttpService
{
    public class HttpService : IHttpService
    {

        public HttpService()
        {
            // Keeping this client for default use cases
            Client = new HttpClient(new HttpLoggingHandler());
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client.DefaultRequestHeaders.Add("Userkey", ServiceLocator.UserKey);
            Client.BaseAddress = new Uri(EnvironmentConfig.BaseAddress.ToString());
        }

        public HttpClient Client { get; }

        // New method to create a client with a specific base address
        public HttpClient CreateHttpClient(Uri baseAddress, string contentType = "application/json")
        {
            var client = new HttpClient();

            client = new HttpClient(new HttpLoggingHandler());
            client.DefaultRequestHeaders.Add("Accept", contentType);
            client.DefaultRequestHeaders.Add("Userkey", ServiceLocator.UserKey);
            client.BaseAddress = baseAddress;
            return client;
        }

    }
}