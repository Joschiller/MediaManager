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
        public delegate void CatalogSelectionHandler(Catalogue catalog);
        public event CatalogSelectionHandler SelectionChanged;
        public event CatalogSelectionHandler CatalogDoubleClick;

        public ObservableCollection<CatalogListElement> Catalogs { get; set; } = new ObservableCollection<CatalogListElement>();
        public Catalogue SelectedCatalog { get => list.SelectedItem == null ? null : (list.SelectedItem as CatalogListElement).Catalog; }

        public CatalogList()
        {
            InitializeComponent();
            DataContext = this;
            RegisterAtLanguageProvider();
            LoadCatalogs();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["activeString"] = "(" + LanguageProvider.getString("Controls.CatalogList.ActiveString") + ")";
        }

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

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e) => SelectionChanged?.Invoke(SelectedCatalog);
        private void Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: double click is not working properly
            if (e.ClickCount == 2) CatalogDoubleClick?.Invoke(SelectedCatalog);
        }
    }
}
