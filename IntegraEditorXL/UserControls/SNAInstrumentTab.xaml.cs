using IntegraEditorXL.UserControls.SNA;
using IntegraXL;
using IntegraXL.Core;
using IntegraXL.Models;
using IntegraXL.Models.Providers;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace IntegraEditorXL.UserControls
{
    /// <summary>
    /// Interaction logic for SNAInstrumentTab.xaml
    /// </summary>
    public partial class SNAInstrumentTab : UserControl, INotifyPropertyChanged
    {
        private SuperNATURALAcousticToneCommon _Context;
        private IntegraSNAProvider _Parameters;
        private object _Component;

        public event PropertyChangedEventHandler PropertyChanged;

        public SNAInstrumentTab()
        {
            InitializeComponent();
            Loaded += SNAInstrumentTabLoaded;
        }

        private void SNAInstrumentTabLoaded(object sender, RoutedEventArgs e)
        {
            _Context = ((Integra)DataContext).TemporaryTone.SuperNATURALAcousticTone.Common;
            _Parameters = (IntegraSNAProvider)_Context.Parameters;
            _Context.ParametersChanged += ContextTypeChanged;
            SetComponent();

        }

        private void ContextTypeChanged(object sender, IntegraParametersChangedEventArgs e)
        {
            _Parameters = (IntegraSNAProvider)_Context.Parameters;
            SetComponent();
        }

        public object ParameterControl
        {
            get => _Component;

            set
            {
                _Component = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void SetComponent()
        {
            switch(_Parameters)
            {
                case SNAPiano:          CreateComponent(typeof(Piano));          break;
                case SNAPianoMono:      CreateComponent(typeof(PianoMono));      break;
                case SNAKeys:           CreateComponent(typeof(Keys));           break;
                case SNABellMallet1:    CreateComponent(typeof(BellMallet1));    break;
                case SNABellMallet2:    CreateComponent(typeof(BellMallet1));    break;
                case SNABellMallet3:    CreateComponent(typeof(BellMallet2));    break;
                case SNAOrgan:          CreateComponent(typeof(Organ));          break;
                case SNAHarmonica:      CreateComponent(typeof(Harmonica));      break;
                case SNAGuitar1:        CreateComponent(typeof(Guitar1));        break;
                case SNAGuitar2:        CreateComponent(typeof(Guitar1));        break;
                case SNAGuitar3:        CreateComponent(typeof(Guitar1));        break;
                case SNAGuitar4:        CreateComponent(typeof(Guitar2));        break;
                case SNAMandolin:       CreateComponent(typeof(Mandolin));       break;
                case SNAUkelele:        CreateComponent(typeof(Ukelele));        break;
                case SNAElectricGuitar: CreateComponent(typeof(ElectricGuitar)); break;
                case SNABass1:          CreateComponent(typeof(Bass));           break;
                case SNABass2:          CreateComponent(typeof(Bass));           break;
                case SNABass3:          CreateComponent(typeof(Bass));           break;
                case SNAHarp:           CreateComponent(typeof(Harp));           break;
                case SNASitar:          CreateComponent(typeof(Sitar));          break;
                case SNAShamisen:       CreateComponent(typeof(Shamisen));       break;
                case SNAKoto1:          CreateComponent(typeof(Koto1));          break;
                case SNAKoto2:          CreateComponent(typeof(Koto2));          break;
                case SNAKalimba:        CreateComponent(typeof(Kalimba));        break;
                case SNAStrings1:       CreateComponent(typeof(Strings1));       break;
                case SNAStrings2:       CreateComponent(typeof(Strings1));       break;
                case SNAStrings3:       CreateComponent(typeof(Strings2));       break;
                case SNAChoir:          CreateComponent(typeof(Strings2));       break;
                case SNABrass1:         CreateComponent(typeof(Brass1));         break;
                case SNABrass2:         CreateComponent(typeof(Brass1));         break;
                case SNABrass3:         CreateComponent(typeof(Brass2));         break;
                case SNAWind1:          CreateComponent(typeof(Wind1));          break;
                case SNAWind2:          CreateComponent(typeof(Wind2));          break;
                case SNAWhistle:        CreateComponent(typeof(Wind2));          break;
                case SNARecorder:       CreateComponent(typeof(Wind2));          break;
                case SNAPanFlute:       CreateComponent(typeof(Brass1));         break;
                case SNASax:            CreateComponent(typeof(Wind1));          break;
                case SNAPipes:          CreateComponent(typeof(Pipes));          break;
                case SNATimpani:        CreateComponent(typeof(Timpani));        break;
                case SNASteelDrums:     CreateComponent(typeof(SteelDrums));     break;

                default: ParameterControl = null; break;
            }
        }

        private void CreateComponent(Type type)
        {
            Application.Current.Dispatcher.Invoke(() => this.ParameterControl = Activator.CreateInstance(type));
        }

    }
}
