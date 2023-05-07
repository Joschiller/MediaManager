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
        #region Events
        public event RoutedEventHandler Click;
        #endregion

        #region Properties
        public object Tooltip { get => btn.ToolTip; set => btn.ToolTip = value; }
        /// <summary>
        /// Size of the button.<br/>
        /// Default: <c>48</c>
        /// </summary>
        public double Size { get; set; } = 48;
        public ImageSource EnabledIconSource { get; set; }
        public ImageSource DisabledIconSource { get; set; } = null;
        /// <summary>
        /// This property should be used to correctly update the appearance of the image button if it gets enabled or disabled.<br/>
        /// Default: <c>true</c>
        /// </summary>
        public bool Enabled
        {
            get => (bool)GetValue(EnabledProperty);
            set => SetValue(EnabledProperty, value);
        }
        public static readonly DependencyProperty EnabledProperty = DependencyProperty.Register("Enabled", typeof(bool), typeof(ImageButton), new PropertyMetadata(true, OnEnabledPropertyChanged));
        public static void OnEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ImageButton)d).RefreshGUI((bool)e.NewValue);
        #endregion

        #region Setup
        public ImageButton()
        {
            InitializeComponent();
            DataContext = this;
            RefreshGUI(Enabled);
        }
        private void RefreshGUI(bool enabled)
        {
            btn.Visibility = enabled ? Visibility.Visible : Visibility.Collapsed;
            disabledIcon.Visibility = enabled ? Visibility.Collapsed : Visibility.Visible;
            if (!enabled && DisabledIconSource == null) DisabledIconSource = EnabledIconSource;
        }
        #endregion

        #region Handler
        private void btn_Click(object sender, RoutedEventArgs e) => Click?.Invoke(this, e);
        #endregion
    }
}
