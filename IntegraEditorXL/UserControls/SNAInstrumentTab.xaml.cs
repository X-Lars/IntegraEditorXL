using IntegraEditorXL.UserControls.SNA;
using IntegraXL;
using IntegraXL.Core;
using IntegraXL.Models;
using IntegraXL.Models.Parameters;
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
        private IntegraSNAMapper _Parameters;
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
            _Parameters = (IntegraSNAMapper)_Context.Parameters;
            _Context.TypeChanged += ContextTypeChanged;
            SetComponent();

        }

        private void ContextTypeChanged(object sender, IntegraTypeChangedEventArgs e)
        {
            _Parameters = (IntegraSNAMapper)_Context.Parameters;
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
                case SNAPiano:          CreateComponent(typeof(SNAParametersPiano));          break;
                case SNAPianoMono:      CreateComponent(typeof(SNAParametersPianoMono));      break;
                case SNAKeys:           CreateComponent(typeof(SNAParametersKeys));           break;
                case SNABellMallet1:    CreateComponent(typeof(SNAParametersBellMallet1));    break;
                case SNABellMallet2:    CreateComponent(typeof(SNAParametersBellMallet1));    break;
                case SNABellMallet3:    CreateComponent(typeof(SNAParametersBellMallet2));    break;
                case SNAOrgan:          CreateComponent(typeof(SNAParametersOrgan));          break;
                case SNAHarmonica:      CreateComponent(typeof(SNAParametersHarmonica));      break;
                case SNAGuitar1:        CreateComponent(typeof(SNAParametersGuitar1));        break;
                case SNAGuitar2:        CreateComponent(typeof(SNAParametersGuitar1));        break;
                case SNAGuitar3:        CreateComponent(typeof(SNAParametersGuitar1));        break;
                case SNAGuitar4:        CreateComponent(typeof(SNAParametersGuitar2));        break;
                case SNAMandolin:       CreateComponent(typeof(SNAParametersMandolin));       break;
                case SNAUkelele:        CreateComponent(typeof(SNAParametersUkelele));        break;
                case SNAElectricGuitar: CreateComponent(typeof(SNAParametersElectricGuitar)); break;
                case SNABass1:          CreateComponent(typeof(SNAParametersBass));           break;
                case SNABass2:          CreateComponent(typeof(SNAParametersBass));           break;
                case SNABass3:          CreateComponent(typeof(SNAParametersBass));           break;
                case SNAHarp:           CreateComponent(typeof(SNAParametersHarp));           break;
                case SNASitar:          CreateComponent(typeof(SNAParametersSitar));          break;
                case SNAShamisen:       CreateComponent(typeof(SNAParametersShamisen));       break;
                case SNAKoto1:          CreateComponent(typeof(SNAParametersKoto1));          break;
                case SNAKoto2:          CreateComponent(typeof(SNAParametersKoto2));          break;
                case SNAKalimba:        CreateComponent(typeof(SNAParametersKalimba));        break;
                case SNAStrings1:       CreateComponent(typeof(SNAParametersStrings1));       break;
                case SNAStrings2:       CreateComponent(typeof(SNAParametersStrings1));       break;
                case SNAStrings3:       CreateComponent(typeof(SNAParametersStrings2));       break;
                case SNAChoir:          CreateComponent(typeof(SNAParametersStrings2));       break;
                case SNABrass1:         CreateComponent(typeof(SNAParametersBrass1));         break;
                case SNABrass2:         CreateComponent(typeof(SNAParametersBrass1));         break;
                case SNABrass3:         CreateComponent(typeof(SNAParametersBrass2));         break;
                case SNAWind1:          CreateComponent(typeof(SNAParametersWind1));          break;
                case SNAWind2:          CreateComponent(typeof(SNAParametersWind2));          break;
                case SNAWhistle:        CreateComponent(typeof(SNAParametersWind2));          break;
                case SNARecorder:       CreateComponent(typeof(SNAParametersWind2));          break;
                case SNAPanFlute:       CreateComponent(typeof(SNAParametersBrass1));         break;
                case SNASax:            CreateComponent(typeof(SNAParametersWind1));          break;
                case SNAPipes:          CreateComponent(typeof(SNAParametersPipes));          break;
                case SNATimpani:        CreateComponent(typeof(SNAParametersTimpani));        break;
                case SNASteelDrums:     CreateComponent(typeof(SNAParametersSteelDrums));     break;

                default: ParameterControl = null; break;
            }
        }

        private void CreateComponent(Type type)
        {
            Application.Current.Dispatcher.Invoke(() => this.ParameterControl = Activator.CreateInstance(type));
        }

    }
}
