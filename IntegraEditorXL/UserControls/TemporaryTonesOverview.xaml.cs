using System.Diagnostics;
using System.Windows.Controls;

namespace IntegraEditorXL.UserControls
{
    /// <summary>
    /// Interaction logic for SelectedTones.xaml
    /// </summary>
    public partial class TemporaryTonesOverview : UserControl
    {
        public TemporaryTonesOverview()
        {
            InitializeComponent();

            Loaded += TemporaryTonesOverview_Loaded;
        }

        private void TemporaryTonesOverview_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Debug.Print(DataContext.ToString());
        }
    }
}
