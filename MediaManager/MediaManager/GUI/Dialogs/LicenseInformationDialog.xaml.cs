using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using System.Windows;
using System;
using System.IO;

namespace MediaManager.GUI.Dialogs
{
    /// <summary>
    /// Interaction logic for LicenseInformationDialog.xaml
    /// </summary>
    public partial class LicenseInformationDialog : Window, LanguageUser
    {
        #region Setup
        public LicenseInformationDialog()
        {
            InitializeComponent();
            LoadTexts(null);
        }

        public void LoadTexts(string language)
        {
            license.Source = new Uri(string.Format("file:///{0}/Resources/{1}", Directory.GetCurrentDirectory(), getString("Menus.Help.LicenseFile"))); // first: start loading content
            loading.Text = getString("Menus.Help.Loading");
            close.Content = "_" + getString("Common.Button.Close");
        }
        #endregion

        #region Handler
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();
        private void license_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            license.Visibility = Visibility.Visible;
            loading.Visibility = Visibility.Collapsed;
        }
        private void close_Click(object sender, RoutedEventArgs e) => DialogResult = false;
        #endregion
    }
}
