using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Controls.MultiUseTabs
{
    /// <summary>
    /// Interaction logic for PlaylistEditor.xaml
    /// </summary>
    public partial class PlaylistEditor : UserControl, MultiUseTabsControl, UpdatedLanguageUser
    {
        #region Setup
        public PlaylistEditor()
        {
            InitializeComponent();
            DataContext = this;
            RegisterAtLanguageProvider();
            lengthInput.SetMin(0);
            lengthInput.SetValue(0);
            create.IsEnabled = false;

            ReloadGUI();
        }
        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            add.Content = LanguageProvider.getString("Common.Button.Add");
            delete.Content = LanguageProvider.getString("Common.Button.Delete");
            titleLabel.Text = LanguageProvider.getString("Controls.MultiUseTabs.PlaylistEditor.Title") + ":";
            lengthLabel.Text = LanguageProvider.getString("Controls.MultiUseTabs.PlaylistEditor.Length") + ":";
            tagLabel.Text = LanguageProvider.getString("Controls.MultiUseTabs.PlaylistEditor.Tag") + ":";
            create.Content = LanguageProvider.getString("Common.Button.Create");
            editGroup.Header = LanguageProvider.getString("Controls.MultiUseTabs.PlaylistEditor.EditGroup");
        }
        ~PlaylistEditor() => LanguageProvider.Unregister(this);
        public ImageSource GetHeader() => new BitmapImage(new Uri("/Resources/playlist.png", UriKind.Relative));
        public bool GetIsVisible() => GlobalContext.Settings.PlaylistEditorVisible;
        public void ReloadGUI()
        {
            ReloadData();
            showDataForSelection();
        }
        public void ReloadData()
        {
            playlists.ItemsSource = null;
            playlists.ItemsSource = CatalogContext.Reader.Lists.Playlists;
            tagInput.ItemsSource = null;
            tagInput.ItemsSource = CatalogContext.Reader.Lists.Tags;
        }
        #endregion

        #region Handler
        #region Playlist List
        private void add_Click(object sender, RoutedEventArgs e)
        {
            addPanel.Visibility = Visibility.Visible;
            playlistEntries.Visibility = Visibility.Collapsed;
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var performDeletion = !GlobalContext.Reader.GetCatalog(CatalogContext.CurrentCatalogId.Value).DeletionConfirmationPlaylist;
            if (!performDeletion)
            {
                var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString("Controls.MultiUseTabs.PlaylistEditor.PlaylistDeletion"));
                performDeletion = confirmation.HasValue && confirmation.Value;
            }
            if (performDeletion)
            {
                GlobalContext.Writer.DeletePlaylist((playlists.SelectedItem as Playlist).Id);
                ReloadData();
            }
        }
        private void playlists_SelectionChanged(object sender, SelectionChangedEventArgs e) => showDataForSelection();
        private void showDataForSelection()
        {
            var selectedItem = playlists.SelectedItem as Playlist;
            delete.IsEnabled = selectedItem != null;
            addPanel.Visibility = selectedItem == null ? Visibility.Visible : Visibility.Collapsed;
            playlistEntries.Visibility = selectedItem == null ? Visibility.Collapsed : Visibility.Visible;
        }
        #endregion
        #region Playlist Editor
        private void titleInput_TextChanged(object sender, TextChangedEventArgs e) => create.IsEnabled = titleInput.Text.Trim().Length > 0;
        private void create_Click(object sender, RoutedEventArgs e)
        {
            var id = CatalogContext.Writer.CreatePlaylist(titleInput.Text.Trim());
            var selectedTag = tagInput.SelectedItem as Tag;

            if (selectedTag != null)
            {
                var allFittingParts = GlobalContext.Reader.GetPartsForTag(selectedTag.Id);
                var random = new Random();
                for (int i = 0; i < lengthInput.Value && allFittingParts.Count > 0; i++)
                {
                    var part = allFittingParts[random.Next(0, allFittingParts.Count)];
                    GlobalContext.Writer.AddPartToPlaylist(id, part.Id);
                    allFittingParts.Remove(part);
                }
            }

            titleInput.Text = "";
            lengthInput.SetValue(0);
            ReloadData();
        }
        private void remove_Click(object sender, RoutedEventArgs e)
        {
            var selectedPlaylist = playlists.SelectedItem as Playlist;
            var selectedPart = (sender as Button).Tag as int?;
            if (selectedPlaylist != null && selectedPart.HasValue)
            {
                GlobalContext.Writer.RemovePartFromPlaylist(selectedPlaylist.Id, selectedPart.Value);
                ReloadData();
                playlists.SelectedItem = CatalogContext.Reader.Lists.Playlists.Find(pl => pl.Id == selectedPlaylist.Id);
            }
        }
        #endregion
        #endregion
    }
}
