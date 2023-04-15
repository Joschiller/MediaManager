using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static MediaManager.Globals.DataConnector;
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
                var media = Reader.GetMedium(id);

                favoriteIcon.Visibility = Visibility.Collapsed;
                textLocation.Visibility = Visibility.Visible;
                location.Visibility = Visibility.Visible;
                integerMeta.Visibility = Visibility.Collapsed;
                image.Visibility = Visibility.Collapsed;

                title.Text = media.Title;
                location.Text = media.Location;
                description.Text = media.Description;
                tags.SetTagList(Reader.GetTagsForMedium(id));
            }
            else
            {
                var part = Reader.GetPart(id);

                favoriteIcon.Visibility = Visibility.Visible;
                textLocation.Visibility = Visibility.Collapsed;
                location.Visibility = Visibility.Collapsed;
                integerMeta.Visibility = Visibility.Visible;
                image.Visibility = Visibility.Visible;

                title.Text = part.Title;
                favoriteIcon.Source = new BitmapImage(new Uri(part.Favourite ? "/Resources/favorite.png" : "/Resources/nofavorite.png", UriKind.Relative)) ;
                description.Text = part.Description;
                length.Text = part.Length.ToString();
                textMinute.Text = LanguageProvider.getString(part.Length == 1 ? "Controls.Edit.Minute" : "Controls.Edit.Minutes");
                textLength.Visibility = part.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
                length.Visibility = part.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
                textMinute.Visibility = part.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
                publication.Text = part.Publication_Year.ToString();
                textPublication.Visibility = part.Publication_Year > 0 ? Visibility.Visible : Visibility.Collapsed;
                publication.Visibility = part.Publication_Year > 0 ? Visibility.Visible : Visibility.Collapsed;
                // TODO image
                tags.SetTagList(Reader.GetTagsForPart(id));
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e) => EditClicked?.Invoke(Mode, CurrentId);
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Mode == ElementMode.Medium)
            {
                var performDeletion = !CURRENT_CATALOGUE.DeletionConfirmationMedium;
                if (!performDeletion)
                {
                    var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString("Controls.Edit.MediaDeletion"));
                    performDeletion = confirmation.HasValue && confirmation.Value;
                }
                if (performDeletion)
                {
                    Writer.DeleteMedium(CurrentId);
                    DeleteClicked?.Invoke(Mode, CurrentId);
                }
            }
            else
            {
                var performDeletion = !CURRENT_CATALOGUE.DeletionConfirmationPart;
                if (!performDeletion)
                {
                    var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString("Controls.Edit.PartDeletion"));
                    performDeletion = confirmation.HasValue && confirmation.Value;
                }
                if (performDeletion)
                {
                    Writer.DeletePart(CurrentId);
                    DeleteClicked?.Invoke(Mode, CurrentId);
                }
            }
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            textLocation.Text = LanguageProvider.getString("Controls.Edit.Label.Location") + ":";
            textLength.Text = LanguageProvider.getString("Controls.Edit.Label.Length") + ":";
            textPublication.Text = LanguageProvider.getString("Controls.Edit.Label.Publication") + ":";
            editButton.ToolTip = LanguageProvider.getString("Controls.Edit.Button.Edit");
            deleteButton.ToolTip = LanguageProvider.getString("Controls.Edit.Button.Delete");
        }
    }
}
