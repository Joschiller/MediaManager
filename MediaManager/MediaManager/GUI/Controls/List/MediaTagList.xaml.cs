using LanguageProvider;
using static LanguageProvider.LanguageProvider;
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
        #region Bindings
        public ObservableCollection<Medium> Media { get; set; } = new ObservableCollection<Medium>();
        public ObservableCollection<MediaTagListElement> Parts { get; set; } = new ObservableCollection<MediaTagListElement>();
        #endregion

        #region Setup
        private Tag CurrentTag;
        private int ItemsPerPage = GlobalContext.Settings.ResultListLength;
        public MediaTagList()
        {
            InitializeComponent();
            DataContext = this;
            pager.CurrentPage = 1;
            pager.TotalPages = CatalogContext.Reader.Statistics.CountOfMedia / ItemsPerPage + (CatalogContext.Reader.Statistics.CountOfMedia % ItemsPerPage == 0 ? 0 : 1);
            pager.setItemCount(CatalogContext.Reader.Statistics.CountOfMedia);
            LoadTagsOfSelectedMedium();
        }
        public void RegisterAtLanguageProvider() => Register(this);
        public void LoadTexts(string language)
        {
            saveButton.Tooltip = getString("Controls.MediaTagList.Save");
        }
        ~MediaTagList() => Unregister(this);
        #endregion

        #region Getter/Setter
        public void setCurrentTag(Tag tag)
        {
            CurrentTag = tag;
            LoadTagsOfSelectedMedium();
        }
        #endregion

        #region Handler
        #region Navigation
        private void pager_PageChanged(int newPage)
        {
            Media.Clear();
            var dbMedia = CatalogContext.Reader.Lists.Media;
            for (int i = 0; i < ItemsPerPage; i++)
            {
                if ((newPage - 1) * ItemsPerPage + i >= dbMedia.Count) break;
                Media.Add(dbMedia[(newPage - 1) * ItemsPerPage + i]);
            }
        }
        private void mediaList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right && pager.CurrentPage < pager.TotalPages)
            {
                pager.CurrentPage++;
                e.Handled = true;
            }
            if (e.Key == Key.Left && pager.CurrentPage > 1)
            {
                pager.CurrentPage--;
                e.Handled = true;
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
                    var value = GlobalContext.Reader.GetTagsForPart(p.Id).Find(t => t.Tag.Id == CurrentTag.Id).Value;
                    Parts.Add(new MediaTagListElement
                    {
                        Part = p,
                        Value = value,
                        Icon = GetIconForTagValue(value)
                    });
                }
                mediumTag.Value = GlobalContext.Reader.GetTagsForMedium(currentMedium.Id).Find(t => t.Tag.Id == CurrentTag.Id).Value;
                mediumTag.TagName = currentMedium.Title;
                saveButton.IsEnabled = false;
            }
            else
            {
                mediumTag.Visibility = Visibility.Hidden;
                partList.Visibility = Visibility.Hidden;
                saveButton.Visibility = Visibility.Hidden;
                saveButton.IsEnabled = false;
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
            saveButton.IsEnabled = true;
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

            saveButton.IsEnabled = true;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e) => Save();
        #endregion

        #region Functions
        public void Undo() => LoadTagsOfSelectedMedium();
        public void Save()
        {
            if (mediaList.SelectedItem == null) return;

            var currentMedium = mediaList.SelectedItem as Medium;
            // update medium
            var mediumTags = GlobalContext.Reader.GetTagsForMedium(currentMedium.Id);
            mediumTags.FirstOrDefault(t => t.Tag.Id == CurrentTag.Id).Value = mediumTag.Value;
            CatalogContext.Writer.SaveMedium(GlobalContext.Reader.GetMedium(currentMedium.Id), mediumTags);

            // update parts
            foreach (var p in Parts)
            {
                var partTags = GlobalContext.Reader.GetTagsForPart(p.Part.Id);
                partTags.FirstOrDefault(t => t.Tag.Id == CurrentTag.Id).Value = p.Value;
                CatalogContext.Writer.SavePart(GlobalContext.Reader.GetPart(p.Part.Id), partTags);
            }

            LoadTagsOfSelectedMedium();
        }
        #endregion
    }
}
