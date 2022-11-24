using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediaManager.GUI.Atoms
{
    /// <summary>
    /// Interaction logic for NavigationButton.xaml
    /// </summary>
    public partial class NavigationButton : UserControl
    {
        public object Tooltip { get; set; }
        public ImageSource IconSource { get; set; }
        public event RoutedEventHandler Click;

        public NavigationButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Click?.Invoke(sender, e);
    }
}
