using System.Windows;
using DCT_CryptoMonitor.Desktop.MVVM.ViewModels;

namespace DCT_CryptoMonitor.Desktop.MVVM.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    
    public MainWindow(MainViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = viewModel;
        InitializeComponent();
    }

    protected override void OnInitialized(EventArgs e)
    {
        _viewModel.Navigation.NavigateTo<HomeViewModel>();
        base.OnInitialized(e);
    }
}