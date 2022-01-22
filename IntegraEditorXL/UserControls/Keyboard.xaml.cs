using IntegraControlsXL;
using IntegraXL;
using IntegraXL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Keyboard.xaml
    /// </summary>
    public partial class Keyboard : UserControl
    {
        public Keyboard()
        {
            InitializeComponent();

            DataContext = ((MainWindow)Application.Current.MainWindow).Integra.StudioSet[((MainWindow)Application.Current.MainWindow).Integra.SelectedPart];
        }

        private void KeyboardNoteOn(object sender, RoutedEventArgs e)
        {
            StudioSetPart part = (StudioSetPart)DataContext;

            KeyboardControlEventArgs args = (KeyboardControlEventArgs)e;

            //((MainWindow)Application.Current.MainWindow).Integra.TransmitNoteOn((int)part.Part, args.NoteNumber, args.Velocity);
            //MainContext.SelectedMidiOutputDevice.SendNoteOn((int)MainContext.SelectedPart, args.NoteNumber, args.Velocity);
        }

        private void KeyboardNoteOff(object sender, RoutedEventArgs e)
        {
            StudioSetPart part = (StudioSetPart)DataContext;
            KeyboardControlEventArgs args = (KeyboardControlEventArgs)e;

            //((MainWindow)Application.Current.MainWindow).Integra.TransmitNoteOn((int)part.Part, args.NoteNumber, 0);
            //MainContext.SelectedMidiOutputDevice.SendNoteOff((int)MainContext.SelectedPart, args.NoteNumber, 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MainContext.SelectedMidiOutputDevice.SendControlChange((int)MainContext.SelectedPart, 123, 0);
        }
    }
}
