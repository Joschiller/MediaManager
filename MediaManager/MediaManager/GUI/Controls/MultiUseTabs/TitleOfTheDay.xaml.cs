using LanguageProvider;
using static LanguageProvider.LanguageProvider;
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
        #region Setup
        public TitleOfTheDay()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
            ShowNextRandomItem();
        }
        public void RegisterAtLanguageProvider() => Register(this);
        public void LoadTexts(string language)
        {
            next.Tooltip = getString("Controls.MultiUseTabs.TitleOfTheDay.Next");
            ShowCurrentItem();
        }
        ~TitleOfTheDay() => Unregister(this);
        public ImageSource GetHeader() => new BitmapImage(new Uri("/Resources/title_of_the_day.png", UriKind.Relative));
        public bool GetIsVisible() => GlobalContext.Settings.TitleOfTheDayVisible;
        public void ReloadGUI() => ShowNextRandomItem();
        #endregion

        #region Handler
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
                title.Text = GlobalContext.Reader.GetCatalog(CatalogContext.CurrentCatalogId.Value).ShowTitleOfTheDayAsMedium ? CurrentItem.Medium.Title : CurrentItem.Title;
                description.Text = GlobalContext.Reader.GetCatalog(CatalogContext.CurrentCatalogId.Value).ShowTitleOfTheDayAsMedium
                    ? (getString("Controls.MultiUseTabs.TitleOfTheDay.Tags") + ": " + getTagsString(GlobalContext.Reader.GetTagsForMedium(CurrentItem.MediumId)) + "\n" + "\n"
                    + CurrentItem.Medium.Description)
                    : (getString("Controls.MultiUseTabs.TitleOfTheDay.Medium") + ": " + CurrentItem.Medium.Title + "\n" + "\n"
                    + getString("Controls.MultiUseTabs.TitleOfTheDay.Tags") + ": " + getTagsString(GlobalContext.Reader.GetTagsForPart(CurrentItem.Id)) + "\n" + "\n"
                    + CurrentItem.Description);
            }
            else
            {
                title.Text = "";
                description.Text = getString("Controls.MultiUseTabs.TitleOfTheDay.Empty");
            }
        }
        private void ShowNextRandomItem()
        {
            var possibleMedia = CatalogContext.Reader.Lists.NonEmptyMedia;
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
        #endregion
    }
}
