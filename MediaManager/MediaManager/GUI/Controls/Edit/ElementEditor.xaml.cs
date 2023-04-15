using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static MediaManager.Globals.DataConnector.Reader;
using static MediaManager.Globals.DataConnector.Writer;

namespace MediaManager.GUI.Controls.Edit
{
    /// <summary>
    /// Interaction logic for ElementEditor.xaml
    /// </summary>
    public partial class ElementEditor : UserControl, UpdatedLanguageUser
    {
        public event ElementEventHandler QuitEditing;

        private ElementMode Mode;
        private int CurrentId;
        private bool IsCurrentlyFavorite;

        public ElementEditor()
        {
            InitializeComponent();
            length.SetMin(0);
            publication.SetMin(0);
            RegisterAtLanguageProvider();
        }

        public void LoadElement(ElementMode mode, int id)
        {
            Mode = mode;
            CurrentId = id;
            if (Mode == ElementMode.Medium)
            {
                var media = GetMedium(id);

                favoriteButtonEnabled.Visibility = Visibility.Collapsed;
                favoriteButtonDisabled.Visibility = Visibility.Collapsed;
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

                favoriteButtonEnabled.Visibility = Visibility.Collapsed;
                favoriteButtonDisabled.Visibility = Visibility.Collapsed;
                location.Visibility = Visibility.Collapsed;
                integerMeta.Visibility = Visibility.Visible;
                image.Visibility = Visibility.Visible;

                title.Text = part.Title;
                IsCurrentlyFavorite = part.Favourite;
                updateFavoriteButtonVisibility();
                description.Text = part.Description;
                length.SetValue((uint)part.Length);
                textMinute.Text = LanguageProvider.getString(part.Length == 1 ? "Controls.Edit.Minute" : "Controls.Edit.Minutes");
                publication.SetValue((uint)part.Length);
                // TODO image
                tags.SetTagList(GetTagsForPart(id));
                // TODO disable tags that are set by parent medium
            }
        }

        private void favoriteEnableButton_Click(object sender, RoutedEventArgs e)
        {
            IsCurrentlyFavorite = true;
            saveButton.Enabled = true;
            updateFavoriteButtonVisibility();
        }
        private void favoriteDisableButton_Click(object sender, RoutedEventArgs e)
        {
            IsCurrentlyFavorite = false;
            saveButton.Enabled = true;
            updateFavoriteButtonVisibility();
        }
        private void updateFavoriteButtonVisibility()
        {
            favoriteButtonEnabled.Visibility = IsCurrentlyFavorite ? Visibility.Visible : Visibility.Collapsed;
            favoriteButtonDisabled.Visibility = IsCurrentlyFavorite ? Visibility.Collapsed : Visibility.Visible;
        }
        private void textChanged(object sender, TextChangedEventArgs e) => saveButton.Enabled = true;
        private void numericValueChanged(uint newVal) => saveButton.Enabled = true;
        private void selectImage_Click(object sender, RoutedEventArgs e) => saveButton.Enabled = true;
        private void removeImage_Click(object sender, RoutedEventArgs e) => saveButton.Enabled = true;

        public void saveChanges()
        {
            // TODO: add validation => Title cannot be empty
            if (Mode == ElementMode.Medium) SaveMedium(new Medium
            {
                Id = CurrentId,
                Title = title.Text.Trim(),
                Location = location.Text.Trim(),
                Description = description.Text.Trim(),
            }, new System.Collections.Generic.List<ValuedTag>(tags.Tags));
            else SavePart(new Part
            {
                Id = CurrentId,
                Title = title.Text.Trim(),
                Favourite = IsCurrentlyFavorite,
                Description = description.Text.Trim(),
                Length = (int)length.Value,
                Publication_Year = (int)publication.Value,
                // TODO image
            }, new System.Collections.Generic.List<ValuedTag>(tags.Tags));
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            saveChanges();
            QuitEditing?.Invoke(Mode, CurrentId);
        }
        private void discardButton_Click(object sender, RoutedEventArgs e) => QuitEditing?.Invoke(Mode, CurrentId);

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            textLength.Text = LanguageProvider.getString("Controls.Edit.Length");
            textPublication.Text = LanguageProvider.getString("Controls.Edit.Publication");
            saveButton.ToolTip = LanguageProvider.getString("Controls.Edit.Save");
            discardButton.ToolTip = LanguageProvider.getString("Controls.Edit.Discard");
        }
    }
}
