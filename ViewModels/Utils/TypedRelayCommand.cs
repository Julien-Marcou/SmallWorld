using System;
using System.Windows.Input;

namespace SmallWorld.ViewModels.Utils
{
    public class TypedRelayCommand<T> : ICommand
    {
        private readonly Action<T> action;
        private readonly Func<T, bool> condition;
        public event EventHandler CanExecuteChanged;

        public TypedRelayCommand(Action<T> action) : this(action, o => true)
        {
        }

        public TypedRelayCommand(Action<T> action, bool condition) : this(action, o => condition)
        {
        }

        public TypedRelayCommand(Action<T> action, Func<T, bool> condition)
        {
            this.action = action;
            this.condition = condition;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        public bool CanExecute(T parameter)
        {
            return condition.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            Execute((T)parameter);
        }

        public void Execute(T parameter)
        {
            if (CanExecute(parameter))
            {
                action.Invoke(parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}