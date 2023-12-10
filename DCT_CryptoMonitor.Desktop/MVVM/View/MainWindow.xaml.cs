using System.Windows;
using System.Windows.Controls;
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
    }
    
    protected override async void OnInitialized(EventArgs e)
    {
        
        var currencies = await _coinService.GetSupportedVsCurrencies();
        DataContext = new MainViewModel
        {
            Currencies = currencies,
            SelectedCurrency = currencies.Find(c => c == "usd") ?? currencies[0],
            Coins = await _coinService.GetTopMarketCapCoins()
        };
        // ComboBox.ItemsSource = (DataContext as MainViewModel)!.Currencies;
        base.OnInitialized(e);
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(await _coinService.Ping() ? "Ping successful" : "Ping failed");
    }
}