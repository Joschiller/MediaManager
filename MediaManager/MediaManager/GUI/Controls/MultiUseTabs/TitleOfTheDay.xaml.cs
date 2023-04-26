using MediaManager.Globals.LanguageProvider;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private void ShowCurrentItem()
        {
            if (CurrentItem != null)
            {
                title.Text = CURRENT_CATALOGUE.ShowTitleOfTheDayAsMedium ? CurrentItem.Medium.Title : CurrentItem.Title;
                description.Text = CURRENT_CATALOGUE.ShowTitleOfTheDayAsMedium
                    ? (LanguageProvider.getString("Controls.MultiUseTabs.TitleOfTheDay.Tags") + ": " + getTagsString(Reader.GetTagsForMedium(CurrentItem.MediumId)) + "\n" + "\n"
                    + CurrentItem.Medium.Description)
                    : (LanguageProvider.getString("Controls.MultiUseTabs.TitleOfTheDay.Medium") + ": " + CurrentItem.Medium.Title + "\n" + "\n"
                    + LanguageProvider.getString("Controls.MultiUseTabs.TitleOfTheDay.Tags") + ": " + getTagsString(Reader.GetTagsForPart(CurrentItem.Id)) + "\n" + "\n"
                    + CurrentItem.Description);
            }
            else
            {
                title.Text = "";
                description.Text = LanguageProvider.getString("Controls.MultiUseTabs.TitleOfTheDay.Empty");
            }
        }
        private void ShowNextRandomItem()
        {
            var possibleMedia = Reader.Media.Where(m => m.Parts.Count > 0).ToList();
            if (possibleMedia.Count > 0)
            {
                var randomMedium = possibleMedia[new Random().Next(possibleMedia.Count)];
                CurrentItem = new List<Part>(randomMedium.Parts)[new Random().Next(randomMedium.Parts.Count)];
            }
            else
            {
                CurrentItem = null;
            }
            ShowCurrentItem();
        }

        public ImageSource GetHeader() => new BitmapImage(new Uri("/Resources/title_of_the_day.png", UriKind.Relative));
        public bool GetIsVisible() => Reader.Settings.TitleOfTheDayVisible;
        public void ReloadGUI() => ShowNextRandomItem();
        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            next.Tooltip = LanguageProvider.getString("Controls.MultiUseTabs.TitleOfTheDay.Next");
            ShowCurrentItem();
        }
    }
}
