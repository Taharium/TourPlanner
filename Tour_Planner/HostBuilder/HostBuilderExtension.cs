using BusinessLayer;
using DataAccessLayer;
using DataAccessLayer.ToursRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Tour_Planner.ReportGeneration;
using Tour_Planner.Services.AddTourServices;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.OpenFileDialogServices;
using Tour_Planner.Services.SaveFileDialogServices;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.Stores.TourLogStores;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.HostBuilder;

public static class HostBuilderExtension {
    public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<TourListVM>();
            services.AddSingleton<SearchbarVM>();
            services.AddSingleton<TabControlVM>();
            services.AddSingleton<TourLogsVM>();
            services.AddSingleton<MenuVM>();
        });
        return hostBuilder;
    }

    public static IHostBuilder AddMainWindow(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            services.AddSingleton(s => new MainWindow() {
                DataContext = s.GetRequiredService<MainWindowVM>(),
                TourList = { DataContext = s.GetRequiredService<TourListVM>() },
                Searchbar = { DataContext = s.GetRequiredService<SearchbarVM>() },
                TabControl = { DataContext = s.GetRequiredService<TabControlVM>() },
                TourLogs = { DataContext = s.GetRequiredService<TourLogsVM>() },
                TopMenu = { DataContext = s.GetRequiredService<MenuVM>() }
            });
        });
        return hostBuilder;
    }

    public static IHostBuilder AddBusinessLayer(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            services.AddSingleton<IBusinessLogicTours, BusinessLogicImp>();
            services.AddSingleton<IBusinessLogicTourLogs, BusinessLogicImp>();
            services.AddSingleton<IPdfReportGeneration, PdfReportGeneration>();
            services.AddTransient<IToursRepository, ToursRepository>();
            services.AddTransient<IUnitofWork, UnitofWork>();
            services.AddSingleton<IAddTourService, AddTourService>();
        });
        return hostBuilder;
    }

    public static IHostBuilder AddServices(this IHostBuilder hostBuilder, IConfiguration configuration) {
        hostBuilder.ConfigureServices(services => {
            services.AddSingleton<IOpenFileDialogService, OpenFileDialogService>();
            services.AddSingleton<ISaveFileDialogService, SaveFileDialogService>();
            services.AddSingleton<IMessageBoxService, MessageBoxService>();
            services.AddSingleton<IWindowStore, WindowStore>();
            services.AddSingleton<ITourStore, TourStore>();
            services.AddSingleton<ITourLogStore, TourLogStore>();
            services.AddSingleton(typeof(IWindowService<,>), typeof(WindowService<,>));
            services.AddTransient<AddTourWindowVM>();
            services.AddTransient<AddTourWindow>();
            services.AddSingleton<Func<AddTourWindowVM>>(s => s.GetRequiredService<AddTourWindowVM>);
            services.AddSingleton<Func<AddTourWindow>>(s => s.GetRequiredService<AddTourWindow>);
            services.AddTransient<AddTourLogWindowVM>();
            services.AddTransient<AddTourLogWindow>();
            services.AddSingleton<Func<AddTourLogWindowVM>>(s => s.GetRequiredService<AddTourLogWindowVM>);
            services.AddSingleton<Func<AddTourLogWindow>>(s => s.GetRequiredService<AddTourLogWindow>);
            //services.AddSingleton<IWindowService<AddTourWindowVM, AddTourWindow>, WindowService<AddTourWindowVM, AddTourWindow>>();
            services.AddTransient<ImportTourWindowVM>();
            services.AddTransient<ImportTourWindow>();
            services.AddSingleton<Func<ImportTourWindowVM>>(s => s.GetRequiredService<ImportTourWindowVM>);
            services.AddSingleton<Func<ImportTourWindow>>(s => s.GetRequiredService<ImportTourWindow>);
            //services.AddSingleton<IWindowService<ImportTourWindowVM, ImportTourWindow>, WindowService<ImportTourWindowVM, ImportTourWindow>>();
            services.AddTransient<ExportTourWindowVM>();
            services.AddTransient<ExportTourWindow>();
            services.AddSingleton<Func<ExportTourWindowVM>>(s => s.GetRequiredService<ExportTourWindowVM>);
            services.AddSingleton<Func<ExportTourWindow>>(s => s.GetRequiredService<ExportTourWindow>);
            //services.AddSingleton<IWindowService<ExportTourWindowVM, ExportTourWindow>, WindowService<ExportTourWindowVM, ExportTourWindow>>();
            services.AddTransient<EditTourWindow>();
            services.AddTransient<EditTourWindowVM>();
            services.AddSingleton<Func<EditTourWindowVM>>(s => s.GetRequiredService<EditTourWindowVM>);
            services.AddSingleton<Func<EditTourWindow>>(s => s.GetRequiredService<EditTourWindow>);
            services.AddTransient<EditTourLogWindow>();
            services.AddTransient<EditTourLogWindowVM>();
            services.AddSingleton<Func<EditTourLogWindowVM>>(s => s.GetRequiredService<EditTourLogWindowVM>);
            services.AddSingleton<Func<EditTourLogWindow>>(s => s.GetRequiredService<EditTourLogWindow>);
            /*services.AddSingleton<IConfigDatabase, AppConfiguration>(s => new AppConfiguration(configuration));*/
        });
        return hostBuilder;
    }

    public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            //dotnet ef migrations add <MigrationName>
            services.AddDbContext<TourPlannerDbContext>();
        });
        return hostBuilder;
    }
}