using LanguageProvider;
using static LanguageProvider.LanguageProvider;
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
        #region Setup
        private int? EditedCatalogId;
        private Catalog EditedCatalog;
        public EditCatalogDialog(int? id)
        {
            EditedCatalogId = id;
            EditedCatalog = EditedCatalogId.HasValue
                ? GlobalContext.Reader.GetCatalog(EditedCatalogId.Value)
                : new Catalog
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
            oldTitle.ToolTip = EditedCatalog.Title;
            oldTitle.Text = EditedCatalog.Title;
            submit.IsEnabled = newTitle.Text.Length > 0;
        }
        public void LoadTexts(string language)
        {
            labelOldTitle.Text = getString("Dialog.EditCatalog.LabelOldTitle") + ":";
            labelNewTitle.Text = getString("Dialog.EditCatalog.LabelNewTitle") + ":";
            labelDescription.Text = getString("Dialog.EditCatalog.LabelDescription") + ":";
            headingSettings.Text = getString("Dialog.EditCatalog.HeadingSettings") + ":";
            cbDeletionConfirmationMedium.Content = getString("Dialog.EditCatalog.LabelDeletionConfirmationMedium");
            cbDeletionConfirmationPart.Content = getString("Dialog.EditCatalog.LabelDeletionConfirmationPart");
            cbDeletionConfirmationPlaylist.Content = getString("Dialog.EditCatalog.LabelDeletionConfirmationPlaylist");
            cbDeletionConfirmationTag.Content = getString("Dialog.EditCatalog.LabelDeletionConfirmationTag");
            cbShowTitleOfTheDayAsMedium.Content = getString("Dialog.EditCatalog.LabelShowTitleOfTheDayAsMedium");
            submit.Content = "_" + getString("Common.Button.Ok");
            cancel.Content = "_" + getString("Common.Button.Cancel");
        }
        #endregion

        #region Handler
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();
        private void newTitle_TextChanged(object sender, TextChangedEventArgs e) => submit.IsEnabled = newTitle.Text.Length > 0;
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (EditedCatalogId != null) GlobalContext.Writer.SaveCatalog(EditedCatalog);
            else GlobalContext.Writer.CreateCatalog(EditedCatalog);
            if (GlobalContext.Reader.Catalogs.Count == 1) CatalogContext.SetCurrentCatalog(GlobalContext.Reader.Catalogs[0]);
            DialogResult = true;
        }
        private void cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
        #endregion
    }
}
