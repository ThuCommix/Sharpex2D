using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ContentPipelineUI.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _predicate;

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The Parameter.</param>
        public void Execute(object parameter)
        {
            _action.Invoke(parameter);
        }

        /// <summary>
        /// A value indicating whether the command can be executed.
        /// </summary>
        /// <param name="parameter">The Parameter.</param>
        /// <returns>True if can execute.</returns>
        public bool CanExecute(object parameter)
        {
            return _predicate == null || _predicate.Invoke(parameter);
        }

        /// <summary>
        /// Initializes a new RelayCommand class.
        /// </summary>
        /// <param name="action">The Action.</param>
        /// <param name="predicate">The Predicate.</param>
        public RelayCommand(Action<object> action, Func<object, bool> predicate = null)
        {
            _action = action;
            _predicate = predicate;
        }

        /// <summary>
        /// Raises when the can execute changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}

