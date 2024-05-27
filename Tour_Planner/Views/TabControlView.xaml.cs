using System;
using System.Windows;
using System.Windows.Controls;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Views
{
    /// <summary>
    /// Interaction logic for TabControlView.xaml
    /// </summary>
    public partial class TabControlView : UserControl
    {
        private readonly string _filepath;
        
        public TabControlView()
        {
            InitializeComponent();
            
            _filepath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/Resource/leaflet.html");

            DataContextChanged += SetEventHandlers;
            
            InitializeAsync();
        }
        
        private void SetEventHandlers(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(DataContext is not TabControlVM) return;
            var vm = (TabControlVM)DataContext;
            vm.UpdatedRoute += UpdateMap;
        }

        private void UpdateMap()
        {
            webView.CoreWebView2.Navigate(_filepath);
        }

        private async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.Navigate(_filepath);
        }
    }
}
