using System.Windows;

namespace Tour_Planner.WindowsWPF {
    /// <summary>
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window {
        public string TitleWindow { get; set; }
        public string Message { get; set; }
        public new bool DialogResult { get; set; }

        public ConfirmationWindow(string message, string title) {
            InitializeComponent();
            TitleWindow = title;
            Message = message;
            DataContext = this;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Window parentWindow = Window.GetWindow(this);
            parentWindow.DialogResult = true;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
            Window parentWindow = Window.GetWindow(this);
            parentWindow.DialogResult = false;
        }
    }
}
