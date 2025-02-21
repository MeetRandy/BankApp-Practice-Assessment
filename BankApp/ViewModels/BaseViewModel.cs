using System.Net;
using BankApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Refit;
namespace BankApp.ViewModels;
public partial class BaseViewModel : ObservableObject
{
    private bool? _isBusy;
    public bool? IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public async Task HandleApiError(ApiException ex)
    {
        string message = ex.StatusCode switch
        {
            HttpStatusCode.ServiceUnavailable => "No internet connection",
            HttpStatusCode.Unauthorized => "Invalid user key",
            HttpStatusCode.BadRequest => ex.Content ?? ex.Message,
            _ => $"API error: {ex.Content ?? ex.Message}"
        };

        await Shell.Current.DisplayAlert("Error", message, "OK");
    }
}