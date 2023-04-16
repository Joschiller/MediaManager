using MediaManager.Globals.LanguageProvider;
using MediaManager.GUI.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for TagMenu.xaml
    /// </summary>
    public partial class TagMenu : Window, UpdatedLanguageUser
    {
        public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();
        public Tag SelectedTag { get; set; }

        #region Setup
        public TagMenu()
        {
            InitializeComponent();
            DataContext = this;
            RegisterAtLanguageProvider();
            LoadTags();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddTag"] = LanguageProvider.getString("Menus.Tag.ToolTip.AddTag");
            Resources["btnEditTag"] = LanguageProvider.getString("Menus.Tag.ToolTip.EditTag");
            Resources["btnDeleteTag"] = LanguageProvider.getString("Menus.Tag.ToolTip.DeleteTag");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnAddTagClick(object sender, RoutedEventArgs e)
        {
            var result = new EditTagDialog(null).ShowDialog();
            if (result.HasValue && result.Value) LoadTags();
        }
        private void btnEditTagClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTag == null) return;
            var result = new EditTagDialog(SelectedTag.Id).ShowDialog();
            if (result.HasValue && result.Value) LoadTags();
        }
        private void btnDeleteTagClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTag == null) return;
            var performDeletion = !CURRENT_CATALOGUE.DeletionConfirmationTag;
            if (!performDeletion)
            {
                var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString("Menus.Tag.TagDeletion"));
                performDeletion = confirmation.HasValue && confirmation.Value;
            }
            if (performDeletion)
            {
                Writer.DeleteTag(SelectedTag.Id);
                LoadTags();
            }
        }
        #endregion

        private void LoadTags()
        {
            Tags.Clear();
            Reader.Tags.ForEach(Tags.Add);
        }
        private void tagList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Resources["tagDependentVisibility"] = SelectedTag == null ? Visibility.Hidden : Visibility.Visible;
            mediaTagList.setCurrentTag(SelectedTag);
        }
    }
}
