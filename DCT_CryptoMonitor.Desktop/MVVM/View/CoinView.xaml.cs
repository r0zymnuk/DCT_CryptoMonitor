using System.Windows.Controls;
using System.Windows.Media;
using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Desktop.MVVM.ViewModel;

namespace DCT_CryptoMonitor.Desktop.MVVM.View;
/// <summary>
/// Interaction logic for CoinView.xaml
/// </summary>
public partial class CoinView : UserControl
{
    //private readonly CoinViewModel _viewModel;

    public CoinView()
    {
        //_viewModel = viewModel;
        //DataContext = _viewModel;
        InitializeComponent();
    }
    
    protected override async void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        var dataContext = (CoinViewModel)DataContext;
        await dataContext.GetCoinData();
        await dataContext.GetCoinHistoryData();
        
        PlotPriceHistory(dataContext.PriceHistory.ToArray());
    }

    public void PlotPriceHistory(PriceHistory[] priceHistory)
    {
        var prices = priceHistory.Select(ph => (double)ph.PriceUsd).ToArray();
        if (prices.Length == 0)
        {
            prices = new[] { 0.0 };
        }

        var dates = priceHistory.Select(ph => ph.DateTime.ToOADate()).ToArray();
        if (dates.Length == 0)
        {
            dates = new[] { DateTime.Now.ToOADate() };
        }

        CoinGraph.Plot.Clear();
        CoinGraph.Plot.AddSignalXY(dates, prices);
        CoinGraph.Plot.XAxis.DateTimeFormat(true);
        CoinGraph.Plot.SetAxisLimitsY(prices.Min()-(prices.Max()-prices.Min()), prices.Max()+(prices.Max()-prices.Min()));
        CoinGraph.Render();
    }
}
