using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BankApp.Services;
using BankApp.ViewModels;
using BankApp.Views;
using Refit;
using Newtonsoft.Json;
using BankApp.Services.HttpService;


namespace BankApp;

public static class MauiProgram
{
	public static IServiceProvider? ServiceProvider { get; private set; }

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddRefitClient<IBankApiService>(new RefitSettings
		{
			ContentSerializer = new NewtonsoftJsonContentSerializer(
		new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore,
			DateTimeZoneHandling = DateTimeZoneHandling.Utc
		})
		})
		.ConfigureHttpClient(c =>	
		{
			c.BaseAddress = new Uri("https://testbankapi.azurewebsites.net");
			c.DefaultRequestHeaders.Add("UserKey", ServiceLocator.UserKey);
			c.Timeout = TimeSpan.FromSeconds(30);
		});

		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddSingleton<IHttpService, HttpService>();
		builder.Services.AddSingleton<IBankApiService, BankApiService>();
		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

		// Register ViewModels
		builder.Services.AddTransient<BeneficiariesViewModel>();
		builder.Services.AddTransient<PaymentFormViewModel>();
		builder.Services.AddTransient<PaymentReviewViewModel>();
		builder.Services.AddTransient<ResultViewModel>();

		// Register Views
		builder.Services.AddTransient<BeneficiariesPage>();
		builder.Services.AddTransient<PaymentFormPage>();
		builder.Services.AddTransient<PaymentReviewPage>();
		builder.Services.AddTransient<ResultPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}