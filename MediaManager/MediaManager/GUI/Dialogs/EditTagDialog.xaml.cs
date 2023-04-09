using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
using static MediaManager.Globals.DataConnector.Reader;
using static MediaManager.Globals.DataConnector.Writer;

namespace MediaManager.GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for EditTagDialog.xaml
    /// </summary>
    public partial class EditTagDialog : Window, LanguageUser
    {
        int? EditedTagId;
        Tag EditedTag;

        public EditTagDialog(int? id)
        {
            EditedTagId = id;
            EditedTag = EditedTagId.HasValue ? GetTag(EditedTagId.Value) : new Tag { Title = "" };

            InitializeComponent();
            LoadTexts(LanguageProvider.CurrentLanguage);
            DataContext = EditedTag;

            labelOldTag.Visibility = id == null ? Visibility.Hidden : Visibility.Visible;
            oldTag.Visibility = id == null ? Visibility.Hidden : Visibility.Visible;
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

        private void newTag_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => submit.IsEnabled = newTag.Text.Length > 0;
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            if (EditedTagId != null) SaveTag(EditedTag);
            else CreateTag(EditedTag);
        }
        private void cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
