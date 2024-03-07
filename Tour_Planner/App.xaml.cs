using System.Windows;

namespace Tour_Planner {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void App_OnStartup(object sender, StartupEventArgs e) {
            MainWindow mainWindow = new MainWindow();
            TourViewModel tourViewModel = new TourViewModel();
            mainWindow.DataContext = tourViewModel;
            mainWindow.Show();
        }
    }
}
