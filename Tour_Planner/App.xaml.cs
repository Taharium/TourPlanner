using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Tour_Planner.Configurations;
using Tour_Planner.Services;
using Tour_Planner.ViewModels;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void App_OnStartup(object sender, StartupEventArgs e) {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var appConfiguration = new AppConfiguration(configuration);
            
            IoCContainerConfig ioCContainerConfig = (IoCContainerConfig?)Current.Resources["IoCConfig2"] ?? throw new NullReferenceException();
            
            
            MainWindow mainWindow = new() {
                DataContext = ioCContainerConfig.MainWindowVm,
                TourList = { DataContext = ioCContainerConfig.TourListVm },
                Searchbar = { DataContext = ioCContainerConfig.SearchbarVm },
                TabControl = { DataContext = ioCContainerConfig.TabControlVm},
                TourLogs = { DataContext = ioCContainerConfig.TourLogsVm }
            };

            mainWindow.Show();
        }

        private void App_OnExit(object sender, ExitEventArgs e) {
            // Clean up code
        }
    }
}
