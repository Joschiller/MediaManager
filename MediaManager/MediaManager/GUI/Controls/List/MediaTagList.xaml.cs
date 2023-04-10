using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Controls.List
{
    /// <summary>
    /// Interaction logic for MediaTagList.xaml
    /// </summary>
    public partial class MediaTagList : UserControl
    {
        public ObservableCollection<Medium> Media { get; set; } = new ObservableCollection<Medium>();
        public ObservableCollection<MediaTagListElement> Parts { get; set; } = new ObservableCollection<MediaTagListElement>();

        private Tag CurrentTag;
        private int ItemsPerPage = Reader.Settings.ResultListLength;

        public MediaTagList()
        {
            InitializeComponent();
            pager.CurrentPage = 1;
            pager.TotalPages = Reader.CountOfMedia / ItemsPerPage + (Reader.CountOfMedia % ItemsPerPage == 0 ? 0 : 1);
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
                var currentMedium = mediaList.SelectedItem as Medium;
                foreach (var p in currentMedium.Parts)
                {
                    Parts.Add(new MediaTagListElement
                    {
                        TagValue = Reader.GetTagsForPart(p.Id).Find(t => t.Tag.Id == CurrentTag.Id).Value,
                        Part = p
                    });
                }
                mediumTag.Value = Reader.GetTagsForMedium(currentMedium.Id).Find(t => t.Tag.Id == CurrentTag.Id).Value;
                mediumTag.TagName = currentMedium.Title;
            }
            else
            {
                mediumTag.Visibility = Visibility.Hidden;
                partList.Visibility = Visibility.Hidden;
            }
        }

        private void mediumTag_TagValueChanged(Atoms.TagCheckbox sender, bool? newValue)
        {
            throw new NotImplementedException();
            // TODO: store current value within component
            // TODO: trigger onchange
        }
        private void TagCheckbox_TagValueChanged(Atoms.TagCheckbox sender, bool? newValue)
        {
            throw new NotImplementedException();
            // TODO: store current value within component
            // TODO: trigger onchange
        }
    }
}
