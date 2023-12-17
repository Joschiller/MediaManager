using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using MediaManager.GUI.Controls.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for AnalyzeMenu.xaml
    /// </summary>
    public partial class AnalyzeMenu : Window, UpdatedLanguageUser
    {
        #region Setup
        private List<AnalyzeListElement> allItems;
        private int itemsPerPage = GlobalContext.Settings.ResultListLength;
        public AnalyzeMenu()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
            reload();
        }
        public void RegisterAtLanguageProvider() => RegisterUnique(this);
        public void LoadTexts(string language)
        {
            modeGroup.Header = getString("Menus.Analyze.Mode");
        }
        private void reload() => mode_ModeChanged(mode.Mode);
        #endregion

        #region Handler
        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e) => OpenHelpMenu();
        #endregion
        private void mode_ModeChanged(Controls.Analyze.AnalyzeMode mode)
        {
            // NOTE: not using SearchRunner here because it leads to issues with closed database connections if the analyze mode is changed quickly
            allItems = CatalogContext.Reader.Analysis.LoadAnalyzeResult(mode);
            pager.CurrentPage = 1;
            pager.TotalPages = allItems.Count / itemsPerPage + (allItems.Count % itemsPerPage == 0 ? 0 : 1);
            pager.setItemCount(allItems.Count);
        }
        private void pager_PageChanged(int newPage) => list.SetItems(allItems.Skip((newPage - 1) * itemsPerPage).Take(itemsPerPage).ToList());
        private void list_SelectionChanged(AnalyzeListElement element) => preview.LoadPreview(mode.Mode, element);
        private void preview_StartEditing(AnalyzeMode mode, AnalyzeListElement element) => StartEditing(mode, element);
        #endregion

        #region Functions
        private void StartEditing(AnalyzeMode mode, AnalyzeListElement element)
        {
            switch (mode)
            {
                case AnalyzeMode.MediumDoubled:
                    OpenWindow(this, new MergeMenu(element.Text), () =>
                    {
                        Show();
                        reload();
                    });
                    break;
                case AnalyzeMode.MediumEmpty:
                case AnalyzeMode.MediumCommonTags:
                case AnalyzeMode.MediumDescription:
                case AnalyzeMode.MediumTags:
                case AnalyzeMode.MediumLocation:
                    OpenWindow(this, new EditMenu(element.Id, null, true), () =>
                    {
                        Show();
                        reload();
                    });
                    break;
                case AnalyzeMode.PartDescription:
                case AnalyzeMode.PartTags:
                case AnalyzeMode.PartLength:
                case AnalyzeMode.PartPublication:
                case AnalyzeMode.PartImage:
                    OpenWindow(this, new EditMenu(GlobalContext.Reader.GetPart(element.Id).MediumId, element.Id, true), () =>
                    {
                        Show();
                        reload();
                    });
                    break;
            }
        }
        #endregion
    }
}
