﻿using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MediaManager.GUI.Controls.Edit
{
    /// <summary>
    /// Interaction logic for ElementViewer.xaml
    /// </summary>
    public partial class ElementViewer : UserControl, UpdatedLanguageUser
    {
        public event ElementEventHandler EditClicked;
        public event ElementEventHandler DeleteClicked;

        private MediumWithTags medium;
        private PartWithTags part;

        public ElementViewer()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

        public MediumWithTags Medium
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
        public PartWithTags Part
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
                // TODO image
                tags.SetTagList(part.Tags);
            }
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            textLocation.Text = LanguageProvider.getString("Controls.Edit.Label.Location") + ":";
            textLength.Text = LanguageProvider.getString("Controls.Edit.Label.Length") + ":";
            textPublication.Text = LanguageProvider.getString("Controls.Edit.Label.Publication") + ":";
        }
    }
}
