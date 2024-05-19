using System.Windows;
namespace Tour_Planner.Services.MessageBoxServices;

public interface IMessageBoxService {
    MessageBoxResult Show(string message, string caption, MessageBoxButton button, MessageBoxImage image);
    MessageBoxResult Show(string message, string caption, MessageBoxButton button, MessageBoxImage image, MessageBoxResult result, MessageBoxOptions options);
}