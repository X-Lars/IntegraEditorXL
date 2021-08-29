using ControlsXL;
using IntegraEditorXL.UserControls;
using IntegraXL;
using IntegraXL.Common;
using IntegraXL.Core;
using IntegraXL.Interfaces;
using StylesXL;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IntegraEditorXL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Integra _Integra;
        private Dialog _Dialog;
       
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            StyleManager.Appearance = ControlAppearance.Flat;
            StyleManager.Style = ControlStyle.Default;

            Loaded += MainWindowLoaded;
        }

        public ICommand ShowControlCommand
        {
            get => new UICommandParameterized<Type>((x) => ShowControl(x));
        }

        private void ShowControl(Type x)
        {
            ActiveContent.Children.Clear();
            ActiveContent.Children.Add((UIElement)Activator.CreateInstance(x));
        }

        public ICommand ShowToneBankCommand
        {
            get => new UICommandParameterized<Type>((x) => ShowToneBank(x));
        }

        private void ShowToneBank(Type x)
        {
            ActiveContent.Children.Clear();
            ActiveContent.Children.Add(new ToneSelection(x));
        }

        private async void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            IIntegraOutputDevice midiOutputDevice = new MidiXLOutputDevice(1);
            IIntegraInputDevice  midiInputDevice  = new MidiXLInputDevice(0);
            
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            _Dialog = DialogManager.ProgressDialog("Connecting INTEGRA-7", "Please wait...", "", true);

            IntegraConnection connection = await DeviceConnectionManager.Connect(midiOutputDevice, midiInputDevice);

            _Dialog.Close();

            if(connection != null)
            {
                _Dialog = DialogManager.ProgressDialog("Initializing INTEGRA-7", "Please wait...", "", true, false, ProgressIndicatorStyles.Circular);

                Integra = await Integra.CreateInstance(connection);
                //NotifyPropertyChanged(nameof(Integra));
                _Dialog.Close();
            }
        }

        public Integra Integra 
        {
            get { return _Integra; }
            private set
            {
                if(_Integra != value)
                {
                    _Integra = value;
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
