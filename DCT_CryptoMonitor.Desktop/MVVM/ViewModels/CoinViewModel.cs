using System.Windows;
using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Core.Models.Enums;
using DCT_CryptoMonitor.Core.Services;
using DCT_CryptoMonitor.Desktop.MVVM.View;
using ScottPlot.Plottable;

namespace DCT_CryptoMonitor.Desktop.MVVM.ViewModels;
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
                    coinView.RenderPriceChart();
                }
            }));
            OnPropertyChanged();
        }
    }

    private readonly ICoinService _coinService;
    private readonly string _coinId;
    public PriceInterval Interval = PriceInterval.m30;
    public DateTime fromDateTime = DateTime.Now.AddDays(-5);
    public DateTime toDateTime = DateTime.Now;

    private List<Market> _markets = new();

    public List<Market> Markets
    {
        get => _markets;
        set
        {
            _markets = value;
            OnPropertyChanged();
        }
    }


    public CoinViewModel(ICoinService coinService, MainViewModel mainViewModel)
    {
        _coinService = coinService;
        _coinId = mainViewModel.SelectedCoinId;
    }

    public async Task GetCoinData()
    {
        Coin = await _coinService.GetCoinById(_coinId);
        await GetCoinHistoryData();
        Markets = await _coinService.GetMarkets(_coinId);
    }

    public async Task GetCoinHistoryData()
    {
        PriceHistory = await _coinService.GetPriceHistory(_coinId, fromDateTime, toDateTime, Interval);
    }
    
}