using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace Tour_Planner.Behavior;

public class WindowClosingBehavior : Behavior<Window> {
    public static readonly DependencyProperty ClosingCommandProperty =
        DependencyProperty.RegisterAttached(
            "ClosingCommand",
            typeof(ICommand),
            typeof(WindowClosingBehavior),
            new PropertyMetadata(null, OnClosingCommandChanged));

    public static void SetClosingCommand(DependencyObject obj, ICommand value) {
        obj.SetValue(ClosingCommandProperty, value);
    }

    public static ICommand GetClosingCommand(DependencyObject obj) {
        return (ICommand)obj.GetValue(ClosingCommandProperty);
    }

    private static void OnClosingCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (d is Window window) {
            if (e.OldValue != null) {
                window.Closing -= Window_Closing;
            }

            if (e.NewValue != null) {
                window.Closing += Window_Closing;
            }
        }
    }

    private static void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
        if (sender is Window window) {
            var command = GetClosingCommand(window);
            if (command.CanExecute(null)) {
                command.Execute(null);
            }
        }
    }
}