using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tour_Planner.HostBuilder;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private readonly IHost _host;

        public App() {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(config => {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .AddViewModels()
                .AddMainWindow()
                .AddServices()
                .AddBusinessLayer()
                .AddDataAccessLayer()
                .AddDbContext()
                .Build();
        }

        private void App_OnStartup(object sender, StartupEventArgs e) {
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            MainWindow.Show();
        }

        private void App_OnExit(object sender, ExitEventArgs e) {
            _host.Dispose();
        }
    }
}