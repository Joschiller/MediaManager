using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static MediaManager.Globals.DataConnector.Reader;
using static MediaManager.Globals.DataConnector.Writer;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Controls.Edit
{
    /// <summary>
    /// Interaction logic for ElementViewer.xaml
    /// </summary>
    public partial class ElementViewer : UserControl, UpdatedLanguageUser
    {
        public event ElementEventHandler EditClicked;
        public event ElementEventHandler DeleteClicked;

        private ElementMode Mode;
        private int CurrentId;

        public ElementViewer()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

        public void LoadElement(ElementMode mode, int id)
        {
            Mode = mode;
            CurrentId = id;
            if (Mode == ElementMode.Medium)
            {
                var media = GetMedium(id);

                favoriteIcon.Visibility = Visibility.Collapsed;
                location.Visibility = Visibility.Visible;
                integerMeta.Visibility = Visibility.Collapsed;
                image.Visibility = Visibility.Collapsed;

                title.Text = media.Title;
                location.Text = media.Location;
                description.Text = media.Description;
                tags.SetTagList(GetTagsForMedium(id));
            }
            else
            {
                var part = GetPart(id);

                favoriteIcon.Visibility = Visibility.Visible;
                location.Visibility = Visibility.Collapsed;
                integerMeta.Visibility = Visibility.Visible;
                image.Visibility = Visibility.Visible;

                title.Text = part.Title;
                favoriteIcon.Source = new BitmapImage(new Uri(part.Favourite ? "/Resources/favorite.png" : "/Resources/nofavorite.png", UriKind.Relative)) ;
                description.Text = part.Description;
                length.Text = part.Length.ToString();
                textMinute.Text = LanguageProvider.getString(part.Length == 1 ? "Controls.Edit.Minute" : "Controls.Edit.Minutes");
                publication.Text = part.Length.ToString();
                // TODO image
                tags.SetTagList(GetTagsForPart(id));
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e) => EditClicked?.Invoke(Mode, CurrentId);
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString(Mode == ElementMode.Medium ? "Controls.Edit.MediaDeletion" : "Controls.Edit.PartDeletion"));
            if (confirmation.HasValue && confirmation.Value)
            {
                if (Mode == ElementMode.Medium) DeleteMedium(CurrentId);
                else DeletePart(CurrentId);
                DeleteClicked?.Invoke(Mode, CurrentId);
            }
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            textLength.Text = LanguageProvider.getString("Controls.Edit.Length");
            textPublication.Text = LanguageProvider.getString("Controls.Edit.Publication");
            editButton.ToolTip = LanguageProvider.getString("Controls.Edit.Edit");
            deleteButton.ToolTip = LanguageProvider.getString("Controls.Edit.Delete");
        }
    }
}
