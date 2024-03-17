using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tour_Planner.Behavior {
    public class SearchbarBehavior : Behavior<TextBox> {

        public static readonly DependencyProperty GotFocusCommandProperty =
        DependencyProperty.RegisterAttached("GotFocusCommand", typeof(ICommand), typeof(SearchbarBehavior), new PropertyMetadata(null, OnGotFocusCommandChanged));

        public static ICommand GetGotFocusCommand(DependencyObject obj) {
            return (ICommand)obj.GetValue(GotFocusCommandProperty);
        }

        public static void SetGotFocusCommand(DependencyObject obj, ICommand value) {
            obj.SetValue(GotFocusCommandProperty, value);
        }

        private static void OnGotFocusCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is TextBox textBox) {
                textBox.GotFocus += TextBox_GotFocus;
            }
        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (sender is TextBox textBox) {
                ICommand command = GetGotFocusCommand(textBox);
                command?.Execute(null);
            }
        }

        public static readonly DependencyProperty LostFocusCommandProperty =
            DependencyProperty.RegisterAttached("LostFocusCommand", typeof(ICommand), typeof(SearchbarBehavior), new PropertyMetadata(null, OnLostFocusCommandChanged));

        public static ICommand GetLostFocusCommand(DependencyObject obj) {
            return (ICommand)obj.GetValue(LostFocusCommandProperty);
        }

        public static void SetLostFocusCommand(DependencyObject obj, ICommand value) {
            obj.SetValue(LostFocusCommandProperty, value);
        }

        private static void OnLostFocusCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is TextBox textBox) {
                textBox.LostFocus += TextBox_LostFocus;
            }
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e) {
            if (sender is TextBox textBox) {
                ICommand command = GetLostFocusCommand(textBox);
                command?.Execute(null);
            }
        }
    }
}
