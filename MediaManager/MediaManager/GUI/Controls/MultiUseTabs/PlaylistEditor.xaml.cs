﻿using MediaManager.Globals.LanguageProvider;
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
        public PlaylistEditor()
        {
            InitializeComponent();
            DataContext = this;
            lengthInput.SetMin(0);
            lengthInput.SetValue(0);

            ReloadData();
            showDataForSelection();
            RegisterAtLanguageProvider();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            editGroup.Visibility = Visibility.Visible;
            playlistEntries.Visibility = Visibility.Collapsed;
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var performDeletion = !CURRENT_CATALOGUE.DeletionConfirmationPlaylist;
            if (!performDeletion)
            {
                var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString("Controls.MultiUseTabs.PlaylistEditor.PlaylistDeletion"));
                performDeletion = confirmation.HasValue && confirmation.Value;
            }
            if (performDeletion)
            {
                Writer.DeletePlaylist((playlists.SelectedItem as Playlist).Id);
                ReloadData();
            }
        }
        private void titleInput_TextChanged(object sender, TextChangedEventArgs e) => create.IsEnabled = titleInput.Text.Trim().Length > 0;
        private void create_Click(object sender, RoutedEventArgs e)
        {
            var id = Writer.CreatePlaylist(titleInput.Text.Trim());
            var selectedTag = tagInput.SelectedItem as Tag;

            if (selectedTag != null)
            {
                var allFittingParts = Reader.GetPartsForTag(selectedTag.Id);
                var random = new Random();
                for (int i = 0; i < lengthInput.Value && allFittingParts.Count > 0; i++)
                {
                    var next = allFittingParts[random.Next(0, allFittingParts.Count)];
                    Writer.AddPartToPlaylist(id, next.Id);
                    allFittingParts.Remove(next);
                }
            }

            titleInput.Text = "";
            lengthInput.SetValue(0);
            ReloadData();
        }
        private void playlists_SelectionChanged(object sender, SelectionChangedEventArgs e) => showDataForSelection();
        private void showDataForSelection()
        {
            var selectedItem = playlists.SelectedItem as Playlist;
            delete.IsEnabled = selectedItem != null;
            editGroup.Visibility = selectedItem == null ? Visibility.Visible : Visibility.Collapsed;
            playlistEntries.Visibility = selectedItem == null ? Visibility.Collapsed : Visibility.Visible;
            if (selectedItem != null) playlistEntries.ItemsSource = selectedItem.Parts;
        }
        public void ReloadData()
        {
            playlists.ItemsSource = Reader.Playlists;
            tagInput.ItemsSource = Reader.Tags;
        }

        public void AddPartToCurrentPlaylist(int partId)
        {
            var selectedItem = playlists.SelectedItem as Playlist;
            if (selectedItem != null)
            {
                Writer.AddPartToPlaylist(selectedItem.Id, partId);
                showDataForSelection();
            }
        }
        private void remove_Click(object sender, RoutedEventArgs e)
        {
            var selectedPlaylist = playlists.SelectedItem as Playlist;
            var selectedPart = (sender as Button).Tag as int?;
            if (selectedPlaylist != null && selectedPart.HasValue)
            {
                Writer.RemovePartFromPlaylist(selectedPlaylist.Id, selectedPart.Value);
                showDataForSelection();
            }
        }

        public ImageSource GetHeader() => new BitmapImage(new Uri("/Resources/playlist.png", UriKind.Relative));
        public bool GetIsVisible() => Reader.Settings.PlaylistEditorVisible;
        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            add.Content = LanguageProvider.getString("Common.Button.Add");
            delete.Content = LanguageProvider.getString("Common.Button.Delete");
            titleLabel.Text = LanguageProvider.getString("Controls.MultiUseTabs.PlaylistEditor.Title") + ":";
            lengthLabel.Text = LanguageProvider.getString("Controls.MultiUseTabs.PlaylistEditor.Length") + ":";
            tagLabel.Text = LanguageProvider.getString("Controls.MultiUseTabs.PlaylistEditor.Tag") + ":";
            create.Content = LanguageProvider.getString("Common.Button.Create");
        }
    }
}
