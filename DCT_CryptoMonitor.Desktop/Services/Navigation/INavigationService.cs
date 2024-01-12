using DCT_CryptoMonitor.Desktop.Core;

namespace DCT_CryptoMonitor.Desktop.Services.Navigation;
public interface INavigationService
{
    ViewModel CurrentView { get; }
    void NavigateTo<TViewModel>() where TViewModel : ViewModel;
}
