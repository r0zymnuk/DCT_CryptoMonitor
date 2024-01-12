using System.Windows.Controls;
using System.Windows.Media;
using DCT_CryptoMonitor.Desktop.MVVM.Model.Enums;
using DCT_CryptoMonitor.Desktop.MVVM.ViewModels;

namespace DCT_CryptoMonitor.Desktop.MVVM.View;
/// <summary>
/// Interaction logic for CoinView.xaml
/// </summary>
public partial class CoinView : UserControl
{
    private CoinViewModel? _dataContext;

    private readonly List<PriceInterval> _priceIntervals = Enum.GetValues(typeof(PriceInterval)).Cast<PriceInterval>().ToList();

    public CoinView()
    {
        InitializeComponent();
        Intervals.ItemsSource = _priceIntervals;
    }
    
    protected override async void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        _dataContext = (CoinViewModel)DataContext;

        Intervals.SelectedIndex = _priceIntervals.IndexOf(_priceIntervals.FirstOrDefault(i => i == _dataContext.Interval));
        FromDateTime.SelectedDate = _dataContext!.fromDateTime;
        ToDateTime.SelectedDate = _dataContext!.toDateTime;

        await _dataContext.GetCoinData();
        
        RenderPriceChart();
    }

    public void RenderPriceChart()
    {
        var priceHistory = _dataContext!.PriceHistory;
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
        var signalPlot = CoinGraph.Plot.AddSignalXY(dates, prices);
        signalPlot.FillBelow(System.Drawing.Color.Blue, System.Drawing.Color.Transparent);

        var visiblePart = priceHistory.Where(ph => _dataContext.toDateTime.AddDays(-1) < ph.DateTime
                                                   && ph.DateTime < _dataContext.toDateTime ).ToArray();
        decimal minVisible = visiblePart.Min(ph => ph.PriceUsd);
        decimal maxVisible = visiblePart.Max(ph => ph.PriceUsd);

        /* X Axis Limits */
        CoinGraph.Plot.XAxis.DateTimeFormat(true);
        CoinGraph.Plot.XAxis.SetBoundary(dates[0], dates[^1]);
        CoinGraph.Plot.SetAxisLimitsX(visiblePart.Min(ph => ph.DateTime).ToOADate(),
                                      visiblePart.Max(ph => ph.DateTime).ToOADate());
        
        /* Y Axis Limits */
        CoinGraph.Plot.YAxis.SetBoundary(0, double.PositiveInfinity);


        CoinGraph.Plot.SetAxisLimitsY((double)(minVisible - (maxVisible - minVisible)),
                                      (double)(maxVisible + (maxVisible - minVisible)));

        CoinGraph.Render();
    }

    private async void SubmitGraphRequest_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _dataContext!.fromDateTime = FromDateTime.SelectedDate ?? DateTime.Now.AddDays(-5);
        _dataContext!.toDateTime = ToDateTime.SelectedDate ?? DateTime.Now;
        _dataContext!.Interval = (PriceInterval)Intervals.SelectedItem;

        await _dataContext.GetCoinHistoryData();
        RenderPriceChart();
    }
}
