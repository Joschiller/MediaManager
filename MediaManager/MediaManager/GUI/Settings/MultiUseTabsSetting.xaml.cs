using LanguageProvider;
using static LanguageProvider.LanguageProvider;
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
            playlistLabel.Text = getString("Controls.Settings.MultiUseTabs.PlaylistLabel") + ":";
            titleOfTheDayLabel.Text = getString("Controls.Settings.MultiUseTabs.TitleOfTheDayLabel") + ":";
            statisticsLabel.Text = getString("Controls.Settings.MultiUseTabs.StatisticsLabel") + ":";
        }
        public string GetControlName() => "MultiUseTabs";
        public string GetTabName() => getString("Menus.Settings.TabName");
        bool SettingsEditorItem.IsVisible() => true;
        #endregion
        #region Data
        public void LoadData(int? accountIdentifier)
        {
            playlistVisible.IsChecked = GlobalContext.Settings.PlaylistEditorVisible;
            titleOfTheDayVisible.IsChecked = GlobalContext.Settings.TitleOfTheDayVisible;
            statisticsVisible.IsChecked = GlobalContext.Settings.StatisticsOverviewVisible;
            LoadTexts(null);
        }
        public void SaveData()
        {
            GlobalContext.Settings.PlaylistEditorVisible = playlistVisible.IsChecked.HasValue && playlistVisible.IsChecked.Value;
            GlobalContext.Settings.TitleOfTheDayVisible = titleOfTheDayVisible.IsChecked.HasValue && titleOfTheDayVisible.IsChecked.Value;
            GlobalContext.Settings.StatisticsOverviewVisible = statisticsVisible.IsChecked.HasValue && statisticsVisible.IsChecked.Value;
        }
        public bool ValidateData() => true;
        #endregion
    }
}
