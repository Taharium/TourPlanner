using System;
using BusinessLayer;
using Microsoft.Extensions.DependencyInjection;
using Tour_Planner.ReportGeneration;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.Services;

public class IoCContainerConfig {

    private readonly ServiceProvider _serviceProvider;
    
    public IoCContainerConfig() {
        var services = new ServiceCollection();
        services.AddSingleton<MainWindowVM>();
        services.AddSingleton<TourListVM>();
        services.AddSingleton<SearchbarVM>();
        services.AddSingleton<TabControlVM>();
        services.AddSingleton<TourLogsVM>();
        services.AddSingleton<IBusinessLogicTours, BusinessLogicImp>();
        services.AddSingleton<IBusinessLogicTourLogs, BusinessLogicImp>();
        services.AddSingleton<IPdfReportGeneration, PdfReportGeneration>();
        services.AddSingleton<IMessageBoxService, MessageBoxService>();
        services.AddSingleton<IWindowStore, WindowStore>();
        services.AddTransient<AddTourWindowVM>();
        services.AddTransient<AddTourWindow>();
        services.AddSingleton<Func<AddTourWindowVM>>(s => s.GetRequiredService<AddTourWindowVM>);
        services.AddSingleton<Func<AddTourWindow>>(s => s.GetRequiredService<AddTourWindow>);
        services.AddSingleton<IWindowService<AddTourWindowVM, AddTourWindow>, WindowService<AddTourWindowVM, AddTourWindow>>();
        _serviceProvider = services.BuildServiceProvider();
    }
    
    public MainWindowVM MainWindowVm => _serviceProvider.GetRequiredService<MainWindowVM>();
    public TourListVM TourListVm => _serviceProvider.GetRequiredService<TourListVM>();
    public SearchbarVM SearchbarVm => _serviceProvider.GetRequiredService<SearchbarVM>();
    public TabControlVM TabControlVm => _serviceProvider.GetRequiredService<TabControlVM>();
    public TourLogsVM TourLogsVm => _serviceProvider.GetRequiredService<TourLogsVM>();
    
    
}