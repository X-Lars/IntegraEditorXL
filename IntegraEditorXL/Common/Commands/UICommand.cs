using System;
using System.Windows.Input;

namespace IntegraEditorXL.Common.Commands
{
    public class UICommand : ICommand
    {
        #region Fields

        /// <summary>
        /// Reference to the method to execute.
        /// </summary>
        private readonly Action<object> _Execute;

        /// <summary>
        /// Reference to the can execute conditions function.
        /// </summary>
        private readonly Func<object, bool> _CanExecute;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the execution conditions are changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new command with validation.
        /// </summary>
        /// <param name="execute">The method to execute on command invokation.</param>
        /// <param name="canExecute">The function to validate if the command can be executed.</param>
        public UICommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets whether the command can be executed.
        /// </summary>
        /// <param name="parameter">Unused.</param>
        /// <returns>True if the command can be executed, false otherwise.</returns>
        public bool CanExecute(object parameter)
        {
            return _CanExecute == null || _CanExecute(parameter);
        }

        /// <summary>
        /// Executes the method associated with the command.
        /// </summary>
        /// <param name="parameter">Unused.</param>
        public void Execute(object parameter)
        {
            _Execute(parameter);
        }

        #endregion
    }
}
