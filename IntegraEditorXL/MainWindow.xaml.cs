using ControlsXL;
using IntegraEditorXL.Common.Commands;
using IntegraEditorXL.UserControls;
using IntegraXL;
using IntegraXL.Core;
using IntegraXL.Interfaces;
using IntegraXL.Models;
using StylesXL;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
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

            StyleManager.Appearance = ControlAppearance.Default;
            StyleManager.Style = ControlStyle.Default;

            Loaded += MainWindowLoaded;
        }

        public ICommand ShowControlCommand
        {
            get => new UICommandParameterized<Type>((x) => ShowControl(x));
        }

        private void ShowControl(Type x)
        {
            Content.Content = (UIElement)Activator.CreateInstance(x);
        }

        public ICommand ShowToneBankCommand
        {
            get => new UICommandParameterized<IntegraToneBanks>((x) => ShowToneBank(x));
        }

        private void ShowToneBank(IntegraToneBanks x)
        {
            Content.Content = new ToneSelection(x);
        }

        private async void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            IMIDIOutputDevice midiOutputDevice = new MidiXLOutputDevice(1);
            IMIDIInputDevice  midiInputDevice  = new MidiXLInputDevice(0);
            
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            _Dialog = DialogManager.ProgressDialog("Connecting INTEGRA-7", "Please wait...", "", true);

            IntegraConnection connection = await IntegraConnectionManager.CreateConnection(midiOutputDevice, midiInputDevice);

            _Dialog.Close();

            if(connection != null)
            {
                _Dialog = DialogManager.ProgressDialog("Initializing INTEGRA-7", "Please wait...", "", true, false, ProgressIndicatorStyles.Circular);

                Integra = await Integra.CreateInstance(connection);
                //NotifyPropertyChanged(nameof(Integra));
                _Dialog.Close();

                TemporaryTone tone = await Integra.GetModel<TemporaryTone>(Parts.Part01);

                Debug.Print("COMPLETE MAIN WINDOW");
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
