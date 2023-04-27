using MediaManager.Globals.LanguageProvider;
using MediaManager.Globals.SettingsEditor;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Settings
{
    /// <summary>
    /// Interaction logic for MultiUseTabsSetting.xaml
    /// </summary>
    public partial class MultiUseTabsSetting : UserControl, LanguageUser, SettingsEditorItem
    {
        #region Setup
        public MultiUseTabsSetting()
        {
            InitializeComponent();
        }
        public void LoadTexts(string language)
        {
            playlistLabel.Text = LanguageProvider.getString("Controls.Settings.MultiUseTabs.PlaylistLabel") + ":";
            titleOfTheDayLabel.Text = LanguageProvider.getString("Controls.Settings.MultiUseTabs.TitleOfTheDayLabel") + ":";
            statisticsLabel.Text = LanguageProvider.getString("Controls.Settings.MultiUseTabs.StatisticsLabel") + ":";
        }
        public string GetControlName() => "MultiUseTabs";
        public string GetTabName() => LanguageProvider.getString("Menus.Settings.TabName");
        bool SettingsEditorItem.IsVisible() => true;
        #endregion
        #region Data
        public void LoadData(int? accountIdentifier)
        {
            playlistVisible.IsChecked = Reader.Settings.PlaylistEditorVisible;
            titleOfTheDayVisible.IsChecked = Reader.Settings.TitleOfTheDayVisible;
            statisticsVisible.IsChecked = Reader.Settings.StatisticsOverviewVisible;
            LoadTexts(null);
        }
        public void SaveData()
        {
            Writer.Settings.PlaylistEditorVisible = playlistVisible.IsChecked.HasValue && playlistVisible.IsChecked.Value;
            Writer.Settings.TitleOfTheDayVisible = titleOfTheDayVisible.IsChecked.HasValue && titleOfTheDayVisible.IsChecked.Value;
            Writer.Settings.StatisticsOverviewVisible = statisticsVisible.IsChecked.HasValue && statisticsVisible.IsChecked.Value;
        }
        public bool ValidateData() => true;
        #endregion
    }
}
