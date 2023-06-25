using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using SettingsEditor;
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
            labelDeletionConfirmationMedium.Text = getString("Controls.Settings.Catalog.DeletionConfirmationMedium") + ":";
            labelDeletionConfirmationPart.Text = getString("Controls.Settings.Catalog.DeletionConfirmationPart") + ":";
            labelDeletionConfirmationPlaylist.Text = getString("Controls.Settings.Catalog.DeletionConfirmationPlaylist") + ":";
            labelDeletionConfirmationTag.Text = getString("Controls.Settings.Catalog.DeletionConfirmationTag") + ":";
            labelShowTitleOfTheDayAsMedium.Text = getString("Controls.Settings.Catalog.ShowTitleOfTheDayAsMedium") + ":";
        }
        public string GetControlName() => "Catalog";
        public string GetTabName() => getString("Controls.Settings.Catalog.TabName");
        bool SettingsEditorItem.IsVisible() => CatalogContext.CurrentCatalogId.HasValue;
        #endregion
        #region Data
        public void LoadData(int? accountIdentifier)
        {
            var catalog = GlobalContext.Reader.GetCatalog(CatalogContext.CurrentCatalogId.Value);
            cbDeletionConfirmationMedium.IsChecked = catalog.DeletionConfirmationMedium;
            cbDeletionConfirmationPart.IsChecked = catalog.DeletionConfirmationPart;
            cbDeletionConfirmationPlaylist.IsChecked = catalog.DeletionConfirmationPlaylist;
            cbDeletionConfirmationTag.IsChecked = catalog.DeletionConfirmationTag;
            cbShowTitleOfTheDayAsMedium.IsChecked = catalog.ShowTitleOfTheDayAsMedium;
            LoadTexts(null);
        }
        public void SaveData()
        {
            var catalog = GlobalContext.Reader.GetCatalog(CatalogContext.CurrentCatalogId.Value);
            catalog.DeletionConfirmationMedium = cbDeletionConfirmationMedium.IsChecked.HasValue && cbDeletionConfirmationMedium.IsChecked.Value;
            catalog.DeletionConfirmationPart = cbDeletionConfirmationPart.IsChecked.HasValue && cbDeletionConfirmationPart.IsChecked.Value;
            catalog.DeletionConfirmationPlaylist = cbDeletionConfirmationPlaylist.IsChecked.HasValue && cbDeletionConfirmationPlaylist.IsChecked.Value;
            catalog.DeletionConfirmationTag = cbDeletionConfirmationTag.IsChecked.HasValue && cbDeletionConfirmationTag.IsChecked.Value;
            catalog.ShowTitleOfTheDayAsMedium = cbShowTitleOfTheDayAsMedium.IsChecked.HasValue && cbShowTitleOfTheDayAsMedium.IsChecked.Value;
            GlobalContext.Writer.SaveCatalog(catalog);
        }
        public bool ValidateData() => true;
        #endregion
    }
}
