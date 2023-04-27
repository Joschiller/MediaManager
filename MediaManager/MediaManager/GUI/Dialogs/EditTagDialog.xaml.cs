using MediaManager.Globals.LanguageProvider;
using System.Windows;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for EditTagDialog.xaml
    /// </summary>
    public partial class EditTagDialog : Window, LanguageUser
    {
        #region Setup
        int? EditedTagId;
        Tag EditedTag;
        public EditTagDialog(int? id)
        {
            EditedTagId = id;
            EditedTag = EditedTagId.HasValue ? Reader.GetTag(EditedTagId.Value) : new Tag { CatalogueId = CURRENT_CATALOGUE.Id, Title = "" };

            InitializeComponent();
            LoadTexts(null);
            DataContext = EditedTag;

            labelOldTag.Visibility = id == null ? Visibility.Hidden : Visibility.Visible;
            oldTag.Visibility = id == null ? Visibility.Hidden : Visibility.Visible;
            oldTag.ToolTip = EditedTag.Title;
            oldTag.Text = EditedTag.Title;
            submit.IsEnabled = newTag.Text.Length > 0;
        }
        public void LoadTexts(string language)
        {
            labelOldTag.Text = LanguageProvider.getString("Dialog.EditTag.LabelOld") + ":";
            labelNewTag.Text = LanguageProvider.getString("Dialog.EditTag.LabelNew") + ":";
            submit.Content = "_" + LanguageProvider.getString("Common.Button.Ok");
            cancel.Content = "_" + LanguageProvider.getString("Common.Button.Cancel");
        }
        #endregion

        #region Handler
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();
        private void newTag_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => submit.IsEnabled = newTag.Text.Length > 0;
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (EditedTagId != null) Writer.SaveTag(EditedTag);
            else Writer.CreateTag(EditedTag);
            DialogResult = true;
        }
        private void cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
        #endregion
    }
}
