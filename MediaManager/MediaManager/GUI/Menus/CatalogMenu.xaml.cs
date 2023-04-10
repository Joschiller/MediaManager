using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for CatalogMenu.xaml
    /// </summary>
    public partial class CatalogMenu : Window, UpdatedLanguageUser
    {
        #region Setup
        public CatalogMenu()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddCatalog"] = LanguageProvider.getString("Menus.Catalog.ToolTip.AddCatalog");
            Resources["btnEditCatalog"] = LanguageProvider.getString("Menus.Catalog.ToolTip.EditCatalog");
            Resources["btnExportCatalog"] = LanguageProvider.getString("Menus.Catalog.ToolTip.ExportCatalog");
            Resources["btnDeleteCatalog"] = LanguageProvider.getString("Menus.Catalog.ToolTip.DeleteCatalog");
            Resources["btnSettings"] = LanguageProvider.getString("Menus.Catalog.ToolTip.Settings");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnAddTagClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            //catalogList.LoadCatalogs();
        }
        private void btnExportCatalogClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnEditTagClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            //catalogList.LoadCatalogs();
        }
        private void btnDeleteTagClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            //catalogList.LoadCatalogs();
        }
        private void btnSettingsClick(object sender, RoutedEventArgs e) => OpenWindow(this, new SettingsMenu());
        #endregion

        private void catalogList_SelectionChanged(Catalogue catalog) => Resources["catalogDependentVisibility"] = catalog == null ? Visibility.Collapsed : Visibility.Visible;
        private void catalogList_CurrentCatalogChanged(Catalogue catalog)
        {
            throw new NotImplementedException();
        }
    }
}
