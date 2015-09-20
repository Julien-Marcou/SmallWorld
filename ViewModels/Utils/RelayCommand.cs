using System;
using System.Windows.Input;

namespace SmallWorld.ViewModels.Utils
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Func<object, bool> condition;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action action) : this(o => action(), o => true)
        {
        }

        public RelayCommand(Action action, bool condition) : this(o => action(), o => condition)
        {
        }

        public RelayCommand(Action action, Func<bool> condition) : this(o => action(), o => condition())
        {
        }

        public RelayCommand(Action<object> action) : this(action, o => true)
        {
        }

        public RelayCommand(Action<object> action, bool condition) : this(action, o => condition)
        {
        }

        public RelayCommand(Action<object> action, Func<object, bool> condition)
        {
            this.action = action;
            this.condition = condition;
        }

        public bool CanExecute()
        {
            return CanExecute(null);
        }

        public bool CanExecute(object parameter)
        {
            return condition.Invoke(parameter);
        }

        public void Execute()
        {
            Execute(null);
        }

        public void Execute(object parameter)
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