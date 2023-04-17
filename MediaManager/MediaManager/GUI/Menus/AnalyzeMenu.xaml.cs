using MediaManager.Globals.LanguageProvider;
using MediaManager.GUI.Controls.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for AnalyzeMenu.xaml
    /// </summary>
    public partial class AnalyzeMenu : Window, UpdatedLanguageUser
    {
        private List<AnalyzeListElement> allItems;
        private int itemsPerPage = Reader.Settings.ResultListLength;

        #region Setup
        public AnalyzeMenu()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
            mode_ModeChanged(mode.Mode);
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            modeGroup.Header = LanguageProvider.getString("Menus.Analyze.Mode");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void mode_ModeChanged(Controls.Analyze.AnalyzeMode mode)
        {
            allItems = Reader.LoadAnalyzeResult(mode);
            pager.CurrentPage = 1;
            pager.TotalPages = allItems.Count / itemsPerPage + (allItems.Count % itemsPerPage == 0 ? 0 : 1);
        }
        private void pager_PageChanged(int newPage) => list.setItems(allItems.Skip((newPage - 1) * itemsPerPage).Take(itemsPerPage).ToList());
        private void list_SelectionChanged(AnalyzeListElement element)
        {
            //throw new NotImplementedException();
        }
    }
}
