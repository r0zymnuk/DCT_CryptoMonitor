using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DCT_CryptoMonitor.Desktop.MVVM.Model;
using DCT_CryptoMonitor.Desktop.Services.Coins;

namespace DCT_CryptoMonitor.Desktop.Components;

public partial class MarketsList : UserControl
{
    public List<Market> Markets
    {
        get => (List<Market>)GetValue(MarketsProperty);
        set => SetValue(MarketsProperty, value);
    }

    public static readonly DependencyProperty MarketsProperty = DependencyProperty.Register(
        nameof(Markets),
        typeof(List<Market>),
        typeof(MarketsList),
        new PropertyMetadata(default(List<Market>)));

    public ICoinService CoinService
    {
        get => (ICoinService)GetValue(CoinServiceProperty);
        set => SetValue(CoinServiceProperty, value);
    }
    
    public static readonly DependencyProperty CoinServiceProperty = DependencyProperty.Register(
        nameof(CoinService),
        typeof(ICoinService),
        typeof(MarketsList),
        new PropertyMetadata(default(ICoinService)));
    
    public MarketsList()
    {
        InitializeComponent();
    }

    private void ListBox_UnblockScroll(object sender, MouseWheelEventArgs e)
    {
        if (e.Handled) return;
        e.Handled = true;
        var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
        {
            RoutedEvent = MouseWheelEvent,
            Source = sender
        };
        var parent = ((Control)sender).Parent as UIElement;
        parent?.RaiseEvent(eventArg);
    }
}