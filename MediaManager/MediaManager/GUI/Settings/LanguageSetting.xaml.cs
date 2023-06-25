using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using SettingsEditor;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Settings
{
    /// <summary>
    /// Interaction logic for LanguageSetting.xaml
    /// </summary>
    public partial class LanguageSetting : UserControl, LanguageUser, SettingsEditorItem
    {
        #region Bindings
        public List<string> LanguageList { get; set; } = LanguageProvider.LanguageProvider.LanguageList;
        #endregion

        #region Setup
        public LanguageSetting()
        {
            InitializeComponent();
            DataContext = this;
        }
        public void LoadTexts(string language)
        {
            languageText.Text = getString(language ?? CurrentLanguage, "Controls.Settings.Language.Label") + ":";
        }
        public string GetControlName() => "Language";
        public string GetTabName() => getString("Menus.Settings.TabName");
        bool SettingsEditorItem.IsVisible() => true;
        #endregion
        #region Data
        private void selectedLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e) => LoadTexts(selectedLanguage.SelectedItem as string);
        public void LoadData(int? accountIdentifier)
        {
            selectedLanguage.SelectedItem = CurrentLanguage;
            LoadTexts(null);
        }
        public void SaveData() => CurrentLanguage = (selectedLanguage.SelectedItem as string) ?? CurrentLanguage;
        public bool ValidateData() => RunValidation(new List<Func<string>>
            {
                () => selectedLanguage.SelectedItem == null ? getString("Controls.Settings.Language.Validation") : null
            });
        #endregion
    }
}
