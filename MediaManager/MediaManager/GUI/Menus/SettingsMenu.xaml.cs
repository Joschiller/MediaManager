using MediaManager.Globals.DefaultDialogs;
using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for SettingsMenu.xaml
    /// </summary>
    public partial class SettingsMenu : Window, UpdatedLanguageUser
    {
        #region Setup
        public SettingsMenu()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnSave"] = LanguageProvider.getString("Menus.Settings.ToolTip.Save");
            Resources["btnDiscard"] = LanguageProvider.getString("Menus.Settings.ToolTip.Discard");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnSaveClick(object sender, RoutedEventArgs e)
        {
            if (editor.ValidateData())
            {
                editor.SaveData();
                editor.LoadData(CURRENT_CATALOGUE.Id);
            }
            else
            {
                ShowDefaultDialog(LanguageProvider.getString("Menus.Settings.ValidationFailed"), SuccessMode.Error);
            }
        }
        private void btnDiscardClick(object sender, RoutedEventArgs e) => editor.LoadData(CURRENT_CATALOGUE.Id);
        #endregion
    }
}
