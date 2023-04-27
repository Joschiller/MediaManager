using MediaManager.Globals.LanguageProvider;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for PlaylistAdditionDialog.xaml
    /// </summary>
    public partial class PlaylistAdditionDialog : Window, LanguageUser
    {
        #region Setup
        private Playlist selectedPlaylist;
        private int referencedId;
        private Controls.Search.SearchResultMode mode;
        public PlaylistAdditionDialog(int id, Controls.Search.SearchResultMode mode)
        {
            referencedId = id;
            this.mode = mode;
            InitializeComponent();
            LoadTexts(null);
            var allPlaylists = Reader.Playlists.ToList();
            allPlaylists.Add(new Playlist
            {
                Id = -1,
                Title = LanguageProvider.getString("Dialog.PlaylistAddition.New")
            });
            playlists.ItemsSource = allPlaylists;
            playlists.SelectedIndex = 0;
            updateButtons();
        }
        public void LoadTexts(string language)
        {
            labelPlaylists.Text = LanguageProvider.getString("Dialog.PlaylistAddition.LabelPlaylists") + ":";
            labelPlaylistName.Text = LanguageProvider.getString("Dialog.PlaylistAddition.LabelTitle") + ":";
            headline.Text = mode == Controls.Search.SearchResultMode.MediaList ? LanguageProvider.getString("Dialog.PlaylistAddition.HeaderMedium") : LanguageProvider.getString("Dialog.PlaylistAddition.HeaderPart");
            submit.Content = "_" + LanguageProvider.getString("Common.Button.Ok");
            cancel.Content = "_" + LanguageProvider.getString("Common.Button.Cancel");
        }
        #endregion

        #region Handler
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();
        private void playlists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlaylist = (sender as ComboBox).SelectedItem as Playlist;
            labelPlaylistName.Visibility = selectedPlaylist.Id < 0 ? Visibility.Visible : Visibility.Hidden;
            playlistName.Visibility = selectedPlaylist.Id < 0 ? Visibility.Visible : Visibility.Hidden;
            updateButtons();
        }
        private void playlistName_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateButtons();
        }
        private void updateButtons()
        {
            submit.IsEnabled = selectedPlaylist?.Id >= 0 || playlistName.Text.Trim().Length > 0;
        }
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            var playlistId = selectedPlaylist.Id;
            if (playlistId < 0) playlistId = Writer.CreatePlaylist(playlistName.Text.Trim());

            if (mode == Controls.Search.SearchResultMode.MediaList) Reader.GetMedium(referencedId).Parts.ToList().ForEach(p => Writer.AddPartToPlaylist(playlistId, p.Id));
            else Writer.AddPartToPlaylist(playlistId, referencedId);
            DialogResult = true;
        }
        private void cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
        #endregion
    }
}
