using System.Windows;
using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Core.Models.Enums;
using DCT_CryptoMonitor.Core.Services;
using DCT_CryptoMonitor.Desktop.MVVM.View;

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
            OnPropertyChanged();
        }
    }

    private List<PriceHistory> _priceHistory = new();
    public List<PriceHistory> PriceHistory
    {
        get => _priceHistory;
        private set
        {
            _priceHistory = value;
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                var coinView = Application.Current.Windows.OfType<CoinView>().FirstOrDefault();
                if (coinView != null)
                {
                    coinView.PlotPriceHistory(_priceHistory.ToArray());
                }
            }));
            OnPropertyChanged();
        }
    }

    public string CoinId
    {
        get => _coinId;
        set
        {
            if (value == _coinId) return;
            _coinId = value;
            
            OnPropertyChanged();
            //new CoinView().PlotPriceHistory(PriceHistory.ToArray());
            // Update the graph when PriceHistory is updated
        }
    }

    private readonly ICoinService _coinService;
    private string _coinId = "bitcoin";

    public CoinViewModel(ICoinService coinService, MainViewModel mainViewModel)
    {
        _coinService = coinService;
        CoinId = mainViewModel.SelectedCoinId;
        
        /*Task.Run(async () => 
        { 
            Coin = await _coinService.GetCoinById(CoinId);
            PriceHistory = await _coinService.GetPriceHistory(CoinId, DateTime.Today.AddDays(-5), DateTime.Now, PriceInterval.h1);
        });*/
    }

    public async Task GetCoinData()
    {
        Coin = await _coinService.GetCoinById(CoinId);
    }

    public async Task GetCoinHistoryData()
    {
        PriceHistory = await _coinService.GetPriceHistory(CoinId, DateTime.Today.AddDays(-5), DateTime.Now, PriceInterval.m30);
    }
}