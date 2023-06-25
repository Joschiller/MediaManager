using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using MediaManager.Globals.SettingsEditor;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Settings
{
    /// <summary>
    /// Interaction logic for BackupSetting.xaml
    /// </summary>
    public partial class BackupSetting : UserControl, LanguageUser, SettingsEditorItem
    {
        #region Setup
        public BackupSetting()
        {
            InitializeComponent();
        }
        public void LoadTexts(string language)
        {
            labelBackup.Text = getString("Controls.Settings.Backup.BackupEnabled") + ":";
            labelExplanation.Text = getString("Controls.Settings.Backup.BackupExplanation");
        }
        public string GetControlName() => "Backup";
        public string GetTabName() => getString("Controls.Settings.Backup.TabName");
        bool SettingsEditorItem.IsVisible() => true;
        #endregion
        #region Data
        public void LoadData(int? accountIdentifier)
        {
            cbBackup.IsChecked = GlobalContext.Settings.BackupEnabled;
            LoadTexts(null);
        }
        public void SaveData()
        {
            GlobalContext.Settings.BackupEnabled = cbBackup.IsChecked.HasValue && cbBackup.IsChecked.Value;
        }
        public bool ValidateData() => true;
        #endregion
    }
}
