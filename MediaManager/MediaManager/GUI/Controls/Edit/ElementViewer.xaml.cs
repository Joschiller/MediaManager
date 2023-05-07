using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static MediaManager.Globals.ImageUtils;

namespace MediaManager.GUI.Controls.Edit
{
    /// <summary>
    /// Interaction logic for ElementViewer.xaml
    /// </summary>
    public partial class ElementViewer : UserControl, UpdatedLanguageUser
    {
        #region Properties
        private EditableMedium medium;
        public EditableMedium Medium
        {
            get => medium;
            set
            {
                medium = value;
                part = null;

                favoriteIcon.Visibility = Visibility.Collapsed;
                textLocation.Visibility = Visibility.Visible;
                location.Visibility = Visibility.Visible;
                integerMeta.Visibility = Visibility.Collapsed;
                image.Visibility = Visibility.Collapsed;

                title.Text = medium.Title;
                description.Text = medium.Description;
                location.Text = medium.Location;
                tags.SetTagList(medium.Tags);
            }
        }
        private EditablePart part;
        public EditablePart Part
        {
            get => part;
            set
            {
                medium = null;
                part = value;

                favoriteIcon.Visibility = Visibility.Visible;
                textLocation.Visibility = Visibility.Collapsed;
                location.Visibility = Visibility.Collapsed;
                integerMeta.Visibility = Visibility.Visible;
                image.Visibility = Visibility.Visible;

                title.Text = part.Title;
                favoriteIcon.Source = new BitmapImage(new Uri(part.Favourite ? "/Resources/favorite.png" : "/Resources/nofavorite.png", UriKind.Relative));
                description.Text = part.Description;
                length.Text = part.Length.ToString();
                textMinute.Text = LanguageProvider.getString(part.Length == 1 ? "Controls.Edit.Minute" : "Controls.Edit.Minutes");
                textLength.Visibility = part.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
                length.Visibility = part.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
                textMinute.Visibility = part.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
                publication.Text = part.Publication_Year.ToString();
                textPublication.Visibility = part.Publication_Year > 0 ? Visibility.Visible : Visibility.Collapsed;
                publication.Visibility = part.Publication_Year > 0 ? Visibility.Visible : Visibility.Collapsed;
                image.Source = part.Image != null ? convertByteArrayToBitmapImage(part.Image) : null;
                tags.SetTagList(part.Tags);
            }
        }
        #endregion

        #region Setup
        public ElementViewer()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }
        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            textLocation.Text = LanguageProvider.getString("Controls.Edit.Label.Location") + ":";
            textLength.Text = LanguageProvider.getString("Controls.Edit.Label.Length") + ":";
            textPublication.Text = LanguageProvider.getString("Controls.Edit.Label.Publication") + ":";
        }
        ~ElementViewer() => LanguageProvider.Unregister(this);
        #endregion
    }
}
