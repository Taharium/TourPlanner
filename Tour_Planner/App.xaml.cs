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
                .ConfigureServices((hostContext, services) =>
                {
                     /*services.AddSingleton<IPdfReportGenerationService, PdfReportGenerationService>();
            services.AddSingleton<IOpenFolderDialogService, OpenFolderDialogService>();
            services.AddSingleton<IOpenFileDialogService, OpenFileDialogService>();
            services.AddSingleton<ISaveFileDialogService, SaveFileDialogService>();
            services.AddSingleton<IMessageBoxService, MessageBoxService>();*/
            
            /*services.AddSingleton<IWindowStore, WindowStore>();*/
            /*services.AddSingleton<ITourStore, TourStore>();
            services.AddSingleton<ITourLogStore, TourLogStore>();*/
            
            /*
            services.AddSingleton(typeof(IWindowService<,>), typeof(WindowService<,>));
            
            services.AddTransient<AddTourWindowVM>();
            services.AddTransient<AddTourWindow>();
            services.AddSingleton<Func<AddTourWindowVM>>(s => s.GetRequiredService<AddTourWindowVM>);
            services.AddSingleton<Func<AddTourWindow>>(s => s.GetRequiredService<AddTourWindow>);
            
            services.AddTransient<AddTourLogWindowVM>();
            services.AddTransient<AddTourLogWindow>();
            services.AddSingleton<Func<AddTourLogWindowVM>>(s => s.GetRequiredService<AddTourLogWindowVM>);
            services.AddSingleton<Func<AddTourLogWindow>>(s => s.GetRequiredService<AddTourLogWindow>);
            
            services.AddTransient<ImportTourWindowVM>();
            services.AddTransient<ImportTourWindow>();
            services.AddSingleton<Func<ImportTourWindowVM>>(s => s.GetRequiredService<ImportTourWindowVM>);
            services.AddSingleton<Func<ImportTourWindow>>(s => s.GetRequiredService<ImportTourWindow>);

            services.AddTransient<ExportTourWindowVM>();
            services.AddTransient<ExportTourWindow>();
            services.AddSingleton<Func<ExportTourWindowVM>>(s => s.GetRequiredService<ExportTourWindowVM>);
            services.AddSingleton<Func<ExportTourWindow>>(s => s.GetRequiredService<ExportTourWindow>);

            services.AddTransient<EditTourWindow>();
            services.AddTransient<EditTourWindowVM>();
            services.AddSingleton<Func<EditTourWindowVM>>(s => s.GetRequiredService<EditTourWindowVM>);
            services.AddSingleton<Func<EditTourWindow>>(s => s.GetRequiredService<EditTourWindow>);*/
            
            /*services.AddTransient<EditTourLogWindow>();
            services.AddTransient<EditTourLogWindowVM>();
            services.AddSingleton<Func<EditTourLogWindowVM>>(s => s.GetRequiredService<EditTourLogWindowVM>);
            services.AddSingleton<Func<EditTourLogWindow>>(s => s.GetRequiredService<EditTourLogWindow>);
            
            services.AddTransient<GeneratePdfWindow>();
            services.AddTransient<GeneratePdfWindowVM>();
            services.AddSingleton<Func<GeneratePdfWindowVM>>(s => s.GetRequiredService<GeneratePdfWindowVM>);
            services.AddSingleton<Func<GeneratePdfWindow>>(s => s.GetRequiredService<GeneratePdfWindow>);
            services.AddSingleton<IConfigDatabase, AppConfiguration>(s => new AppConfiguration(hostContext.Configuration));*/
            /*services.AddSingleton<IConfigOpenRouteService, AppConfiguration>(s => new AppConfiguration(hostContext.Configuration));
            services.AddSingleton<IBusinessLogicTours, BusinessLogicImp>();
            services.AddSingleton<IBusinessLogicTourLogs, BusinessLogicImp>();
            services.AddSingleton<IOpenRouteService, BusinessLogicOpenRouteService>();
            services.AddTransient<IToursRepository, ToursRepository>();
            services.AddTransient<ITourLogsRepository, TourLogsRepository>();
            services.AddTransient<IUnitofWork, UnitofWork>();
            services.AddSingleton<IAddTourService, AddTourService>();
            services.AddSingleton(s => new MainWindow() {
            DataContext = s.GetRequiredService<MainWindowVM>(),
            TourList = { DataContext = s.GetRequiredService<TourListVM>() },
            Searchbar = { DataContext = s.GetRequiredService<SearchbarVM>() },
            TabControl = { DataContext = s.GetRequiredService<TabControlVM>() },
            TourLogs = { DataContext = s.GetRequiredService<TourLogsVM>() },
            TopMenu = { DataContext = s.GetRequiredService<MenuVM>() }
            });
                services.AddSingleton<MainWindowVM>();
                services.AddSingleton<TourListVM>();
                services.AddSingleton<SearchbarVM>();
                services.AddSingleton<TabControlVM>();
                services.AddSingleton<TourLogsVM>();
                services.AddSingleton<MenuVM>();*/
                    // DB Context
                    services.AddDbContext<TourPlannerDbContext>(options =>
                        options.UseNpgsql(hostContext.Configuration.GetConnectionString("DataBase")));
                })
                .Build();
            /*.AddViewModels()
            .AddBusinessLayer()
            .AddServices(configuration)
            .AddMainWindow()
            /*.AddDbContext(configu)#1#
            .Build();*/
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
