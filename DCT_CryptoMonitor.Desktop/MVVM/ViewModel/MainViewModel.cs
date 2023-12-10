using System.Windows.Documents;
using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Desktop.Core;

namespace DCT_CryptoMonitor.Desktop.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    private List<string> _currencies = new();
    private string _selectedCurrency;

    public List<string> Currencies
    {
        get => _currencies;
        set {
            _currencies = value;
            OnPropertyChanged();
        }
    }

    public string SelectedCurrency
    {
        get => _selectedCurrency;
        set {
            _selectedCurrency = value;
            OnPropertyChanged();
        }
    }
    
    public List<CoinMinimal> Coins { get; set; } = new();
}