using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IntegraEditorXL.Common.Commands
{
    /// <summary>
    /// Defines a model command for UI binding.
    /// </summary>
    public class UICommandParameterized<TParameter> : ICommand
    {
        #region Fields

        /// <summary>
        /// Stores a reference to the method to execute.
        /// </summary>
        private Action<TParameter> _Execute;

        /// <summary>
        /// Stores a reference to the execution conditions function.
        /// </summary>
        private Func<TParameter, bool> _CanExecute;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the execution conditions are changed.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new command that can be executed without validation.
        /// </summary>
        /// <param name="execute">The method to excecute on command invokation.</param>
        public UICommandParameterized(Action<TParameter> execute) : this(execute, null) { }

        /// <summary>
        /// Creates a new command that can be validated before execution.
        /// </summary>
        /// <param name="execute">The method to execute on command invokation.</param>
        /// <param name="canExecute">The function to validate if the command can be executed.</param>
        /// <param name="instance">The instance that provides the property changed event.</param>
        public UICommandParameterized(Action<TParameter> execute, Func<TParameter, bool> canExecute)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets whether the command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True if the command can be executed, false otherwise.</returns>
        public bool CanExecute(object parameter)
        {
            return _CanExecute == null ? true : _CanExecute((TParameter)parameter);
        }

        /// <summary>
        /// Executes the method associated with the command.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _Execute((TParameter)parameter);
        }

        #endregion
    }
}
