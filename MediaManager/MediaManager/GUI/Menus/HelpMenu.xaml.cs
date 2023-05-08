using MediaManager.Globals.LanguageProvider;
using MediaManager.GUI.HelpTopics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for HelpMenu.xaml
    /// </summary>
    public partial class HelpMenu : Window, UpdatedLanguageUser
    {
        private HelpTopic selectedTopic;

        #region Setup
        public HelpMenu()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }
        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            setupTopics();
        }
        private void setupTopics()
        {
            topics.ItemsSource = translateTopics(Topics.HelpTopics);
        }
        private List<HelpTopic> translateTopics(List<HelpTopic> topicList) => topicList.Select(t => new HelpTopic
        {
            Caption = LanguageProvider.getString("Help." + t.Caption),
            Pages = t.Pages?.Select(p => new HelpTopicPage
            {
                Caption = LanguageProvider.getString("Help." + p.Caption),
                Image = p.Image,
                Content = LanguageProvider.getString("Help." + p.Content),
            }).ToList(),
            Children = t.Children != null ? translateTopics(t.Children) : null
        }).ToList();
        #endregion

        #region Handler
        private void topics_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            selectedTopic = topics.SelectedItem as HelpTopic;
            pager.CurrentPage = 1;
            pager.TotalPages = 1;
        }
        private void pager_PageChanged(int newPage)
        {
            contentCaption.Text = selectedTopic?.Pages?[newPage - 1]?.Caption;
            contentImage.Source = selectedTopic?.Pages?[newPage - 1]?.Image != null ? getImageSourceFromBitmap(selectedTopic?.Pages?[newPage - 1]?.Image) : null;
            contentText.Text = selectedTopic?.Pages?[newPage - 1]?.Content;
        }
        private ImageSource getImageSourceFromBitmap(Bitmap bmp) => Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        #endregion
    }
}
