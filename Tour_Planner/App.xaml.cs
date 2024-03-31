using System.Windows;
using Tour_Planner.ViewModels;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void App_OnStartup(object sender, StartupEventArgs e) {
            TourListVM tourListVM = new TourListVM();
            SearchbarVM searchbarVM = new SearchbarVM();
            TabControlVM tabControlVM = new TabControlVM();
            TourLogsVM tourLogsVM = new TourLogsVM();
            MainWindowVM mainWindowVM = new MainWindowVM(tourListVM, searchbarVM, tabControlVM, tourLogsVM);

            MainWindow mainWindow = new() {
                DataContext = mainWindowVM,
                TourList = { DataContext = tourListVM },
                Searchbar = { DataContext = searchbarVM },
                TabControl = { DataContext = tabControlVM },
                TourLogs = { DataContext = tourLogsVM }
            };

            mainWindow.Show();
        }

        private void App_OnExit(object sender, ExitEventArgs e) {
            // Clean up code
        }
    }
}
