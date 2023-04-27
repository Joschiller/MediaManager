﻿using MediaManager.Globals.LanguageProvider;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static MediaManager.Globals.DataConnector;
using static MediaManager.TagUtils;

namespace MediaManager.GUI.Controls.List
{
    /// <summary>
    /// Interaction logic for MediaTagList.xaml
    /// </summary>
    public partial class MediaTagList : UserControl, UpdatedLanguageUser
    {
        public ObservableCollection<Medium> Media { get; set; } = new ObservableCollection<Medium>();
        public ObservableCollection<MediaTagListElement> Parts { get; set; } = new ObservableCollection<MediaTagListElement>();

        private Tag CurrentTag;
        private int ItemsPerPage = Reader.Settings.ResultListLength;

        public MediaTagList()
        {
            InitializeComponent();
            DataContext = this;
            pager.CurrentPage = 1;
            pager.TotalPages = Reader.CountOfMedia / ItemsPerPage + (Reader.CountOfMedia % ItemsPerPage == 0 ? 0 : 1);
            LoadTagsOfSelectedMedium();
        }

        public void setCurrentTag(Tag tag)
        {
            CurrentTag = tag;
            LoadTagsOfSelectedMedium();
        }

        #region Navigation
        private void pager_PageChanged(int newPage)
        {
            Media.Clear();
            var dbMedia = Reader.Media;
            for (int i = 0; i < ItemsPerPage; i++)
            {
                if ((newPage - 1) * ItemsPerPage + i >= dbMedia.Count) break;
                Media.Add(dbMedia[(newPage - 1) * ItemsPerPage + i]);
            }
        }
        private void mediaList_SelectionChanged(object sender, SelectionChangedEventArgs e) => LoadTagsOfSelectedMedium();
        #endregion

        private void LoadTagsOfSelectedMedium()
        {
            Parts.Clear();
            if (CurrentTag != null && mediaList.SelectedItem != null)
            {
                mediumTag.Visibility = Visibility.Visible;
                partList.Visibility = Visibility.Visible;
                saveButton.Visibility = Visibility.Visible;
                var currentMedium = mediaList.SelectedItem as Medium;
                foreach (var p in currentMedium.Parts)
                {
                    var value = Reader.GetTagsForPart(p.Id).Find(t => t.Tag.Id == CurrentTag.Id).Value;
                    Parts.Add(new MediaTagListElement
                    {
                        Part = p,
                        Value = value,
                        Icon = GetIconForTagValue(value)
                    });
                }
                mediumTag.Value = Reader.GetTagsForMedium(currentMedium.Id).Find(t => t.Tag.Id == CurrentTag.Id).Value;
                mediumTag.TagName = currentMedium.Title;
                saveButton.Enabled = false;
            }
            else
            {
                mediumTag.Visibility = Visibility.Hidden;
                partList.Visibility = Visibility.Hidden;
                saveButton.Visibility = Visibility.Hidden;
                saveButton.Enabled = false;
            }
        }

        private void mediumTag_TagValueChanged(Atoms.TagCheckbox sender, bool? newValue)
        {
            if (mediaList.SelectedItem == null) return;

            if (newValue.HasValue)
            {
                // overwrite part tags
                var currentPartTags = Parts.ToList();
                var newIcon = GetIconForTagValue(newValue);
                currentPartTags.ForEach(p =>
                {
                    p.Value = newValue;
                    p.Icon = newIcon;
                });
                Parts.Clear();
                currentPartTags.ForEach(Parts.Add);
            }
            saveButton.Enabled = true;
        }
        private void Grid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (mediaList.SelectedItem == null) return;

            var partId = (int)((Grid)sender).Tag;
            var currentTags = Parts.ToList();
            var tagToEdit = currentTags.FirstOrDefault(p => p.Part.Id == partId);
            if (tagToEdit == null) return;

            bool? newValue = null;
            if (e.ChangedButton == MouseButton.Left)
            {
                if (!tagToEdit.Value.HasValue) newValue = true;
                else if (tagToEdit.Value.Value) newValue = false;
                else if (!tagToEdit.Value.Value) newValue = null;
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                if (!tagToEdit.Value.HasValue) newValue = false;
                else if (tagToEdit.Value.Value) newValue = null;
                else if (!tagToEdit.Value.Value) newValue = true;
            }
            tagToEdit.Value = newValue;
            tagToEdit.Icon = GetIconForTagValue(newValue);

            Parts.Clear();
            currentTags.ForEach(Parts.Add);

            // reset media tag
            mediumTag.Value = null;

            saveButton.Enabled = true;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (mediaList.SelectedItem == null) return;

            var currentMedium = mediaList.SelectedItem as Medium;
            // update medium
            var mediumTags = Reader.GetTagsForMedium(currentMedium.Id);
            mediumTags.FirstOrDefault(t => t.Tag.Id == CurrentTag.Id).Value = mediumTag.Value;
            Writer.SaveMedium(Reader.GetMedium(currentMedium.Id), mediumTags);

            // update parts
            foreach (var p in Parts)
            {
                var partTags = Reader.GetTagsForPart(p.Part.Id);
                partTags.FirstOrDefault(t => t.Tag.Id == CurrentTag.Id).Value = p.Value;
                Writer.SavePart(Reader.GetPart(p.Part.Id), partTags);
            }

            LoadTagsOfSelectedMedium();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            saveButton.Tooltip = LanguageProvider.getString("Controls.MediaTagList.Save");
        }
        ~MediaTagList() => LanguageProvider.Unregister(this);
    }
}
