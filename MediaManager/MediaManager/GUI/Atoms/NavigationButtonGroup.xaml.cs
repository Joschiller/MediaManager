using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Markup;
using WPFComponents;

namespace MediaManager.GUI.Atoms
{
    /// <summary>
    /// Interaction logic for NavigationButtonGroup.xaml
    /// </summary>
    [ContentProperty("Children")]
    public partial class NavigationButtonGroup : UserControl
    {
        #region Properties
        /// <summary>
        /// Orientation in which the image buttons are stacked.<br/>
        /// Default: <see cref="Orientation.Horizontal"/>
        /// </summary>
        public Orientation Orientation { get; set; } = Orientation.Horizontal;
        public ObservableCollection<ImageButton> Children { get; private set; } = new ObservableCollection<ImageButton>();
        #endregion

        #region Setup
        public NavigationButtonGroup()
        {
            InitializeComponent();
            DataContext = this;
            Children.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Children_CollectionChanged);
        }
        #endregion

        #region Handler
        void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (ImageButton elem in e.NewItems) container.Children.Add(elem);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (ImageButton elem in e.OldItems) container.Children.Remove(elem);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
