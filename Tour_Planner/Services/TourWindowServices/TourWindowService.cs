using System;
using System.Windows;
using Models;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Services.TourWindowServices;

public class TourWindowService<TViewModel, TWindow, TTour> : ITourWindowService<TViewModel, TWindow, TTour>
    where TViewModel : ViewModelBase where TWindow : Window where TTour : Tour {
    
    private readonly Func<TViewModel> _createViewModel;
    private readonly Func<TWindow> _createWindow;
    private readonly IWindowStore _windowStore;
    private readonly ITourStore _tourStore;

    public TourWindowService(Func<TViewModel> viewmodel, Func<TWindow> window, IWindowStore windowStore, ITourStore tourStore) {
        _createViewModel = viewmodel;
        _createWindow = window;
        _windowStore = windowStore;
        _tourStore = tourStore;
    }
    
    public void ShowDialog() {
        var viewModel = _createViewModel();
        var window = _createWindow();
        window.DataContext = viewModel;
        //_tourStore.CurrentTour = 
        _windowStore.CurrentWindow = window;
        window.ShowDialog();
    }
}