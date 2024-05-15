using System.Windows;
namespace Tour_Planner.Services.MessageBoxServices;

public class MessageBoxService : IMessageBoxService {
    public MessageBoxResult Show(string message, string caption, MessageBoxButton button, MessageBoxImage image) {
        return MessageBox.Show(message, caption, button, image);
    }
}