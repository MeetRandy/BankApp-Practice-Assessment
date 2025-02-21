using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using BankApp.Models;
using BankApp.Services;
using BankApp.Services.HttpService;
using BankApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Refit;
namespace BankApp.ViewModels;
public partial class BeneficiariesViewModel : BaseViewModel
{
    private readonly IBankApiService _bankApiService;

    private ObservableCollection<Beneficiary> _filteredBeneficiaries = new ObservableCollection<Beneficiary>();
    public ObservableCollection<Beneficiary> FilteredBeneficiaries
    {
        get => _filteredBeneficiaries;
        set
        {
            _filteredBeneficiaries = value;
            OnPropertyChanged(nameof(FilteredBeneficiaries));
        }
    }

    private string _searchText;
    public string Search
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged(nameof(Search));
                FilterBeneficiaries();
            }
        }
    }
    private PaymentInitResponse _data;
    public PaymentInitResponse Data
    {
        get => _data;
        set => SetProperty(ref _data, value);
    }
    public List<Account> Accounts => Data?.Accounts?.ToList() ?? new List<Account>();
    public Beneficiary Beneficiary => Data.Beneficiaries?.FirstOrDefault() ?? new Beneficiary();

    public ObservableCollection<Beneficiary> Beneficiaries => Data.Beneficiaries ?? [];

    public BeneficiariesViewModel(IBankApiService bankApiService)
    {
        _bankApiService = bankApiService;
        _data = new PaymentInitResponse();
        _searchText = string.Empty;

        Task.Run(LoadBeneficiaries);
    }


    [RelayCommand]
    private async Task LoadBeneficiaries()
    {
        try
        {
            IsBusy = true;
            var userkey = ServiceLocator.UserKey;

            var response = await _bankApiService.GetBeneficiaries(userkey);

            if (response.IsSuccessStatusCode)
            {
                Data = response.Content != null ? response.Content : new PaymentInitResponse();
                FilteredBeneficiaries = [.. Data.Beneficiaries ?? []];
                Preferences.Set("UserKey", userkey);
            }
            else
            {
                await HandleApiError(response.Error);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SelectBeneficiary(Beneficiary beneficiary)
    {
        var accounts = Accounts;
        await Shell.Current.GoToAsync(nameof(PaymentFormPage),
        new Dictionary<string, object>
        {
            ["Beneficiary"] = beneficiary,
            ["Accounts"] = accounts
        });
    }

    [RelayCommand]
    private void FilterBeneficiaries()
    {
        if (string.IsNullOrWhiteSpace(Search))
        {
            FilteredBeneficiaries = new ObservableCollection<Beneficiary>(Beneficiaries);
        }
        else
        {
            var filtered = Beneficiaries
                           .Where(b => b.Name?.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0)
                           .ToList();
            FilteredBeneficiaries = new ObservableCollection<Beneficiary>(filtered);
        }
    }

}