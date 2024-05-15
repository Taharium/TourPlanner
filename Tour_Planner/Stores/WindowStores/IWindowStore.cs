using System.Windows;

namespace Tour_Planner.Stores.WindowStores;

public interface IWindowStore {
    Window? CurrentWindow { get; set; }
    void Close();
}