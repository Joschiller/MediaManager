using MediaManager.Globals.LanguageProvider;
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
        private List<AnalyzeListElement> allItems;
        private int itemsPerPage = Reader.Settings.ResultListLength;

        #region Setup
        public AnalyzeMenu()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
            reload();
        }
        private void reload() => mode_ModeChanged(mode.Mode);

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            modeGroup.Header = LanguageProvider.getString("Menus.Analyze.Mode");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e) => OpenHelpMenu();
        #endregion

        private void mode_ModeChanged(Controls.Analyze.AnalyzeMode mode)
        {
            allItems = Reader.LoadAnalyzeResult(mode);
            pager.CurrentPage = 1;
            pager.TotalPages = allItems.Count / itemsPerPage + (allItems.Count % itemsPerPage == 0 ? 0 : 1);
        }
        private void pager_PageChanged(int newPage) => list.SetItems(allItems.Skip((newPage - 1) * itemsPerPage).Take(itemsPerPage).ToList());
        private void list_SelectionChanged(AnalyzeListElement element) => preview.LoadPreview(mode.Mode, element);
        private void preview_StartEditing(AnalyzeMode mode, AnalyzeListElement element)
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
                    OpenWindow(this, new EditMenu(Reader.GetPart(element.Id).MediumId, element.Id, true), () =>
                    {
                        Show();
                        reload();
                    });
                    break;
            }
        }
    }
}
