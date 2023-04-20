using MediaManager.Globals.LanguageProvider;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector.Reader;

namespace MediaManager.GUI.Controls.Analyze
{
    /// <summary>
    /// Interaction logic for AnalyzePreview.xaml
    /// </summary>
    public partial class AnalyzePreview : UserControl, UpdatedLanguageUser
    {
        public delegate void AnalyzeEditHandler(AnalyzeMode mode, AnalyzeListElement element);
        public event AnalyzeEditHandler StartEditing;

        private AnalyzeMode mode;
        private AnalyzeListElement element;

        public AnalyzePreview()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

        public void LoadPreview(AnalyzeMode mode, AnalyzeListElement element)
        {
            this.mode = mode;
            this.element = element;
            doubled.Visibility = Visibility.Collapsed;
            tagHighlight.Visibility = Visibility.Collapsed;
            elementViewer.Visibility = Visibility.Collapsed;

            if (element == null) return;

            switch(mode)
            {
                case AnalyzeMode.MediumDoubled:
                    doubled.Visibility = Visibility.Visible;
                    mediumSelection.ItemsSource = GetDoubledMediaToMediumTitle(element.Text);
                    mediumSelection.SelectedIndex = 0;
                    break;
                case AnalyzeMode.MediumCommonTags:
                    tagHighlight.Visibility = Visibility.Visible;
                    var nonCommonTags = GetNonCommonTagsOfMedium(element.Id);
                    var tagsOfMedium = GetTagsForMedium(element.Id);
                    highlightedTagList.SetTagList(tagsOfMedium, new System.Collections.Generic.List<int>(), nonCommonTags);
                    mediaPartListTag.ItemsSource = GetMedium(element.Id).Parts.Select(p => p.Title).ToList();
                    break;
                case AnalyzeMode.MediumEmpty:
                case AnalyzeMode.MediumDescription:
                case AnalyzeMode.MediumTags:
                case AnalyzeMode.MediumLocation:
                    elementViewer.Visibility = Visibility.Visible;
                    var medium = GetMedium(element.Id);
                    elementViewer.Medium = new Edit.EditableMedium
                    {
                        Id = medium.Id,
                        CatalogueId = medium.CatalogueId,
                        Title = medium.Title,
                        Description = medium.Description,
                        Location = medium.Location,
                        Tags = GetTagsForMedium(element.Id)
                    };
                    break;
                case AnalyzeMode.PartDescription:
                case AnalyzeMode.PartTags:
                case AnalyzeMode.PartLength:
                case AnalyzeMode.PartPublication:
                case AnalyzeMode.PartImage:
                    elementViewer.Visibility = Visibility.Visible;
                    var part = GetPart(element.Id);
                    elementViewer.Part = new Edit.EditablePart
                    {
                        Id = part.Id,
                        MediumId = part.MediumId,
                        Title = part.Title,
                        Description = part.Description,
                        Favourite = part.Favourite,
                        Length = part.Length,
                        Publication_Year = part.Publication_Year,
                        Image = part.Image,
                        Tags = GetTagsForPart(element.Id),
                        TagsBlockedByMedium = GetTagsForMedium(part.MediumId).Where(t => t.Value.HasValue).Select(t => t.Tag.Id).ToList()
                    };
                    break;
            }
        }
        private void mediumSelection_SelectionChanged(object sender, SelectionChangedEventArgs e) => mediaPartListDoubled.ItemsSource = (mediumSelection.SelectedItem as Medium).Parts;

        private void elementViewer_EditClicked(Edit.ElementMode mode, int id) => StartEditing(this.mode, element);
        private void editButton_Click(object sender, RoutedEventArgs e) => StartEditing(mode, element);

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            editButton.ToolTip = LanguageProvider.getString("Common.Button.Edit");
            labelMediumSelection.Text = LanguageProvider.getString("Controls.Analyze.AnalyzePreview.MediumSelection") + ":";
            labelMediaTagList.Text = LanguageProvider.getString("Controls.Analyze.AnalyzePreview.MediaTagListLabel") + ":";
            labelMediaPartList.Text = LanguageProvider.getString("Controls.Analyze.AnalyzePreview.PartListLabel") + ":";
        }
    }
}
