using System;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.WindowServices;
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
        });
        return hostBuilder;
    }

    public static IHostBuilder AddServices(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            services.AddSingleton<IMessageBoxService, MessageBoxService>();
            services.AddSingleton<IWindowStore, WindowStore>();
            services.AddTransient<AddTourWindowVM>();
            services.AddTransient<AddTourWindow>();
            services.AddSingleton<Func<AddTourWindowVM>>(s => s.GetRequiredService<AddTourWindowVM>);
            services.AddSingleton<Func<AddTourWindow>>(s => s.GetRequiredService<AddTourWindow>);
            services.AddSingleton<IWindowService<AddTourWindowVM, AddTourWindow>, WindowService<AddTourWindowVM, AddTourWindow>>();
        });
        return hostBuilder;
    }
    
    public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            services.AddDbContext<TourPlannerDbContext>();
        });
        return hostBuilder;
    }
}