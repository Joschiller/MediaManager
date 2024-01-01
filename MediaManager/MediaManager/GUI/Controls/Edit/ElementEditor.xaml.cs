using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static MediaManager.Globals.ImageUtils;

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
                textLength.Visibility = Visibility.Collapsed;
                dataLength.Visibility = Visibility.Collapsed;
                textPublication.Visibility = Visibility.Collapsed;
                publication.Visibility = Visibility.Collapsed;
                imageButtons.Visibility = Visibility.Collapsed;
                imageViewer.Visibility = Visibility.Collapsed;

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
                textLength.Visibility = Visibility.Visible;
                dataLength.Visibility = Visibility.Visible;
                textPublication.Visibility = Visibility.Visible;
                publication.Visibility = Visibility.Visible;
                imageButtons.Visibility = Visibility.Visible;
                imageViewer.Visibility = Visibility.Visible;

                title.Text = part.Title;
                IsCurrentlyFavorite = part.Favourite;
                updateFavoriteButtonVisibility();
                description.Text = part.Description;
                length.SetValue(part.Length);
                textMinute.Text = getString(part.Length == 1 ? "Controls.Edit.Minute" : "Controls.Edit.Minutes");
                publication.SetValue(part.Publication_Year);
                image.Source = part.Image != null ? convertByteArrayToBitmapImage(part.Image) : null;
                imagePath.Text = "";
                imagePath.Visibility = Visibility.Collapsed;
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
        public void RegisterAtLanguageProvider() => Register(this);
        public void LoadTexts(string language)
        {
            textTitle.Text = getString("Controls.Edit.Label.Title") + ":";
            textDescription.Text = getString("Controls.Edit.Label.Description") + ":";
            textTags.Text = getString("Controls.Edit.Label.Tags") + ":";
            selectImage.Content = getString("Controls.Edit.Button.SelectImage");
            removeImage.Content = getString("Controls.Edit.Button.RemoveImage");
            textLocation.Text = getString("Controls.Edit.Label.Location") + ":";
            textLength.Text = getString("Controls.Edit.Label.Length") + ":";
            textPublication.Text = getString("Controls.Edit.Label.Publication") + ":";
        }
        ~ElementEditor() => Unregister(this);
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
        private void numericValueChanged(int newVal)
        {
            textMinute.Text = getString(length.Value == 1 ? "Controls.Edit.Minute" : "Controls.Edit.Minutes");
            onEdited();
        }
        private void tags_TagValueChanged(List<ValuedTag> tags) => onEdited();
        private void selectImage_Click(object sender, RoutedEventArgs e) => SelectImage();
        private void removeImage_Click(object sender, RoutedEventArgs e) => SelectImage();
        #endregion

        #region Functions
        public void ToggleFavourite()
        {
            if (part == null) return;

            IsCurrentlyFavorite = !IsCurrentlyFavorite;
            updateFavoriteButtonVisibility();
            onEdited();
        }
        private string showLoadImageDialog()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = getString("Common.ImageFileType") + " (*.png)|*.png";
            ofd.ShowDialog();
            return ofd.FileName;
        }
        public void SelectImage()
        {
            if (part == null) return;

            var fileName = showLoadImageDialog();
            if (fileName == null || fileName == "") return;

            part.Image = convertImageToByteArray(fileName);
            imagePath.Text = fileName;
            imagePath.Visibility = Visibility.Visible;
            image.Source = convertByteArrayToBitmapImage(part.Image);

            onEdited();
        }
        public void RemoveImage()
        {
            if (part == null) return;

            part.Image = null;
            imagePath.Text = "";
            imagePath.Visibility = Visibility.Collapsed;
            image.Source = null;
            onEdited();
        }
        #endregion
    }
}
