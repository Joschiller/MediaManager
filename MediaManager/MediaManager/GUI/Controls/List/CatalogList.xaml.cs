using MediaManager.Globals.LanguageProvider;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Controls.List
{
    /// <summary>
    /// Interaction logic for CatalogList.xaml
    /// </summary>
    public partial class CatalogList : UserControl, UpdatedLanguageUser
    {
        #region Events
        public delegate void CatalogSelectionHandler(Catalogue catalog);
        public event CatalogSelectionHandler SelectionChanged;
        public event CatalogSelectionHandler CatalogDoubleClick;
        #endregion

        #region Properties
        public Catalogue SelectedCatalog { get => list.SelectedItem == null ? null : (list.SelectedItem as CatalogListElement).Catalog; }
        #endregion

        #region Bindings
        public ObservableCollection<CatalogListElement> Catalogs { get; set; } = new ObservableCollection<CatalogListElement>();
        #endregion

        #region Setup
        public CatalogList()
        {
            InitializeComponent();
            DataContext = this;
            RegisterAtLanguageProvider();
            LoadCatalogs();
        }
        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            Resources["activeString"] = "(" + LanguageProvider.getString("Controls.CatalogList.ActiveString") + ")";
        }
        ~CatalogList() => LanguageProvider.Unregister(this);
        #endregion

        #region Getter/Setter
        public void LoadCatalogs()
        {
            Catalogs.Clear();
            var currentCatalog = CURRENT_CATALOGUE;
            Reader.Catalogs.ForEach(c => Catalogs.Add(new CatalogListElement
            {
                Catalog = c,
                IsActiveMarkVisible = c.Id == currentCatalog?.Id ? Visibility.Visible : Visibility.Collapsed
            }));
        }
        #endregion

        #region Handler
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e) => SelectionChanged?.Invoke(SelectedCatalog);
        private void list_MouseDoubleClick(object sender, MouseButtonEventArgs e) => CatalogDoubleClick?.Invoke(SelectedCatalog);
        #endregion
    }
}
