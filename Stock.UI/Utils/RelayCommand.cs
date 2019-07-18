using System;
using System.Windows.Input;

namespace Stock.UI.Utils
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> action)
            : this(action, null)
        {
        }

        public RelayCommand(Action<object> action, Predicate<object> canExecute)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null || !(parameter is object)) return true;

            var result = _canExecute?.Invoke((object)parameter) ?? true;

            return result;
        }

        public void Execute(object parameter)
        {
            _action((object)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
