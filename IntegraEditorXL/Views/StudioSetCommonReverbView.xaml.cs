using IntegraXL;
using IntegraXL.Core;
using IntegraXL.Models;
using IntegraXL.Models.Providers;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace IntegraEditorXL.Views
{
    /// <summary>
    /// Interaction logic for StudioSetCommonReverb.xaml
    /// </summary>
    public partial class StudioSetCommonReverbView : UserControl, INotifyPropertyChanged
    {
        private StudioSetCommonReverb _Context;
        private IntegraMFXProvider _Parameters;
        private object _Component;

        public StudioSetCommonReverbView()
        {
            InitializeComponent();

            Loaded += StudioSetCommonReverbViewLoaded;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void StudioSetCommonReverbViewLoaded(object sender, RoutedEventArgs e)
        {
            _Context = ((Integra)DataContext).StudioSet.CommonReverb;
            _Context.ParametersChanged += ParametersChanged;

            _Parameters = (IntegraMFXProvider)_Context.Parameters;
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

        private void ParametersChanged(object sender, IntegraParametersChangedEventArgs e)
        {
            _Parameters = (IntegraMFXProvider)_Context.Parameters;
            SetComponent();
        }

        private void SetComponent()
        {
            switch (_Parameters)
            {
                case CommonReverb:    CreateComponent(typeof(UserControls.Components.CommonReverb));    break;
                case CommonReverbGM2: CreateComponent(typeof(UserControls.Components.CommonReverbGM2)); break;
                case CommonReverbOff: CreateComponent(typeof(UserControls.Components.Empty));           break;

                default: ParameterControl = null; break;
            }
        }

        private void CreateComponent(Type type)
        {
            Application.Current.Dispatcher.Invoke(() => this.ParameterControl = Activator.CreateInstance(type));
        }

        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
