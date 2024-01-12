using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DCT_CryptoMonitor.Desktop.MVVM.Model;

namespace DCT_CryptoMonitor.Desktop.Components;

public partial class CoinCard : UserControl
{
    public static readonly DependencyProperty CoinProperty = DependencyProperty.Register(
        nameof(Coin), typeof(Coin), typeof(CoinCard), new PropertyMetadata(default(Coin)));

    public Coin Coin
    {
        get => (Coin)GetValue(CoinProperty);
        set => SetValue(CoinProperty, value);
    }



    public ICommand NavigateToCoinCommand
    {
        get { return (ICommand)GetValue(NavigateToCoinCommandProperty); }
        set { SetValue(NavigateToCoinCommandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NavigateToCoinCommandProperty =
        DependencyProperty.Register(nameof(NavigateToCoinCommand), typeof(ICommand), typeof(CoinCard));



    public CoinCard()
    {
        InitializeComponent();
    }

    private void NavigateToCoinView(object sender, MouseButtonEventArgs e)
    {
        NavigateToCoinCommand.Execute(Coin.Id);
    }
}
