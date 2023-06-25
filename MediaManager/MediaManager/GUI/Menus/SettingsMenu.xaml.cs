using MediaManager.Globals.DefaultDialogs;
using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using System;
using System.Windows;
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
            editor.LoadData(null);
        }
        public void RegisterAtLanguageProvider() => RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnDiscard"] = getString("Menus.Settings.ToolTip.Discard");
            Resources["btnSave"] = getString("Menus.Settings.ToolTip.Save");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e) => OpenHelpMenu();
        private void btnDiscardClick(object sender, RoutedEventArgs e) => editor.LoadData(null);
        private void btnSaveClick(object sender, RoutedEventArgs e)
        {
            if (editor.ValidateData())
            {
                editor.SaveData();
                ShowDefaultDialog(getString("Menus.Settings.Saved"), SuccessMode.Success);

                editor.InstantiateAllElements(); // reload all settings in case the language changed
                editor.LoadData(null); // needed so that the tabs are reloaded correctly
            }
            else
            {
                ShowDefaultDialog(getString("Menus.Settings.ValidationFailed"), SuccessMode.Error);
            }
        }
        #endregion
    }
}
