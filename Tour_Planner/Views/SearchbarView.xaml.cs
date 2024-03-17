using System.Windows.Controls;

namespace Tour_Planner.Views {
    /// <summary>
    /// Interaction logic for SearchbarView.xaml
    /// </summary>
    public partial class SearchbarView : UserControl {
        public SearchbarView() {
            InitializeComponent();
        }

        private string text = "Search...";

        private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e) {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
        }

        private void TextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e) {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "") {
                textBox.Text = text;
            }
        }
    }
}
