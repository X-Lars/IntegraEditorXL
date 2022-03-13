using IntegraEditorXL.UserControls.Components;
using IntegraXL;
using IntegraXL.Core;
using IntegraXL.Models;
using IntegraXL.Models.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
                if(_Component != value)
                {
                    _Component = value;
                    NotifyPropertyChanged();
                }
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
                case SNAPiano: CreateComponent(typeof(SNAParametersPiano)); break;
                case SNAPianoMono: CreateComponent(typeof(SNAParametersPianoMono)); break;
                case SNAKeys:  CreateComponent(typeof(SNAParametersKeys));  break;
                //case SNAStringsStaccatoPizzicatoTremolo: CreateComponent(typeof(SNAParametersStringsStaccatoPizzicatoTremolo)); break;
                default: ParameterControl = null; break;
            }
        }

        private void CreateComponent(Type type)
        {
            Application.Current.Dispatcher.Invoke(() => this.ParameterControl = Activator.CreateInstance(type));
        }

    }
}
