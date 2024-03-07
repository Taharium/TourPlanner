using System;
using System.Windows.Input;

namespace Tour_Planner {
    internal class RelayCommand : ICommand {

        private readonly Action<object> _execute;
        private readonly Predicate<object>? _canExecute;
        //private readonly Func<object, bool> _canExecute;

        /*public RelayCommand(Action<object> execute, Func<object, bool> canExecute) {
            _execute = execute;
            _canExecute = canExecute;
        }*/
        public RelayCommand(Action<object> execute, Predicate<object?> canExecute) {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute) {
            _execute = execute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object parameter) {
            //return _canExecute(parameter);
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter) {
            _execute(parameter);
        }
    }
}
