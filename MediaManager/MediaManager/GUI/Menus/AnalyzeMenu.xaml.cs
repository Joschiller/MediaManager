using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using MediaManager.GUI.Controls.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.KeyboardShortcutHelper;
using static MediaManager.Globals.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for AnalyzeMenu.xaml
    /// </summary>
    public partial class AnalyzeMenu : Window, UpdatedLanguageUser
    {
        #region Bindings
        public ObservableCollection<AnalyzeListElement> Items { get; set; } = new ObservableCollection<AnalyzeListElement>();
        #endregion

        #region Setup
        private List<AnalyzeListElement> allItems;
        private int itemsPerPage = GlobalContext.Settings.ResultListLength;
        public AnalyzeMenu()
        {
            InitializeComponent();
            DataContext = this;
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
        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) => runKeyboardShortcut(e, new System.Collections.Generic.Dictionary<(ModifierKeys Modifiers, Key Key), Action>
        {
            [(ModifierKeys.None, Key.F1)] = OpenHelpMenu,
            [(ModifierKeys.None, Key.Escape)] = Close,
            [(ModifierKeys.Control, Key.Up)] = mode.ModeUp,
            [(ModifierKeys.Control, Key.Down)] = mode.ModeDown,
            [(ModifierKeys.Control, Key.E)] = preview.TriggerStartEditing,
        });
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
        private void pager_PageChanged(int newPage)
        {
            Items.Clear();
            allItems.Skip((newPage - 1) * itemsPerPage).Take(itemsPerPage).ToList().ForEach(Items.Add);
            if (Items.Count > 0) list.SelectedIndex = 0;
        }
        private void list_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right && pager.CurrentPage < pager.TotalPages)
            {
                pager.CurrentPage++;
                e.Handled = true;
            }
            if (e.Key == Key.Left && pager.CurrentPage > 1)
            {
                pager.CurrentPage--;
                e.Handled = true;
            }
        }
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e) => preview.LoadPreview(mode.Mode, ((ListView)sender).SelectedItem as AnalyzeListElement);
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
