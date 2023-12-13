using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DCT_CryptoMonitor.Core.Models;

namespace DCT_CryptoMonitor.Desktop.Components;

public partial class CoinCard : UserControl
{
    public static readonly DependencyProperty CoinProperty = DependencyProperty.Register(
        nameof(Coin), typeof(CoinMinimal), typeof(CoinCard), new PropertyMetadata(default(CoinMinimal)));

    public CoinMinimal Coin
    {
        get => (CoinMinimal)GetValue(CoinProperty);
        set => SetValue(CoinProperty, value);
    }

    public CoinCard()
    {
        InitializeComponent();
    }
}
