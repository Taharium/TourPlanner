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

        private bool _isInitialized = false;
        
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
            if (_isInitialized) {
                webView.CoreWebView2.Navigate(_filepath);
            }
        }

        private async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            _isInitialized = true;
            webView.CoreWebView2.Navigate(_filepath);
        }
    }
}
