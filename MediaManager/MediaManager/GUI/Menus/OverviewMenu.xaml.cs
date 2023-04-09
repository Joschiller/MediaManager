using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
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
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddMedium"] = LanguageProvider.getString("Menus.Overview.Tooltip.AddMedium");
            Resources["btnAddTag"] = LanguageProvider.getString("Menus.Overview.Tooltip.AddTag");
            Resources["btnTags"] = LanguageProvider.getString("Menus.Overview.Tooltip.Tags");
            Resources["btnCatalogs"] = LanguageProvider.getString("Menus.Overview.Tooltip.Catalogs");
            Resources["btnSettings"] = LanguageProvider.getString("Menus.Overview.Tooltip.Settings");
            Resources["btnAnalyze"] = LanguageProvider.getString("Menus.Overview.Tooltip.Analyze");
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
        private void btnAddMediumClick(object sender, RoutedEventArgs e) => OpenWindow(this, new EditMenu(null, null));
        private void btnAddTagClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnTagsClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnCatalogsClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnSettingsClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnAnalyzeClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void SearchPanel_MediumSelected(int mediumId, int? partId) => OpenWindow(this, new EditMenu(mediumId, partId));
    }
}
