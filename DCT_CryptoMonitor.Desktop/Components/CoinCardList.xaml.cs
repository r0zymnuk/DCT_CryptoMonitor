using DCT_CryptoMonitor.Core.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DCT_CryptoMonitor.Desktop.Components;
/// <summary>
/// Interaction logic for CoinCardList.xaml
/// </summary>
public partial class CoinCardList : UserControl
{
    public List<Coin> CoinList
    {
        get => (List<Coin>)GetValue(CoinListProperty);
        set => SetValue(CoinListProperty, value);
    }

    public static readonly DependencyProperty CoinListProperty = DependencyProperty.Register(
        nameof(CoinList), 
        typeof(List<Coin>), 
        typeof(CoinCardList), 
        new PropertyMetadata(default(List<Coin>)));



    public ICommand ListNavigateToCoinCommand
    {
        get { return (ICommand)GetValue(ListNavigateToCoinCommandProperty); }
        set { SetValue(ListNavigateToCoinCommandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ListNavigateToCoinCommandProperty =
        DependencyProperty.Register(nameof(ListNavigateToCoinCommand), typeof(ICommand), typeof(CoinCardList));

    public CoinCardList()
    {
        InitializeComponent();
    }
}
