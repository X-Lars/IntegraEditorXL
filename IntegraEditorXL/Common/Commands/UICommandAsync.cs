using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IntegraEditorXL.Common.Commands
{
    public class UICommandAsync : ICommand
    {
        #region Fields

        /// <summary>
        /// Reference to the function to execute.
        /// </summary>
        private readonly Func<Task> _Execute;

        /// <summary>
        /// Reference to the can execute conditions function.
        /// </summary>
        private readonly Func<bool> _CanExecute;

        /// <summary>
        /// Tracks wheter the command is currently executing.
        /// </summary>
        private bool _IsExecuting;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the execution conditions are changed.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new awaitable command.
        /// </summary>
        /// <param name="execute">The function to excecute on command invokation.</param>
        internal UICommandAsync(Func<Task> execute) : this(execute, null, null) { }

        /// <summary>
        /// Creates a new awaitable command with validation.
        /// </summary>
        /// <param name="execute">The function to execute on command invokation.</param>
        /// <param name="canExecute">The function to validate if the command can be executed.</param>
        /// <param name="provider">Optional property change provider to reevalute if the command can be executed.</param>
        internal UICommandAsync(Func<Task> execute, Func<bool> canExecute, INotifyPropertyChanged provider = null, string property = null)
        {
            _Execute = execute;
            _CanExecute = canExecute;

            if (provider != null && canExecute != null)
            {
                _TrackedProperty = property;
                provider.PropertyChanged += PropertyChanged;
            }
        }

        private string _TrackedProperty = null;

        #endregion

        #region Methods

        /// <summary>
        /// Raises the can execute changed event when a property of the associated model is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_TrackedProperty != null && e.PropertyName == _TrackedProperty)
            {
                Action raiseChange = () => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                Task.Run(() => raiseChange);
            }
            //CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets whether the function can be executed.
        /// </summary>
        /// <param name="parameter">Unused.</param>
        /// <returns>True if the command can be executed, false otherwise.</returns>
        public bool CanExecute(object parameter)
        {
            if (_IsExecuting)
                return false;

            return _CanExecute == null ? true : _CanExecute();
        }

        /// <summary>
        /// Executes the function associated with the command.
        /// </summary>
        /// <param name="parameter">Unused.</param>
        public async void Execute(object parameter)
        {
            _IsExecuting = true;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            await _Execute();

            _IsExecuting = false;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
