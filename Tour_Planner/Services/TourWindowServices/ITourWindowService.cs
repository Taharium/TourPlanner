using System.Windows;
using Models;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Services.TourWindowServices;

public interface ITourWindowService<TViewModel, TWindow, TTour>  where TViewModel : ViewModelBase where TWindow : Window where TTour : Tour {
    //void Show();
    void ShowDialog();
}