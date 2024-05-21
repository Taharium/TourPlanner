using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tour_Planner.ViewModels;

public class AsyncRelayCommand(Func<object?, Task> execute, Predicate<object?>? canExecute) : ICommand
{
    private readonly Func<object?, Task> _execute = execute;
    private readonly Predicate<object?>? _canExecute = canExecute;

    private bool _isExecuting = false;
    public bool IsExecuting
    {
        get => _isExecuting;
        set
        {
            _isExecuting = value;
            OnExecuteChanged();
        }
    }

    public event EventHandler? CanExecuteChanged;

    public AsyncRelayCommand(Func<object?, Task> execute)
        : this(execute, null) { }

    public bool CanExecute(object? parameter)
    {
        return !IsExecuting && (_canExecute == null || _canExecute(parameter));
    }

    public async void Execute(object? parameter)
    {
        IsExecuting = true;

        try
        {
            await _execute(parameter);
        }
        finally
        {
            IsExecuting = false;
        }
    }

    public void OnExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}