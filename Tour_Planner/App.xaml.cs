using System.Windows;
using Tour_Planner.ViewModels;

namespace Tour_Planner {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void App_OnStartup(object sender, StartupEventArgs e) {
            TourListVM tourListVM = new TourListVM();
            SearchbarVM searchbarVM = new SearchbarVM();
            MainWindowVM mainWindowVM = new MainWindowVM(tourListVM, searchbarVM);

            MainWindow mainWindow = new() {
                DataContext = mainWindowVM,
                TourList = { DataContext = tourListVM },
                Searchbar = { DataContext = searchbarVM }
            };

            mainWindow.Show();
        }
    }
}
