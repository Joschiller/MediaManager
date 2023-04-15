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
        bool SettingsEditorItem.IsVisible() => CURRENT_CATALOGUE != null;
        #endregion
        #region Data
        public void LoadData(int? accountIdentifier)
        {
            cbDeletionConfirmationMedium.IsChecked = CURRENT_CATALOGUE.DeletionConfirmationMedium;
            cbDeletionConfirmationPart.IsChecked = CURRENT_CATALOGUE.DeletionConfirmationPart;
            cbDeletionConfirmationPlaylist.IsChecked = CURRENT_CATALOGUE.DeletionConfirmationPlaylist;
            cbDeletionConfirmationTag.IsChecked = CURRENT_CATALOGUE.DeletionConfirmationTag;
            cbShowTitleOfTheDayAsMedium.IsChecked = CURRENT_CATALOGUE.ShowTitleOfTheDayAsMedium;
            LoadTexts(null);
        }
        public void SaveData()
        {
            var catalog = CURRENT_CATALOGUE;
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
