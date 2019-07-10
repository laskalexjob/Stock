using System;
using System.Windows.Input;

namespace Stock.UI.Utils
{
    public class CustomCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _canExecute;

        public CustomCommand(Action<object> action)
            : this(action, null)
        {
        }

        public CustomCommand(Action<object> action, Predicate<object> canExecute)
        {
            this._action = action ?? throw new ArgumentNullException(nameof(action));
            this._canExecute = canExecute;
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
