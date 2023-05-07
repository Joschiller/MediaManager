using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MediaManager.GUI.Controls.MultiUseTabs
{
    /// <summary>
    /// Interaction logic for MultiUseTabs.xaml
    /// </summary>
    [ContentProperty("Children")]
    public partial class MultiUseTabs : UserControl
    {
        #region Properties
        public ObservableCollection<MultiUseTabsControl> Children { get; private set; } = new ObservableCollection<MultiUseTabsControl>();
        #endregion

        #region Bindings
        public ObservableCollection<TabItem> Tabs { get; private set; } = new ObservableCollection<TabItem>();
        #endregion

        #region Setup
        public MultiUseTabs()
        {
            InitializeComponent();
            DataContext = this;
            Children.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Children_CollectionChanged);
        }
        public void ReloadGUI()
        {
            foreach (var t in Tabs) t.Content = null;
            Tabs.Clear();
            single.Children.Clear();

            if (Children.Where(t => t.GetIsVisible()).Count() > 1)
            {
                tabs.Visibility = Visibility.Visible;
                single.Visibility = Visibility.Collapsed;

                foreach (MultiUseTabsControl elem in Children)
                {
                    if (!elem.GetIsVisible()) continue;
                    var header = new Image();
                    header.Source = elem.GetHeader();
                    header.Height = 48;
                    header.Width = 48;
                    Tabs.Add(new TabItem
                    {
                        Header = header,
                        Content = elem
                    });
                    elem.ReloadGUI();
                }

                if (tabs.SelectedIndex < 0) tabs.SelectedIndex = 0;
            }
            else
            {
                tabs.Visibility = Visibility.Collapsed;
                single.Visibility = Visibility.Visible;

                var child = Children.FirstOrDefault(t => t.GetIsVisible());
                if (child != null) single.Children.Add((UIElement)child);
            }
        }
        #endregion

        #region Handler
        void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => ReloadGUI();
        #endregion
    }
}
