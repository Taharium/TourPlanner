using System;
using System.Windows;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tour_Planner.Configurations;
using Tour_Planner.HostBuilder;
using Tour_Planner.Services;
using Tour_Planner.ViewModels;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private readonly IHost _host;
        public App() {
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .AddServices()
                .ConfigureServices((hostContext, services) => {
                    string connectionString = hostContext.Configuration.GetConnectionString("DataBase")!;
                    //services.AddDbContext<TourPlannerDbContext>();
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainWindowVM>(),
                        TourList = { DataContext = s.GetRequiredService<TourListVM>() },
                        Searchbar = { DataContext = s.GetRequiredService<SearchbarVM>() },
                        TabControl = { DataContext = s.GetRequiredService<TabControlVM>() },
                        TourLogs = { DataContext = s.GetRequiredService<TourLogsVM>() }
                        
                    });
                })
                .Build();
        }
        private void App_OnStartup(object sender, StartupEventArgs e) {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _host.Start();
            /*var appConfiguration = new AppConfiguration(configuration);*/
            
            /*IoCContainerConfig ioCContainerConfig = (IoCContainerConfig?)Current.Resources["IoCConfig2"] ?? throw new NullReferenceException();
            ioCContainerConfig.AddServices();
            ioCContainerConfig.AddVMs();
            ioCContainerConfig.AddDbContext(configuration);
            ioCContainerConfig.Build();*/


            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            
            /*MainWindow mainWindow = new() {
                DataContext = ioCContainerConfig.MainWindowVm,
                TourList = { DataContext = ioCContainerConfig.TourListVm },
                Searchbar = { DataContext = ioCContainerConfig.SearchbarVm },
                TabControl = { DataContext = ioCContainerConfig.TabControlVm},
                TourLogs = { DataContext = ioCContainerConfig.TourLogsVm }
            };*/

            MainWindow.Show();
        }

        private void App_OnExit(object sender, ExitEventArgs e) {
            //_host.Dispose();
        }
    }
}
