using BusinessLayer;
using Microsoft.Extensions.DependencyInjection;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Services;

public class IoCContainerConfig {

    private readonly ServiceProvider _serviceProvider;
    
    public IoCContainerConfig() {
        var services = new ServiceCollection();
        services.AddSingleton<MainWindowVM>();
        _serviceProvider = services.BuildServiceProvider();
    }
    
    public MainWindowVM MainWindowVM => _serviceProvider.GetRequiredService<MainWindowVM>();
    
}