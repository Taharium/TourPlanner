using System;
using BusinessLayer;
using BusinessLayer.Services.AddTourServices;
using DataAccessLayer;
using DataAccessLayer.ToursRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.PdfReportGenerationServices;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.Services;

public class IoCContainerConfig {

    private ServiceProvider _serviceProvider = null!;
    
    private readonly ServiceCollection _services = new();
    
    public IoCContainerConfig() {
       
        _services.AddSingleton<IBusinessLogicTours, BusinessLogicImp>();
        _services.AddSingleton<IBusinessLogicTourLogs, BusinessLogicImp>();
        _services.AddSingleton<IPdfReportGenerationService, PdfReportGenerationService>();
        _services.AddTransient<IToursRepository, ToursRepository>();
        _services.AddTransient<IUnitofWork, UnitofWork>();
        _services.AddSingleton<IAddTourService, AddTourService>();
    }

    public void AddServices()
    {
        _services.AddSingleton<IMessageBoxService, MessageBoxService>();
        _services.AddSingleton<IWindowStore, WindowStore>();
        _services.AddTransient<AddTourWindowVM>();
        _services.AddTransient<AddTourWindow>();
        _services.AddSingleton<Func<AddTourWindowVM>>(s => s.GetRequiredService<AddTourWindowVM>);
        _services.AddSingleton<Func<AddTourWindow>>(s => s.GetRequiredService<AddTourWindow>);
        _services.AddSingleton<IWindowService<AddTourWindowVM, AddTourWindow>, WindowService<AddTourWindowVM, AddTourWindow>>();
    }

    public void AddVMs()
    {
        _services.AddSingleton<MainWindowVM>();
        _services.AddSingleton<TourListVM>();
        _services.AddSingleton<SearchbarVM>();
        _services.AddSingleton<TabControlVM>();
        _services.AddSingleton<TourLogsVM>();
    }
    
    public void AddDbContext(IConfiguration configuration)
    {
        _services.AddDbContext<TourPlannerDbContext>();
    }

    public void Build()
    {
        _serviceProvider = _services.BuildServiceProvider();
    }
    
    public MainWindowVM MainWindowVm => _serviceProvider.GetRequiredService<MainWindowVM>();
    public TourListVM TourListVm => _serviceProvider.GetRequiredService<TourListVM>();
    public SearchbarVM SearchbarVm => _serviceProvider.GetRequiredService<SearchbarVM>();
    public TabControlVM TabControlVm => _serviceProvider.GetRequiredService<TabControlVM>();
    public TourLogsVM TourLogsVm => _serviceProvider.GetRequiredService<TourLogsVM>();
    
    
}