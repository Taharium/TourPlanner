using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using BusinessLayer;
using DataAccessLayer;
using DataAccessLayer.TourLogsRepository;
using DataAccessLayer.ToursRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tour_Planner.Configurations;
using Tour_Planner.HostBuilder;
using Tour_Planner.Services;
using Tour_Planner.Services.AddTourServices;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.OpenFileDialogServices;
using Tour_Planner.Services.OpenFolderDialogServices;
using Tour_Planner.Services.PdfReportGenerationServices;
using Tour_Planner.Services.SaveFileDialogServices;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.Stores.TourLogStores;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
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
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .AddViewModels()
                .AddMainWindow()
                .AddBusinessLayer()
                .AddServices()
                .AddDbContext()
                .Build();
        }
        private void App_OnStartup(object sender, StartupEventArgs e) {
            
            //Copy if newer in properties for appesettings.json
            //var s = configuration.GetConnectionString("DataBase");
            //Debugger.Break();
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
            _host.Dispose();
        }
    }
}
