using IntegraXL.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace IntegraEditorXL.UserControls
{
    /// <summary>
    /// Interaction logic for ToneBank.xaml
    /// </summary>
    public partial class ToneSelection : UserControl, INotifyPropertyChanged
    {
        private IntegraToneBank _ToneBank;

        public ToneSelection(IntegraToneBank tonebank)
        {
            InitializeComponent();

            DataContext = this;
            ToneBank = tonebank;
        }

        public IntegraToneBank ToneBank
        {
            get => _ToneBank;
            set
            {
                if (_ToneBank != value)
                {
                    _ToneBank = value;
                    NotifyPropertyChanged();
                }
            }
        }

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
