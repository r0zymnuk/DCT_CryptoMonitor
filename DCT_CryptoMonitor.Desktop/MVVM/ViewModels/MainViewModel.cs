﻿using DCT_CryptoMonitor.Core.Services;
using DCT_CryptoMonitor.Desktop.Core;
using DCT_CryptoMonitor.Desktop.Services;

namespace DCT_CryptoMonitor.Desktop.MVVM.ViewModel;

public class MainViewModel : Core.ViewModel
{
    private INavigationService _navigation;
    public ICoinService CoinService { get; private set; }

    public string SelectedCoinId { get; set; } = "bitcoin";

    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToHomeCommand { get; set;  }
    public RelayCommand NavigateToCoinCommand { get; set; }

    public MainViewModel(INavigationService navService, ICoinService coinService)
    {
        CoinService = coinService;

        Navigation = navService;
        NavigateToHomeCommand = new RelayCommand(o => { Navigation.NavigateTo<HomeViewModel>(); }, o => true);
        NavigateToCoinCommand = new RelayCommand(o => { Navigation.NavigateTo<CoinViewModel>(); }, o => true);
    }
}