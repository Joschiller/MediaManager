using MediaManager.Globals.LanguageProvider;
using MediaManager.GUI.Dialogs;
using System;
using System.Windows;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for TagMenu.xaml
    /// </summary>
    public partial class TagMenu : Window, UpdatedLanguageUser
    {
        #region Setup
        public TagMenu()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddTag"] = LanguageProvider.getString("Menus.Overview.Tooltip.AddTag");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnAddTagClick(object sender, RoutedEventArgs e) => new EditTagDialog(null).ShowDialog();
        #endregion
    }
}
