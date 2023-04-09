using MediaManager.Globals;
using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for OverviewMenu.xaml
    /// </summary>
    public partial class OverviewMenu : Window, UpdatedLanguageUser
    {
        public OverviewMenu()
        {
            InitializeComponent();
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

        private void btnAddMediumClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
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
        private void NavigationBar_IconClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchPanel_MediumSelected(int mediumId, int? partId)
        {
            throw new NotImplementedException();
        }
    }
}
