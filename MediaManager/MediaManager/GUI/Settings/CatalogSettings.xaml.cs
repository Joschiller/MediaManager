using MediaManager.Globals.LanguageProvider;
using MediaManager.Globals.SettingsEditor;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Settings
{
    /// <summary>
    /// Interaction logic for CatalogSettings.xaml
    /// </summary>
    public partial class CatalogSettings : UserControl, LanguageUser, SettingsEditorItem
    {
        #region Setup
        public CatalogSettings()
        {
            InitializeComponent();
        }
        public void LoadTexts(string language)
        {
            labelDeletionConfirmationMedium.Text = LanguageProvider.getString("Controls.Settings.Catalog.DeletionConfirmationMedium") + ":";
            labelDeletionConfirmationPart.Text = LanguageProvider.getString("Controls.Settings.Catalog.DeletionConfirmationPart") + ":";
            labelDeletionConfirmationPlaylist.Text = LanguageProvider.getString("Controls.Settings.Catalog.DeletionConfirmationPlaylist") + ":";
            labelDeletionConfirmationTag.Text = LanguageProvider.getString("Controls.Settings.Catalog.DeletionConfirmationTag") + ":";
            labelShowTitleOfTheDayAsMedium.Text = LanguageProvider.getString("Controls.Settings.Catalog.ShowTitleOfTheDayAsMedium") + ":";
        }
        public string GetControlName() => "Catalog";
        public string GetTabName() => LanguageProvider.getString("Controls.Settings.Catalog.TabName");
        bool SettingsEditorItem.IsVisible() => CURRENT_CATALOG != null;
        #endregion
        #region Data
        public void LoadData(int? accountIdentifier)
        {
            cbDeletionConfirmationMedium.IsChecked = CURRENT_CATALOG.DeletionConfirmationMedium;
            cbDeletionConfirmationPart.IsChecked = CURRENT_CATALOG.DeletionConfirmationPart;
            cbDeletionConfirmationPlaylist.IsChecked = CURRENT_CATALOG.DeletionConfirmationPlaylist;
            cbDeletionConfirmationTag.IsChecked = CURRENT_CATALOG.DeletionConfirmationTag;
            cbShowTitleOfTheDayAsMedium.IsChecked = CURRENT_CATALOG.ShowTitleOfTheDayAsMedium;
            LoadTexts(null);
        }
        public void SaveData()
        {
            var catalog = CURRENT_CATALOG;
            catalog.DeletionConfirmationMedium = cbDeletionConfirmationMedium.IsChecked.HasValue && cbDeletionConfirmationMedium.IsChecked.Value;
            catalog.DeletionConfirmationPart = cbDeletionConfirmationPart.IsChecked.HasValue && cbDeletionConfirmationPart.IsChecked.Value;
            catalog.DeletionConfirmationPlaylist = cbDeletionConfirmationPlaylist.IsChecked.HasValue && cbDeletionConfirmationPlaylist.IsChecked.Value;
            catalog.DeletionConfirmationTag = cbDeletionConfirmationTag.IsChecked.HasValue && cbDeletionConfirmationTag.IsChecked.Value;
            catalog.ShowTitleOfTheDayAsMedium = cbShowTitleOfTheDayAsMedium.IsChecked.HasValue && cbShowTitleOfTheDayAsMedium.IsChecked.Value;
            Writer.SaveCatalog(catalog);
        }
        public bool ValidateData() => true;
        #endregion
    }
}
