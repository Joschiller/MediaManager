using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for HelpMenu.xaml
    /// </summary>
    public partial class HelpMenu : Window, UpdatedLanguageUser
    {
        public HelpMenu()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            throw new NotImplementedException();
        }
    }
}
