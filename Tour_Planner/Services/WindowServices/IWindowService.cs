using System.Windows;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Services.WindowServices;

public interface IWindowService<TViewModel, TWindow>  where TViewModel : ViewModelBase where TWindow : Window {
    void ShowDialog();
}