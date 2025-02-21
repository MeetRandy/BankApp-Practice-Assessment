namespace BankApp;

public static partial class EnvironmentConfig
    {
        public static Uri BaseAddress { get; set; }
        static EnvironmentConfig()
        {
            // Environment Config
            BaseAddress = new Uri("https://testbankapi.azurewebsites.net");
        }
}