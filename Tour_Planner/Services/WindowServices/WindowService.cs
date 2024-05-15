using System;
using System.Windows;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;


namespace Tour_Planner.Services.WindowServices;

public class WindowService<TViewModel, TWindow> : IWindowService<TViewModel, TWindow>
    where TViewModel : ViewModelBase where TWindow : Window {

    private readonly Func<TViewModel> _createViewModel;
    private readonly Func<TWindow> _createWindow;
    private readonly IWindowStore _windowStore;

    public WindowService(Func<TViewModel> viewmodel, Func<TWindow> window, IWindowStore windowStore) {
        _createViewModel = viewmodel;
        _createWindow = window;
        _windowStore = windowStore;
    }
    
    /*public void Show() {
        var window = Init();
        window.Show();
    }*/

    public void ShowDialog() {
        var viewModel = _createViewModel();
        var window = _createWindow();
        window.DataContext = viewModel;
        _windowStore.CurrentWindow = window;
        window.ShowDialog();
    }
}