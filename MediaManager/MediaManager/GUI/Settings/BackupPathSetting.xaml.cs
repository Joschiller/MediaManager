using MediaManager.Globals.LanguageProvider;
using MediaManager.Globals.SettingsEditor;
using System.Windows;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Settings
{
    /// <summary>
    /// Interaction logic for BackupPathSetting.xaml
    /// </summary>
    public partial class BackupPathSetting : UserControl, LanguageUser, SettingsEditorItem
    {
        #region Setup
        public BackupPathSetting()
        {
            InitializeComponent();
        }
        public void LoadTexts(string language)
        {
            labelBackupPath.Text = LanguageProvider.getString("Controls.Settings.Backup.BackupPath") + ":";
            select.Content = LanguageProvider.getString("Controls.Settings.Backup.BtnSelect");
            reset.Content = LanguageProvider.getString("Controls.Settings.Backup.BtnReset");
        }
        public string GetControlName() => "Backup";
        public string GetTabName() => LanguageProvider.getString("Controls.Settings.Backup.TabName");
        bool SettingsEditorItem.IsVisible() => true;
        #endregion
        #region Data
        public void LoadData(int? accountIdentifier)
        {
            valueBackupPath.Text = GlobalContext.Settings.BackupPath;
            LoadTexts(null);
        }
        public void SaveData()
        {
            GlobalContext.Settings.BackupPath = valueBackupPath.Text.Trim();
        }
        public bool ValidateData() => valueBackupPath.Text.Trim().Length > 0;
        #endregion
        #region Handler
        private string showLoadFileDialog()
        {
            var fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.SelectedPath = valueBackupPath.Text;
            fbd.ShowDialog();
            return fbd.SelectedPath;
        }
        private void select_Click(object sender, RoutedEventArgs e)
        {
            var folderName = showLoadFileDialog();
            if (folderName == null || folderName == "") return;
            if (folderName.Length > 512)
            {
                ShowDefaultDialog(LanguageProvider.getString("Controls.Settings.Backup.PathToLong"), Globals.DefaultDialogs.SuccessMode.Error);
                return;
            }
            valueBackupPath.Text = folderName;
        }
        private void reset_Click(object sender, RoutedEventArgs e) => valueBackupPath.Text = DefaultBackupPath;
        #endregion
    }
}
