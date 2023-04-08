using MediaManager.Globals.LanguageProvider;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Controls.MultiUseTabs
{
    /// <summary>
    /// Interaction logic for TitleOfTheDay.xaml
    /// </summary>
    public partial class TitleOfTheDay : UserControl, MultiUseTabsControl, UpdatedLanguageUser
    {
        public TitleOfTheDay()
        {
            InitializeComponent();
            ShowNextRandomItem();
            RegisterAtLanguageProvider();
        }

        private void next_Click(object sender, System.Windows.RoutedEventArgs e) => ShowNextRandomItem();
        private string getTagsString(List<ValuedTag> tags)
        {
            var filteredTags = new List<string>();
            tags.ForEach(t => {
                if (t.Value.HasValue && t.Value.Value) filteredTags.Add(t.Tag.Title);
            });
            return string.Join(", ", filteredTags);
        }
        private Part CurrentItem = null;
        private bool ShowBasedOnPart = true; // TODO decide based on setting => show title and description of medium or of part within medium
        private void ShowCurrentItem()
        {
            if (CurrentItem != null)
            {
                title.Text = ShowBasedOnPart ? CurrentItem.Title : CurrentItem.Medium.Title;
                description.Text = ShowBasedOnPart
                    ? (LanguageProvider.getString("Controls.MultiUseTabs.TitleOfTheDay.Medium") + ":" + CurrentItem.Medium.Title + "\n" + "\n"
                    + LanguageProvider.getString("Controls.MultiUseTabs.TitleOfTheDay.Tags") + ":" + getTagsString(Reader.GetTagsForPart(CurrentItem.Id)) + "\n" + "\n"
                    + CurrentItem.Description)
                    : (LanguageProvider.getString("Controls.MultiUseTabs.TitleOfTheDay.Tags") + ":" + getTagsString(Reader.GetTagsForMedium(CurrentItem.MediumId)) + "\n" + "\n"
                    + CurrentItem.Medium.Description);
            }
            else
            {
                title.Text = "";
                description.Text = LanguageProvider.getString("Controls.MultiUseTabs.TitleOfTheDay.Empty");
            }
        }
        private void ShowNextRandomItem()
        {
            if (Reader.CountOfMedia > 0)
            {
                var randomMedium = Reader.Media[new Random().Next(Reader.CountOfMedia)];
                CurrentItem = new List<Part>(randomMedium.Parts)[new Random().Next(randomMedium.Parts.Count)];
            }
            else
            {
                CurrentItem = null;
            }
            ShowCurrentItem();
        }

        public bool Visible { get; set; } = true;

        public ImageSource GetHeader() => new BitmapImage(new Uri("/Resources/title_of_the_day.png", UriKind.Relative));
        public bool GetIsVisible() => Visible;
        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            next.Tooltip = LanguageProvider.getString("Controls.MultiUseTabs.TitleOfTheDay.Next");
        }
    }
}
