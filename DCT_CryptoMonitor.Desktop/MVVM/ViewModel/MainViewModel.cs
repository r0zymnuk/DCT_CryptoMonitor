using System.Collections.ObjectModel;
using System.Windows.Documents;
using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Desktop.Core;

namespace DCT_CryptoMonitor.Desktop.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public ObservableCollection<CoinMinimal> Coins { get; set; } = new();
}