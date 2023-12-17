using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Controls.Analyze
{
    /// <summary>
    /// Interaction logic for AnalyzePreview.xaml
    /// </summary>
    public partial class AnalyzePreview : UserControl, UpdatedLanguageUser
    {
        #region Events
        public delegate void AnalyzeEditHandler(AnalyzeMode mode, AnalyzeListElement element);
        public event AnalyzeEditHandler StartEditing;
        #endregion

        #region Setup
        private AnalyzeMode mode;
        private AnalyzeListElement element;
        public AnalyzePreview()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }
        public void RegisterAtLanguageProvider() => Register(this);
        public void LoadTexts(string language)
        {
            editButton.ToolTip = getString("Common.Button.Edit");
            labelMediumSelection.Text = getString("Controls.Analyze.AnalyzePreview.MediumSelection") + ":";
            labelMediaTagList.Text = getString("Controls.Analyze.AnalyzePreview.MediaTagListLabel") + ":";
            labelMediaPartList.Text = getString("Controls.Analyze.AnalyzePreview.PartListLabel") + ":";
        }
        ~AnalyzePreview() => Unregister(this);
        #endregion

        #region Getter/Setter
        public void LoadPreview(AnalyzeMode mode, AnalyzeListElement element)
        {
            this.mode = mode;
            this.element = element;
            editButton.Visibility = Visibility.Collapsed;
            doubled.Visibility = Visibility.Collapsed;
            tagHighlight.Visibility = Visibility.Collapsed;
            elementViewer.Visibility = Visibility.Collapsed;

            if (element == null) return;

            switch(mode)
            {
                case AnalyzeMode.MediumDoubled:
                    editButton.Visibility = Visibility.Visible;
                    doubled.Visibility = Visibility.Visible;
                    mediumSelection.ItemsSource = CatalogContext.Reader.Analysis.GetDoubledMediaToMediumTitle(element.Text);
                    mediumSelection.SelectedIndex = 0;
                    break;
                case AnalyzeMode.MediumCommonTags:
                    editButton.Visibility = Visibility.Visible;
                    tagHighlight.Visibility = Visibility.Visible;
                    var nonCommonTags = CatalogContext.Reader.Analysis.GetNonCommonTagsOfMedium(element.Id);
                    var tagsOfMedium = GlobalContext.Reader.GetTagsForMedium(element.Id);
                    highlightedTagList.SetTagList(tagsOfMedium, new System.Collections.Generic.List<int>(), nonCommonTags);
                    mediaPartListTag.ItemsSource = GlobalContext.Reader.GetMedium(element.Id).Parts.Select(p => p.Title).ToList();
                    break;
                case AnalyzeMode.MediumEmpty:
                case AnalyzeMode.MediumDescription:
                case AnalyzeMode.MediumTags:
                case AnalyzeMode.MediumLocation:
                    editButton.Visibility = Visibility.Visible;
                    elementViewer.Visibility = Visibility.Visible;
                    var medium = GlobalContext.Reader.GetMedium(element.Id);
                    elementViewer.Medium = new Edit.EditableMedium
                    {
                        Id = medium.Id,
                        CatalogId = medium.CatalogId,
                        Title = medium.Title,
                        Description = medium.Description,
                        Location = medium.Location,
                        Tags = GlobalContext.Reader.GetTagsForMedium(element.Id)
                    };
                    break;
                case AnalyzeMode.PartDescription:
                case AnalyzeMode.PartTags:
                case AnalyzeMode.PartLength:
                case AnalyzeMode.PartPublication:
                case AnalyzeMode.PartImage:
                    editButton.Visibility = Visibility.Visible;
                    elementViewer.Visibility = Visibility.Visible;
                    var part = GlobalContext.Reader.GetPart(element.Id);
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
                        Tags = GlobalContext.Reader.GetTagsForPart(element.Id),
                        TagsBlockedByMedium = GlobalContext.Reader.GetTagsForMedium(part.MediumId).Where(t => t.Value.HasValue).Select(t => t.Tag.Id).ToList()
                    };
                    break;
            }
        }
        #endregion

        #region Handler
        private void mediumSelection_SelectionChanged(object sender, SelectionChangedEventArgs e) => mediaPartListDoubled.ItemsSource = mediumSelection.SelectedItem != null ? (mediumSelection.SelectedItem as Medium).Parts : new List<Part>();
        private void elementViewer_EditClicked(Edit.ElementMode mode, int id) => StartEditing(this.mode, element);
        private void editButton_Click(object sender, RoutedEventArgs e) => StartEditing(mode, element);
        #endregion

        public void TriggerStartEditing() => StartEditing(mode, element);
    }
}
