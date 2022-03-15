using IntegraXL;
using IntegraXL.Core;
using IntegraXL.Models;
using IntegraXL.Models.Parameters;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace IntegraEditorXL.Views
{
    /// <summary>
    /// Interaction logic for StudioSetCommonChorus.xaml
    /// </summary>
    public partial class StudioSetCommonChorusView : INotifyPropertyChanged
    {
        private StudioSetCommonChorus _Context;
        private IntegraMFXMapper _Parameters;
        private object _Component;

        public StudioSetCommonChorusView()
        {
            InitializeComponent();

            Loaded += StudioSetCommonChorusLoaded;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void StudioSetCommonChorusLoaded(object sender, RoutedEventArgs e)
        {
            _Context = ((Integra)DataContext).StudioSet.CommonChorus;
            _Context.TypeChanged += ContextTypeChanged;

            _Parameters = (IntegraMFXMapper)_Context.Parameters;
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

        private void ContextTypeChanged(object sender, IntegraTypeChangedEventArgs e)
        {
            _Parameters = (IntegraMFXMapper)_Context.Parameters;
            SetComponent();
        }

        private void SetComponent()
        {
            switch(_Parameters)
            {
                case CommonChorus:    CreateComponent(typeof(UserControls.Components.CommonChorus));    break;
                case CommonDelay:     CreateComponent(typeof(UserControls.Components.CommonDelay));     break;
                case CommonChorusGM2: CreateComponent(typeof(UserControls.Components.CommonChorusGM2)); break;
                case CommonChorusOff: CreateComponent(typeof(UserControls.Components.Empty));           break;

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
