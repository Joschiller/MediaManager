using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediaManager.GUI.Atoms
{
    /// <summary>
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : UserControl
    {
        public object Tooltip { get; set; }
        public int Size { get; set; }
        public ImageSource EnabledIconSource { get; set; }
        public ImageSource DisabledIconSource { get; set; } = null;
        public bool Enabled
        {
            get => IsEnabled;
            set
            {
                IsEnabled = value;
                btn.Visibility = IsEnabled ? Visibility.Visible : Visibility.Collapsed;
                disabledIcon.Visibility = IsEnabled ? Visibility.Collapsed : Visibility.Visible;
                if (!IsEnabled && DisabledIconSource == null) DisabledIconSource = EnabledIconSource;
            }
        }
        public event RoutedEventHandler Click;

        public ImageButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btn_Click(object sender, RoutedEventArgs e) => Click?.Invoke(sender, e);
    }
}
