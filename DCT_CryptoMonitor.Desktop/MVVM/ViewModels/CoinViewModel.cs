using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Core.Services;
using System.Windows;

namespace DCT_CryptoMonitor.Desktop.MVVM.ViewModel;
public class CoinViewModel : Core.ViewModel
{
    private Coin _coin = new();
    public Coin Coin 
    {
        get => _coin;
        set
        {
            _coin = value;
            OnPropertyChanged();
        }
    }

    public string CoinId { get; set; } = "bitcoin";

    private readonly ICoinService _coinService;

    public CoinViewModel(ICoinService coinService, MainViewModel mainViewModel)
    {
        _coinService = coinService;
        CoinId = mainViewModel.SelectedCoinId;

        Task.Run(async () => 
        { 
            Coin = await _coinService.GetCoinById(CoinId);
        });
    }

    public async Task GetCoinData(string coinId)
    {
        Coin = await _coinService.GetCoinById(coinId);
    }
}