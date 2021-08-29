using IntegraXL.Collections;
using IntegraXL.Core;
using IntegraXL.Models;
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
    /// <summary>
    /// Interaction logic for ToneBank.xaml
    /// </summary>
    public partial class ToneSelection : UserControl, INotifyPropertyChanged
    {
        private IntegraToneBanks _SelectedToneBank = IntegraToneBanks.SNAPresetTone;

        private IntegraToneCollection _ToneBank;

        public ToneSelection(Type toneBank)
        {
            InitializeComponent();
            DataContext = this;
            InitializeToneBank(toneBank);
        }

        private async void InitializeToneBank(Type type)
        {
            ToneBank = await ((MainWindow)Application.Current.MainWindow).Integra.ToneBank(type);
            //NotifyPropertyChanged(nameof(ToneBank));
        }
        
        public IntegraToneCollection ToneBank
        {
            get => _ToneBank;
            set
            {
                if(_ToneBank != value)
                {
                    _ToneBank = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IntegraToneBanks SelectedToneBank
        {
            get => _SelectedToneBank;
            set
            {
                if (_SelectedToneBank != value)
                {
                    _SelectedToneBank = value;
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
