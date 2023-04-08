using MediaManager.Globals.LanguageProvider;
using MediaManager.GUI.Atoms;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MediaManager.GUI.Components
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    [ContentProperty("Children")]
    public partial class NavigationBar : UserControl, UpdatedLanguageUser
    {
        public ObservableCollection<NavigationButtonGroup> Children { get; private set; } = new ObservableCollection<NavigationButtonGroup>();
        private NavigationBarMode _Mode = 0;
        /// <summary>
        /// Presentation mode of the <see cref="NavigationBar"/>.
        /// <br/>
        /// Concatenation of several modi can be done using a comma inside the WPF. e.g. <c>Mode="WithAppIcon,WithBackButton,WithHelpButton"</c>
        /// </summary>
        [TypeConverter(typeof(EnumConverter))]
        public NavigationBarMode Mode
        {
            get => _Mode;
            set
            {
                _Mode = value;
                var withIcon = ((int)value & (int)NavigationBarMode.WithAppIcon) != 0;
                var withBack = ((int)value & (int)NavigationBarMode.WithBackButton) != 0;
                var withHelp = ((int)value & (int)NavigationBarMode.WithHelpButton) != 0;
                icon1.Visibility = withIcon && !withBack ? Visibility.Visible : Visibility.Collapsed;
                icon2.Visibility = withIcon && withBack ? Visibility.Visible : Visibility.Collapsed;
                back.Visibility = withBack ? Visibility.Visible : Visibility.Collapsed;
                help.Visibility = withHelp ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public event EventHandler IconClicked;
        public event EventHandler BackClicked;
        public event EventHandler HelpClicked;

        public NavigationBar()
        {
            InitializeComponent();
            DataContext = this;

            Children.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Children_CollectionChanged);
            RegisterAtLanguageProvider();
        }

        void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (NavigationButtonGroup elem in e.NewItems) container.Children.Add(elem);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (NavigationButtonGroup elem in e.OldItems) container.Children.Remove(elem);
                    break;
                default:
                    break;
            }
        }

        private void icon_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => IconClicked?.Invoke(sender, e);
        private void back_Click(object sender, RoutedEventArgs e) => BackClicked?.Invoke(sender, e);
        private void help_Click(object sender, RoutedEventArgs e) => HelpClicked?.Invoke(sender, e);

        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            back.Tooltip = LanguageProvider.getString("Common.Tooltip.Back");
            help.Tooltip = LanguageProvider.getString("Common.Tooltip.Help");
        }
        ~NavigationBar() => LanguageProvider.Unregister(this);
    }

    public enum NavigationBarMode {
        WithAppIcon = 1,
        WithBackButton = 2,
        WithHelpButton = 4,
    };
}
