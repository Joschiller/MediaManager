using MediaManager.Globals.LanguageProvider;
using System.Windows;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for EditCatalogDialog.xaml
    /// </summary>
    public partial class EditCatalogDialog : Window, LanguageUser
    {
        int? EditedCatalogId;
        Catalogue EditedCatalog;

        public EditCatalogDialog(int? id)
        {
            EditedCatalogId = id;
            EditedCatalog = EditedCatalogId.HasValue
                ? Reader.GetCatalog(EditedCatalogId.Value)
                : new Catalogue
                {
                    Title = "",
                    Description = "",
                    DeletionConfirmationMedium = true,
                    DeletionConfirmationPart = true,
                    DeletionConfirmationPlaylist = true,
                    DeletionConfirmationTag = true,
                    ShowTitleOfTheDayAsMedium = false,
                };

            InitializeComponent();
            LoadTexts(null);
            DataContext = EditedCatalog;
            labelOldTitle.Visibility = id == null ? Visibility.Hidden : Visibility.Visible;
            oldTitle.Visibility = id == null ? Visibility.Hidden : Visibility.Visible;
            oldTitle.Text = EditedCatalog.Title;
            submit.IsEnabled = newTitle.Text.Length > 0;
        }

        public void LoadTexts(string language)
        {
            labelOldTitle.Text = LanguageProvider.getString("Dialog.EditCatalog.LabelOldTitle") + ":";
            labelNewTitle.Text = LanguageProvider.getString("Dialog.EditCatalog.LabelNewTitle") + ":";
            labelDescription.Text = LanguageProvider.getString("Dialog.EditCatalog.LabelDescription") + ":";
            headingSettings.Text = LanguageProvider.getString("Dialog.EditCatalog.HeadingSettings") + ":";
            cbDeletionConfirmationMedium.Content = LanguageProvider.getString("Dialog.EditCatalog.LabelDeletionConfirmationMedium");
            cbDeletionConfirmationPart.Content = LanguageProvider.getString("Dialog.EditCatalog.LabelDeletionConfirmationPart");
            cbDeletionConfirmationPlaylist.Content = LanguageProvider.getString("Dialog.EditCatalog.LabelDeletionConfirmationPlaylist");
            cbDeletionConfirmationTag.Content = LanguageProvider.getString("Dialog.EditCatalog.LabelDeletionConfirmationTag");
            cbShowTitleOfTheDayAsMedium.Content = LanguageProvider.getString("Dialog.EditCatalog.LabelShowTitleOfTheDayAsMedium");
            submit.Content = "_" + LanguageProvider.getString("Common.Button.Ok");
            cancel.Content = "_" + LanguageProvider.getString("Common.Button.Cancel");
        }
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();

        private void newTitle_TextChanged(object sender, TextChangedEventArgs e) => submit.IsEnabled = newTitle.Text.Length > 0;

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (EditedCatalogId != null) Writer.SaveCatalog(EditedCatalog);
            else Writer.CreateCatalog(EditedCatalog);
            if (Reader.Catalogs.Count == 1) CURRENT_CATALOGUE = Reader.Catalogs[0];
            DialogResult = true;
        }
        private void cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
