using System;
using System.Windows;
using System.Windows.Controls;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Views {
    /// <summary>
    /// Interaction logic for TabControlView.xaml
    /// </summary>
    public partial class TabControlView : UserControl {
        private readonly string _filepath = "";

        private bool _isInitialized = false;

        public TabControlView() {
            InitializeComponent();
            try {
                _filepath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Assets/Resource/leaflet.html");
            }
            catch (Exception) {
                MessageBox.Show("Failed to get path to leaflet.html", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            DataContextChanged += SetEventHandlers;
            Loaded += TabControlView_Loaded;
        }

        private void SetEventHandlers(object sender, DependencyPropertyChangedEventArgs e) {
            if (DataContext is not TabControlVM) return;
            var vm = (TabControlVM)DataContext;
            vm.UpdatedRoute += UpdateMap;
        }

        private void UpdateMap() {
            if (_isInitialized && _filepath != "") {
                webView2.CoreWebView2.Navigate(_filepath);
            }
        }

        private async void InitializeAsync() {
            await webView2.EnsureCoreWebView2Async(null);
            _isInitialized = true;
            webView2.CoreWebView2.Navigate(_filepath);
        }

        private void TabControlView_Loaded(object sender, RoutedEventArgs e) {
            InitializeAsync();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (((TabControl)sender).SelectedIndex == 1) {
                if (webView2.Parent is Panel parentPanel) {
                    parentPanel.Children.Remove(webView2);
                }

                if (RouteTabPlaceholder.Children.Count == 0) {
                    RouteTabPlaceholder.Children.Add(webView2);
                    webView2.Visibility = Visibility.Visible;
                }
            }
            else {
                webView2.Visibility = Visibility.Collapsed;
            }
        }
    }
}