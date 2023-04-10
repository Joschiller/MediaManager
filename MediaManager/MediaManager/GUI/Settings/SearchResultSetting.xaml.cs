using MediaManager.Globals.LanguageProvider;
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
            searchResultLabel.Text = LanguageProvider.getString("Controls.Settings.SearchResult.Label") + ":";
        }
        public string GetControlName() => "SearchResult";
        public string GetTabName() => LanguageProvider.getString("Menus.Settings.TabName");
        bool SettingsEditorItem.IsVisible() => true;
        #endregion
        #region Data
        public void LoadData(int? accountIdentifier)
        {
            searchResultValue.SetValue((uint)Reader.Settings.ResultListLength);
            LoadTexts(null);
        }
        public void SaveData() => Writer.Settings.ResultListLength = (int)searchResultValue.Value;
        public bool ValidateData()
        {
            // validation is already ensured by to the input mechanism itself
            return RunValidation(new List<System.Func<string>>
            {
                () => searchResultValue.Value <= 0 && searchResultValue.Value > 50 ? LanguageProvider.getString("Controls.Settings.SearchResult.Validation") : null, // value is valid
            });
        }
        #endregion
    }
}
