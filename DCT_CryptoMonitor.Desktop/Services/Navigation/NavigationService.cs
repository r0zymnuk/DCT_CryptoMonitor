﻿using DCT_CryptoMonitor.Desktop.Core;

namespace DCT_CryptoMonitor.Desktop.Services.Navigation;
public class NavigationService : ObservableObject, INavigationService
{
    private ViewModel _currentView;
    private readonly Func<Type, ViewModel> _viewModelFactory;

    public ViewModel CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public NavigationService(Func<Type, ViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : ViewModel
    {
        ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }
}
