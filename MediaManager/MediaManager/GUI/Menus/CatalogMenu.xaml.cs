using MediaManager.Globals.LanguageProvider;
using MediaManager.GUI.Dialogs;
using System;
using System.Windows;
using static MediaManager.Globals.DataConnector;
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
            Resources["btnImportCatalog"] = LanguageProvider.getString("Menus.Catalog.ToolTip.ImportCatalog");
            Resources["btnEditCatalog"] = LanguageProvider.getString("Menus.Catalog.ToolTip.EditCatalog");
            Resources["btnExportCatalog"] = LanguageProvider.getString("Menus.Catalog.ToolTip.ExportCatalog");
            Resources["btnDeleteCatalog"] = LanguageProvider.getString("Menus.Catalog.ToolTip.DeleteCatalog");
            Resources["btnSettings"] = LanguageProvider.getString("Menus.Catalog.ToolTip.Settings");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e) => OpenHelpMenu();
        private void btnAddCatalogClick(object sender, RoutedEventArgs e)
        {
            var result = new EditCatalogDialog(null).ShowDialog();
            if (result.HasValue && result.Value) catalogList.LoadCatalogs();
        }
        private void btnImportCatalogClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnExportCatalogClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnEditCatalogClick(object sender, RoutedEventArgs e)
        {
            var result = new EditCatalogDialog(catalogList.SelectedCatalog.Id).ShowDialog();
            if (result.HasValue && result.Value) catalogList.LoadCatalogs();
        }
        private void btnDeleteCatalogClick(object sender, RoutedEventArgs e)
        {
            if (catalogList.SelectedCatalog == null) return;

            // deletion confirmation must always be shown
            var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString("Menus.Catalog.CatalogDeletion"));
            if (confirmation.HasValue && confirmation.Value)
            {
                Writer.DeleteCatalog(catalogList.SelectedCatalog.Id);
                if (CURRENT_CATALOGUE == null && Reader.AnyCatalogExists()) CURRENT_CATALOGUE = Reader.Catalogs[0];
                catalogList.LoadCatalogs();
            }
        }
        private void btnSettingsClick(object sender, RoutedEventArgs e) => OpenWindow(this, new SettingsMenu());
        #endregion

        private void catalogList_SelectionChanged(Catalogue catalog) => Resources["catalogDependentVisibility"] = catalog == null ? Visibility.Collapsed : Visibility.Visible;
        private void catalogList_CatalogDoubleClick(Catalogue catalog)
        {
            CURRENT_CATALOGUE = catalog;
            catalogList.LoadCatalogs();
        }
    }
}
