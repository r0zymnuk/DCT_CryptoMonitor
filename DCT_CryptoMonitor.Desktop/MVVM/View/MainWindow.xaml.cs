using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Core.Services;
using DCT_CryptoMonitor.Desktop.MVVM.ViewModel;

namespace DCT_CryptoMonitor.Desktop.MVVM.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ICoinService _coinService;

    public MainWindow(ICoinService coinService)
    {
        _coinService = coinService;
        InitializeComponent();
        Console.WriteLine("MainWindow initialized");
    }
    
    protected override async void OnInitialized(EventArgs e)
    {
        DataContext = new MainViewModel
        {
            Coins = new ObservableCollection<CoinMinimal>(await _coinService.GetTopMarketCapCoins())
        };
        base.OnInitialized(e);
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(await _coinService.Ping() ? "Ping successful" : "Ping failed");
    }
}