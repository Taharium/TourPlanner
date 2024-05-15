using System.Windows;

namespace Tour_Planner.Stores.WindowStores;

public class WindowStore : IWindowStore {
    public Window? CurrentWindow { get; set; }

    public void Close() {
        CurrentWindow?.Close();
        CurrentWindow = null;
    }
}