using System.Windows.Input;

namespace Tour_Planner {
    internal class ExitCommand : ICommand {
        public event System.EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) {
            return true;
        }

        public void Execute(object? parameter) {
            System.Environment.Exit(0);
        }
    }
}
