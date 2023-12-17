using DefaultDialogs;
using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using System;
using System.Windows;
using System.Windows.Input;
using static MediaManager.Globals.KeyboardShortcutHelper;
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

        #region Handler
        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) => runKeyboardShortcut(e, new System.Collections.Generic.Dictionary<(ModifierKeys Modifiers, Key Key), Action>
        {
            [(ModifierKeys.None, Key.F1)] = OpenHelpMenu,
            [(ModifierKeys.None, Key.Escape)] = Close,
            [(ModifierKeys.Control, Key.R)] = Discard,
            [(ModifierKeys.Control, Key.S)] = Save,
        });
        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e) => OpenHelpMenu();
        private void btnDiscardClick(object sender, RoutedEventArgs e) => Discard();
        private void btnSaveClick(object sender, RoutedEventArgs e) => Save();
        #endregion
        #endregion

        #region Functions
        private void Discard() => editor.LoadData(null);
        private void Save()
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
