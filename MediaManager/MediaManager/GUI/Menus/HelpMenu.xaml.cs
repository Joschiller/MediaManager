using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using MediaManager.GUI.HelpTopics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MediaManager.GUI.Dialogs;

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
            contentImageContainer.Visibility = Visibility.Collapsed;
        }
        public void RegisterAtLanguageProvider() => RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnLicenseTooltip"] = getString("Menus.Help.ToolTip.License");
            Resources["btnLicenseText"] = "_" + getString("Menus.Help.Button.License");
            setupTopics();
        }
        private void setupTopics()
        {
            topics.ItemsSource = translateTopics(Topics.HelpTopics);
        }
        private List<HelpTopic> translateTopics(List<HelpTopic> topicList) => topicList.Select(t => new HelpTopic
        {
            TreeCaption = getString("Help." + t.TreeCaption),
            Pages = t.Pages?.Select(p => new HelpTopicPage
            {
                PageCaption = getString("Help." + p.PageCaption),
                Image = p.Image,
                Content = getString("Help." + p.Content),
            }).ToList(),
            Children = t.Children != null ? translateTopics(t.Children) : null
        }).ToList();
        #endregion

        #region Handler
        private void topics_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            selectedTopic = topics.SelectedItem as HelpTopic;
            pager.CurrentPage = 1;
            pager.TotalPages = selectedTopic.Pages?.Count ?? 0;
        }
        private void pager_PageChanged(int newPage)
        {
            contentCaption.Text = selectedTopic?.Pages?[newPage - 1]?.PageCaption;
            var image = selectedTopic?.Pages?[newPage - 1]?.Image != null ? getImageSourceFromBitmap(selectedTopic?.Pages?[newPage - 1]?.Image) : null;
            contentImageContainer.Visibility = image != null ? Visibility.Visible : Visibility.Collapsed;
            contentImage.Source = image;
            contentText.Text = selectedTopic?.Pages?[newPage - 1]?.Content;
        }
        private void topics_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Right && pager.CurrentPage < pager.TotalPages)
            {
                pager.CurrentPage++;
                e.Handled = true;
            }
            if (e.Key == System.Windows.Input.Key.Left && pager.CurrentPage > 1)
            {
                pager.CurrentPage--;
                e.Handled = true;
            }
        }
        private ImageSource getImageSourceFromBitmap(Bitmap bmp) => Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

        private void license_Click(object sender, RoutedEventArgs e) => new LicenseInformationDialog().ShowDialog();
        #endregion
    }
}
