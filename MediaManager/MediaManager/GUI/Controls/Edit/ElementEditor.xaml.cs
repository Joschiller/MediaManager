using MediaManager.Globals.LanguageProvider;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MediaManager.GUI.Controls.Edit
{
    /// <summary>
    /// Interaction logic for ElementEditor.xaml
    /// </summary>
    public partial class ElementEditor : UserControl, UpdatedLanguageUser
    {
        #region Events
        public delegate void MediumHandler(EditableMedium medium);
        public event MediumHandler MediumEdited;
        public delegate void PartHandler(EditablePart part);
        public event PartHandler PartEdited;
        #endregion

        #region Properties
        private EditableMedium medium;
        public EditableMedium Medium
        {
            get => new EditableMedium
            {
                Id = medium.Id,
                CatalogId = medium.CatalogId,
                Title = title.Text.Trim(),
                Description = description.Text.Trim(),
                Location = location.Text.Trim(),
                Tags = new List<ValuedTag>(tags.GetTagList())
            };
            set
            {
                skipOnChange = true;
                medium = value;
                part = null;

                favoriteButtonEnabled.Visibility = Visibility.Collapsed;
                favoriteButtonDisabled.Visibility = Visibility.Collapsed;
                textLocation.Visibility = Visibility.Visible;
                location.Visibility = Visibility.Visible;
                integerMeta.Visibility = Visibility.Collapsed;
                image.Visibility = Visibility.Collapsed;

                title.Text = medium.Title;
                location.Text = medium.Location;
                description.Text = medium.Description;
                tags.SetTagList(medium.Tags);
                skipOnChange = false;
            }
        }
        private EditablePart part;
        public EditablePart Part
        {
            get => new EditablePart
            {
                Id = part.Id,
                MediumId = part.MediumId,
                Title = title.Text.Trim(),
                Favourite = IsCurrentlyFavorite,
                Description = description.Text.Trim(),
                Length = (int)length.Value,
                Publication_Year = (int)publication.Value,
                Image = part.Image,
                Tags = new List<ValuedTag>(tags.GetTagList()),
                TagsBlockedByMedium = part.TagsBlockedByMedium
            };
            set
            {
                skipOnChange = true;
                medium = null;
                part = value;

                favoriteButtonEnabled.Visibility = Visibility.Collapsed;
                favoriteButtonDisabled.Visibility = Visibility.Collapsed;
                textLocation.Visibility = Visibility.Collapsed;
                location.Visibility = Visibility.Collapsed;
                integerMeta.Visibility = Visibility.Visible;
                image.Visibility = Visibility.Visible;

                title.Text = part.Title;
                IsCurrentlyFavorite = part.Favourite;
                updateFavoriteButtonVisibility();
                description.Text = part.Description;
                length.SetValue((uint)part.Length);
                textMinute.Text = LanguageProvider.getString(part.Length == 1 ? "Controls.Edit.Minute" : "Controls.Edit.Minutes");
                publication.SetValue((uint)part.Publication_Year);
                // TODO image
                tags.SetTagList(part.Tags, part.TagsBlockedByMedium);
                skipOnChange = false;
            }
        }
        #endregion

        #region Setup
        private bool skipOnChange = false;
        private bool IsCurrentlyFavorite;
        public ElementEditor()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
            length.SetMin(0);
            publication.SetMin(0);
        }
        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            textTitle.Text = LanguageProvider.getString("Controls.Edit.Label.Title") + ":";
            textDescription.Text = LanguageProvider.getString("Controls.Edit.Label.Description") + ":";
            textTags.Text = LanguageProvider.getString("Controls.Edit.Label.Tags") + ":";
            textLocation.Text = LanguageProvider.getString("Controls.Edit.Label.Location") + ":";
            textLength.Text = LanguageProvider.getString("Controls.Edit.Label.Length") + ":";
            textPublication.Text = LanguageProvider.getString("Controls.Edit.Label.Publication") + ":";
        }
        ~ElementEditor() => LanguageProvider.Unregister(this);
        #endregion

        #region Handler
        private void onEdited()
        {
            if (skipOnChange) return;
            if (medium != null) MediumEdited?.Invoke(Medium);
            if (part != null) PartEdited?.Invoke(Part);
        }
        private void updateFavoriteButtonVisibility()
        {
            favoriteButtonEnabled.Visibility = IsCurrentlyFavorite ? Visibility.Visible : Visibility.Collapsed;
            favoriteButtonDisabled.Visibility = IsCurrentlyFavorite ? Visibility.Collapsed : Visibility.Visible;
        }

        private void favoriteEnableButton_Click(object sender, RoutedEventArgs e)
        {
            IsCurrentlyFavorite = true;
            updateFavoriteButtonVisibility();
            onEdited();
        }
        private void favoriteDisableButton_Click(object sender, RoutedEventArgs e)
        {
            IsCurrentlyFavorite = false;
            updateFavoriteButtonVisibility();
            onEdited();
        }
        private void textChanged(object sender, TextChangedEventArgs e) => onEdited();
        private void numericValueChanged(uint newVal)
        {
            textMinute.Text = LanguageProvider.getString(length.Value == 1 ? "Controls.Edit.Minute" : "Controls.Edit.Minutes");
            onEdited();
        }
        private void tags_TagValueChanged(List<ValuedTag> tags) => onEdited();
        private void selectImage_Click(object sender, RoutedEventArgs e) => onEdited(); // TODO: dialog => set image of part and imagePath
        private void removeImage_Click(object sender, RoutedEventArgs e)
        {
            if (part != null)
            {
                part.Image = null;
                imagePath.Text = "";
            }
            onEdited();
        }
        #endregion
    }
}
