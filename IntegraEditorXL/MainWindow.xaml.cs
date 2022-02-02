using ControlsXL;
using IntegraEditorXL.Common.Commands;
using IntegraEditorXL.UserControls;
using IntegraXL;
using IntegraXL.Core;

using StylesXL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private Dialog _Dialog;
        private Integra _Integra;
        private IntegraConnection _Connection;

        public MainWindow()
        {
            SelectedConnection = IntegraConnectionManager.CreateConnection(16, new MidiXLOutputDevice(0), new MidiXLInputDevice(0));

            Integra = new Integra(SelectedConnection);
            DataContext = Integra;


            InitializeComponent();

            StyleManager.Appearance = ControlAppearance.Default;
            StyleManager.Style = ControlStyle.Default;

            Loaded += MainWindowLoaded;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            InitializeIntegra();
        }

        private async void InitializeIntegra()
        {

            if(await Integra.Connect())
            {
                await Integra.Initialize();
            }
            else
            {
                ShowControl(typeof(DeviceSelection));
            }
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
        
        public IEnumerable<MidiXLOutputDevice> MidiOutputDevices
        {
            get => MidiOutputDevice.GetDevices;
        }

        public IEnumerable<MidiXLInputDevice> MidiInputDevices
        {
            get => MidiInputDevice.GetDevices;
        }

        public MidiXLOutputDevice MidiOutputDevice
        {
            get => (MidiXLOutputDevice)SelectedConnection.MidiOutputDevice;
            set
            {
                if (SelectedConnection.MidiOutputDevice != value)
                {
                    SelectedConnection.SetMidiOutput(value);

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(SelectedConnection));
                }
            }
        }

        public MidiXLInputDevice MidiInputDevice
        {
            get => (MidiXLInputDevice)SelectedConnection.MidiInputDevice;
            set
            {
                if (SelectedConnection.MidiInputDevice != value)
                {
                    SelectedConnection.SetMidiInput(value);

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(SelectedConnection));
                }
            }
        }

        public IntegraConnection SelectedConnection
        {
            get => _Connection;
            set
            {
                _Connection = value;

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(MidiOutputDevice));
                NotifyPropertyChanged(nameof(MidiInputDevice));
            }
        }

        public Integra Integra
        {
            get => _Integra;
            set
            {
                _Integra = value;
                NotifyPropertyChanged();
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
