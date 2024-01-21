using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using MediaManager.GUI.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.KeyboardShortcutHelper;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for TagMenu.xaml
    /// </summary>
    public partial class TagMenu : Window, UpdatedLanguageUser
    {
        #region Bindings
        public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();
        public Tag SelectedTag { get; set; }
        #endregion

        #region Setup
        public TagMenu()
        {
            InitializeComponent();
            DataContext = this;
            RegisterAtLanguageProvider();
            LoadTags();
        }
        public void RegisterAtLanguageProvider() => RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddTag"] = getString("Menus.Tag.ToolTip.AddTag");
            Resources["btnEditTag"] = getString("Menus.Tag.ToolTip.EditTag");
            Resources["btnDeleteTag"] = getString("Menus.Tag.ToolTip.DeleteTag");
        }
        #endregion

        #region Handler
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosingPermitted()) e.Cancel = true;
        }
        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) => runKeyboardShortcut(e, new System.Collections.Generic.Dictionary<(ModifierKeys Modifiers, Key Key), Action>
        {
            [(ModifierKeys.None, Key.F1)] = OpenHelpMenu,
            [(ModifierKeys.None, Key.Escape)] = Close,
            [(ModifierKeys.Control, Key.N)] = AddTag,
            [(ModifierKeys.Control, Key.E)] = EditTag,
            [(ModifierKeys.Control, Key.D)] = DeleteTag,
            [(ModifierKeys.None, Key.Delete)] = DeleteTag,
            [(ModifierKeys.Control, Key.S)] = mediaTagList.Save,
            [(ModifierKeys.Control, Key.R)] = mediaTagList.Undo,
        });
        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e) => OpenHelpMenu();
        private void btnAddTagClick(object sender, RoutedEventArgs e) => AddTag();
        private void btnEditTagClick(object sender, RoutedEventArgs e) => EditTag();
        private void btnDeleteTagClick(object sender, RoutedEventArgs e) => DeleteTag();
        #endregion
        private void LoadTags()
        {
            Tags.Clear();
            CatalogContext.Reader.Lists.Tags.ForEach(Tags.Add);
        }
        private void tagList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Resources["tagDependentVisibility"] = SelectedTag == null ? Visibility.Hidden : Visibility.Visible;
            mediaTagList.setCurrentTag(SelectedTag);
        }
        #endregion

        #region Functions
        private bool ClosingPermitted()
        {
            var confirmation = !mediaTagList.IsDirty;
            if (!confirmation)
            {
                var result = ShowUnsavedChangesDialog();
                confirmation = result.HasValue;
                // if should save => try saving and cancel closing, if saving fails
                if (result.HasValue && result.Value) mediaTagList.Save();
            }
            return confirmation;
        }
        private void AddTag()
        {
            var result = new EditTagDialog(null).ShowDialog();
            if (result.HasValue && result.Value) LoadTags();
        }
        private void EditTag()
        {
            if (SelectedTag == null) return;
            var result = new EditTagDialog(SelectedTag.Id).ShowDialog();
            if (result.HasValue && result.Value) LoadTags();
        }
        private void DeleteTag()
        {
            if (SelectedTag == null) return;
            var performDeletion = !GlobalContext.Reader.GetCatalog(CatalogContext.CurrentCatalogId.Value).DeletionConfirmationTag;
            if (!performDeletion)
            {
                var confirmation = ShowDeletionConfirmationDialog(getString("Menus.Tag.TagDeletion"));
                performDeletion = confirmation.HasValue && confirmation.Value;
            }
            if (performDeletion)
            {
                GlobalContext.Writer.DeleteTag(SelectedTag.Id);
                LoadTags();
            }
        }
        #endregion
    }
}
