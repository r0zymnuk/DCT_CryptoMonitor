using DCT_CryptoMonitor.Desktop.Core;
using DCT_CryptoMonitor.Desktop.MVVM.Model;
using DCT_CryptoMonitor.Desktop.Services.Coins;
using DCT_CryptoMonitor.Desktop.Services.Navigation;

namespace DCT_CryptoMonitor.Desktop.MVVM.ViewModels;
public class HomeViewModel : Core.ViewModel
{
    private List<Coin> _coins = new();
    private INavigationService _navigation;

    public List<Coin> Coins
    {
        get => _coins;
        set 
        { 
            _coins = value;
            OnPropertyChanged();
        }
    }
    public INavigationService Navigation 
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToCoin { get; set; }

    private readonly ICoinService _coinService;
    private readonly MainViewModel _mainViewModel;

    public HomeViewModel(INavigationService navService, ICoinService coinService, MainViewModel mainViewModel)
    {
        Navigation = navService;
        _coinService = coinService;
        _mainViewModel = mainViewModel;
        NavigateToCoin = new RelayCommand(PickCoin, o => true);

        Task.Run(async () => { Coins = await _coinService.GetTopMarketCapCoins(); });
    }

    private void PickCoin(object? param)
    {
        if (param is string coinId && !string.IsNullOrWhiteSpace(coinId))
        {
            _mainViewModel.SelectedCoinId = coinId;
            Navigation.NavigateTo<CoinViewModel>();
        }
    }
}
