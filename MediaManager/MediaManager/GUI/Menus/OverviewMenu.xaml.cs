using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using MediaManager.GUI.Dialogs;
using System;
using System.Windows;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for OverviewMenu.xaml
    /// </summary>
    public partial class OverviewMenu : Window, UpdatedLanguageUser
    {
        #region Setup
        public OverviewMenu()
        {
            Globals.Init.Initialize();
            InitializeComponent();
            RegisterAtLanguageProvider();
            if (!GlobalContext.Reader.AnyCatalogExists) OpenWindow(this, new CatalogMenu(), CloseIfNoCatalogExists);
            else
            {
                if (GlobalContext.Settings.BackupEnabled)
                {
                    var currentCatalog = CatalogContext.CurrentCatalogId;
                    if (currentCatalog.HasValue) RunBackgroundBackup(currentCatalog.Value);
                }
                Reload();
            }
        }
        public void RegisterAtLanguageProvider() => RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddMedium"] = getString("Menus.Overview.ToolTip.AddMedium");
            Resources["btnAddTag"] = getString("Menus.Overview.ToolTip.AddTag");
            Resources["btnTags"] = getString("Menus.Overview.ToolTip.Tags");
            Resources["btnCatalogs"] = getString("Menus.Overview.ToolTip.Catalogs");
            Resources["btnSettings"] = getString("Menus.Overview.ToolTip.Settings");
            Resources["btnAnalyze"] = getString("Menus.Overview.ToolTip.Analyze");
        }

        private void CloseIfNoCatalogExists()
        {
            if (!GlobalContext.Reader.AnyCatalogExists)
            {
                ShowDefaultDialog(getString("Menus.Overview.CloseHint"));
                Close();
            }
            else ShowAndReload();
        }
        private void ShowAndReload()
        {
            Show();
            Reload();
        }
        private void Reload()
        {
            catalogTitle.Text = GlobalContext.Reader.GetCatalog(CatalogContext.CurrentCatalogId.Value).Title;
            multiUseTabs.ReloadGUI();
            searchPanel.ReloadTags();
            searchPanel.ReloadResultList();
        }
        #endregion

        #region Handler
        #region Navbar
        private void NavigationBar_IconClicked(object sender, EventArgs e) => OpenHelpMenu(); // NOTE: may only be a temporary link
        private void NavigationBar_HelpClicked(object sender, EventArgs e) => OpenHelpMenu();
        private void btnAddMediumClick(object sender, RoutedEventArgs e) => AddMedium();
        private void btnAddTagClick(object sender, RoutedEventArgs e) => AddTag();
        private void btnTagsClick(object sender, RoutedEventArgs e) => OpenTagMenu();
        private void btnCatalogsClick(object sender, RoutedEventArgs e) => OpenCatalogMenu();
        private void btnSettingsClick(object sender, RoutedEventArgs e) => OpenSettingsMenu();
        private void btnAnalyzeClick(object sender, RoutedEventArgs e) => OpenAnalyzeMenu();
        #endregion
        private void searchPanel_MediumSelected(int mediumId, int? partId) => OpenMedium(mediumId, partId);
        private void searchPanel_PlaylistAdditionRequested(int id, Controls.Search.SearchResultMode mode)
        {
            var result = new PlaylistAdditionDialog(id, mode).ShowDialog();
            if (result.HasValue && result.Value) Reload();
        }
        private void PlaylistEditor_PartSelected(Part part) => OpenWindow(this, new EditMenu(part.MediumId, part.Id, false), ShowAndReload);
        #endregion

        #region Functions
        private void AddMedium() => OpenWindow(this, new EditMenu(null, null, true), ShowAndReload);
        private void AddTag()
        {
            var result = new EditTagDialog(null).ShowDialog();
            if (result.HasValue && result.Value) Reload();
        }
        private void OpenTagMenu() => OpenWindow(this, new TagMenu(), ShowAndReload);
        private void OpenCatalogMenu() => OpenWindow(this, new CatalogMenu(), CloseIfNoCatalogExists);
        private void OpenSettingsMenu() => OpenWindow(this, new SettingsMenu(), ShowAndReload);
        private void OpenAnalyzeMenu() => OpenWindow(this, new AnalyzeMenu(), ShowAndReload);
        private void OpenMedium(int mediumId, int? partId) => OpenWindow(this, new EditMenu(mediumId, partId, false), ShowAndReload);
        #endregion
    }
}
