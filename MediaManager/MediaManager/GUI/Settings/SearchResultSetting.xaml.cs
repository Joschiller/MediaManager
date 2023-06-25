using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using MediaManager.Globals.SettingsEditor;
using System.Collections.Generic;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Settings
{
    /// <summary>
    /// Interaction logic for SearchResultSetting.xaml
    /// </summary>
    public partial class SearchResultSetting : UserControl, LanguageUser, SettingsEditorItem
    {
        #region Setup
        public SearchResultSetting()
        {
            InitializeComponent();
            searchResultValue.SetMin(1);
            searchResultValue.SetMax(50);
            searchResultValue.MaxLength = 512;
        }
        public void LoadTexts(string language)
        {
            searchResultLabel.Text = getString("Controls.Settings.SearchResult.Label") + ":";
        }
        public string GetControlName() => "SearchResult";
        public string GetTabName() => getString("Menus.Settings.TabName");
        bool SettingsEditorItem.IsVisible() => true;
        #endregion
        #region Data
        public void LoadData(int? accountIdentifier)
        {
            searchResultValue.SetValue((uint)GlobalContext.Settings.ResultListLength);
            LoadTexts(null);
        }
        public void SaveData() => GlobalContext.Settings.ResultListLength = (int)searchResultValue.Value;
        // validation is already ensured by to the input mechanism itself
        public bool ValidateData() => RunValidation(new List<System.Func<string>>
            {
                () => searchResultValue.Value <= 0 && searchResultValue.Value > 50 ? getString("Controls.Settings.SearchResult.Validation") : null, // value is valid
            });
        #endregion
    }
}
