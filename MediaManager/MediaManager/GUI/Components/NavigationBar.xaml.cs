using MediaManager.Globals.LanguageProvider;
using MediaManager.GUI.Atoms;
using System;
using System.Collections.ObjectModel;
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
        public bool WithBackButton
        {
            get => back.Visibility == Visibility.Visible;
            set
            {
                back.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                icon1.Visibility = !value && WithAppIcon ? Visibility.Visible : Visibility.Collapsed;
                icon2.Visibility = value && WithAppIcon ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public bool WithAppIcon
        {
            get => icon1.Visibility == Visibility.Visible || icon1.Visibility == Visibility.Visible;
            set
            {
                icon1.Visibility = !WithBackButton && value ? Visibility.Visible : Visibility.Collapsed;
                icon2.Visibility = WithBackButton && value ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public bool WithHelpButton
        {
            get => help.Visibility == Visibility.Visible;
            set => help.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
        public event EventHandler IconClicked;
        public event EventHandler BackClicked;
        public event EventHandler HelpClicked;

        public NavigationBar()
        {
            InitializeComponent();
            DataContext = this;

            Children.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Children_CollectionChanged);
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
    }
}
