﻿using MediaManager.Globals.LanguageProvider;
using MediaManager.Globals.SettingsEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Settings
{
    /// <summary>
    /// Interaction logic for LanguageSetting.xaml
    /// </summary>
    public partial class LanguageSetting : UserControl, LanguageUser, SettingsEditorItem
    {
        public List<string> LanguageList { get; set; } = LanguageProvider.LanguageList;

        #region Setup
        public LanguageSetting()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void LoadTexts(string language)
        {
            languageText.Text = LanguageProvider.getString(language ?? LanguageProvider.CurrentLanguage, "Controls.Settings.Language.Label") + ":";
        }
        public string GetControlName() => "Language";
        public string GetTabName() => LanguageProvider.getString("Menus.Settings.TabName");
        bool SettingsEditorItem.IsVisible() => true;
        #endregion
        #region Data
        private void selectedLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e) => LoadTexts(selectedLanguage.SelectedItem as string);
        public void LoadData(int? accountIdentifier)
        {
            selectedLanguage.SelectedItem = LanguageProvider.CurrentLanguage;
            LoadTexts(null);
        }
        public void SaveData() => LanguageProvider.CurrentLanguage = (selectedLanguage.SelectedItem as string) ?? LanguageProvider.CurrentLanguage;
        public bool ValidateData()
        {
            return RunValidation(new List<Func<string>>
            {
                () => selectedLanguage.SelectedItem == null ? LanguageProvider.getString("Controls.Settings.Language.Validation") : null
            });
        }
        #endregion
    }
}
