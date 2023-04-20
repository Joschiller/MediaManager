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
        public object Tooltip { get; set; } = null;
        public int Size { get; set; } = 48;
        public ImageSource EnabledIconSource { get; set; }
        public ImageSource DisabledIconSource { get; set; } = null;
        public bool Enabled
        {
            get => (bool)GetValue(EnabledProperty);
            set => SetValue(EnabledProperty, value);
        }
        public static readonly DependencyProperty EnabledProperty = DependencyProperty.Register("Enabled", typeof(bool), typeof(ImageButton), new PropertyMetadata(true, OnEnabledPropertyChanged));
        public static void OnEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ImageButton)d).RefreshGUI((bool)e.NewValue);
        public event RoutedEventHandler Click;

        private void RefreshGUI(bool enabled)
        {
            btn.Visibility = enabled ? Visibility.Visible : Visibility.Collapsed;
            disabledIcon.Visibility = enabled ? Visibility.Collapsed : Visibility.Visible;
            if (!enabled && DisabledIconSource == null) DisabledIconSource = EnabledIconSource;
        }

        public ImageButton()
        {
            InitializeComponent();
            DataContext = this;
            RefreshGUI(Enabled);
        }

        private void btn_Click(object sender, RoutedEventArgs e) => Click?.Invoke(this, e);
    }
}
