using IntegraXL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IntegraEditorXL.UserControls
{
    
    public class BaseControl : UserControl, INotifyPropertyChanged
    {
        public BaseControl() : base()
        {
            DataContext = this;
            
            AppContext.PropertyChanged += AppContextPropertyChanged;
        }

        /// <summary>
        /// Gets the application context.
        /// </summary>
        public MainWindow AppContext
        {
            get => (MainWindow)Application.Current.MainWindow;
        }

        /// <summary>
        /// Gets the device context.
        /// </summary>
        public Integra DeviceContext
        {
            get => AppContext.Integra;
        }

        #region Methods

        private void AppContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(AppContext));
            NotifyPropertyChanged(nameof(DeviceContext));
        }

        #endregion

        #region Interfaces: INotifyPropertyChanged

        /// <summary>
        /// Event raised when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property that is changed.</param>
        /// <remarks><i>If no property name is specified, the actual name of the property in code is used.</i></remarks>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        
    }
}
