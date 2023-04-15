using MediaManager.Globals.LanguageProvider;
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
            if (!Reader.AnyCatalogExists()) OpenWindow(this, new CatalogMenu(), CloseIfNoCatalogExists);
            else Reload();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddMedium"] = LanguageProvider.getString("Menus.Overview.ToolTip.AddMedium");
            Resources["btnAddTag"] = LanguageProvider.getString("Menus.Overview.ToolTip.AddTag");
            Resources["btnTags"] = LanguageProvider.getString("Menus.Overview.ToolTip.Tags");
            Resources["btnCatalogs"] = LanguageProvider.getString("Menus.Overview.ToolTip.Catalogs");
            Resources["btnSettings"] = LanguageProvider.getString("Menus.Overview.ToolTip.Settings");
            Resources["btnAnalyze"] = LanguageProvider.getString("Menus.Overview.ToolTip.Analyze");
        }
        #endregion

        #region Navbar
        private void NavigationBar_IconClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnAddMediumClick(object sender, RoutedEventArgs e) => OpenWindow(this, new EditMenu(null, null), ShowAndReload);
        private void btnAddTagClick(object sender, RoutedEventArgs e) => new EditTagDialog(null).ShowDialog();
        private void btnTagsClick(object sender, RoutedEventArgs e) => OpenWindow(this, new TagMenu(), ShowAndReload);
        private void btnCatalogsClick(object sender, RoutedEventArgs e) => OpenWindow(this, new CatalogMenu(), CloseIfNoCatalogExists);
        private void btnSettingsClick(object sender, RoutedEventArgs e) => OpenWindow(this, new SettingsMenu(), ShowAndReload);
        private void btnAnalyzeClick(object sender, RoutedEventArgs e) => OpenWindow(this, new AnalyzeMenu(), ShowAndReload);
        #endregion

        private void CloseIfNoCatalogExists()
        {
            if (!Reader.AnyCatalogExists())
            {
                ShowDefaultDialog(LanguageProvider.getString("Menus.Overview.CloseHint"));
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
            catalogTitle.Text = CURRENT_CATALOGUE.Title;
            multiUseTabs.ReloadGUI();
            // TODO: reload search Panel, without clearing search or changing page
        }

        private void SearchPanel_MediumSelected(int mediumId, int? partId) => OpenWindow(this, new EditMenu(mediumId, partId));
    }
}
