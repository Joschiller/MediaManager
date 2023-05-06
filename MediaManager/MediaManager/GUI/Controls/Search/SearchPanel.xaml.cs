using MediaManager.Globals.LanguageProvider;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Controls.Search
{
    /// <summary>
    /// Interaction logic for SearchPanel.xaml
    /// </summary>
    public partial class SearchPanel : UserControl, UpdatedLanguageUser
    {
        #region Events
        public delegate void MediumSelectionHandler(int mediumId, int? partId);
        public event MediumSelectionHandler MediumSelected;
        public delegate void PlaylistAdditionHandler(int id, SearchResultMode mode);
        public event PlaylistAdditionHandler PlaylistAdditionRequested;
        #endregion

        #region Bindings
        public ObservableCollection<SearchResultItem> SearchResult { get; set; } = new ObservableCollection<SearchResultItem>();
        public int ItemsPerPage { get; set; } = GlobalContext.Settings.ResultListLength;
        #endregion

        #region Setup
        private SearchParameters CurrentSearchParameters;
        private List<SearchResultItem> CompleteResultList = new List<SearchResultItem>();
        public SearchPanel()
        {
            InitializeComponent();
            DataContext = this;
            RegisterAtLanguageProvider();
            CurrentSearchParameters = input.CurrentSearchParameters;
        }
        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            Resources["contextMenuAddToPlaylist"] = LanguageProvider.getString(CurrentSearchParameters?.SearchResult == SearchResultMode.PartList ? "Controls.Search.AddPartToPlaylist" : "Controls.Search.AddMediumToPlaylist");
        }
        ~SearchPanel() => LanguageProvider.Unregister(this);
        public void ReloadTags() => input.reloadTagList();
        public void ReloadResultList()
        {
            ItemsPerPage = GlobalContext.Settings.ResultListLength;
            input_SearchParametersChanged(CurrentSearchParameters);
        }
        #endregion

        #region Handler
        private void input_SearchParametersChanged(SearchParameters parameters)
        {
            CurrentSearchParameters = parameters;
            CompleteResultList = CatalogContext.Reader.SearchUsingParameters(parameters);
            pager.CurrentPage = 1;
            pager.TotalPages = CompleteResultList.Count / ItemsPerPage + (CompleteResultList.Count % ItemsPerPage == 0 ? 0 : 1);
            LoadTexts(null);
        }
        private void pager_PageChanged(int newPage)
        {
            SearchResult.Clear();
            CompleteResultList.Skip((newPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList().ForEach(SearchResult.Add);
        }

        private void resultList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as SearchResultItem;
            if (item != null)
            {
                if (CurrentSearchParameters.SearchResult == SearchResultMode.MediaList) MediumSelected?.Invoke(item.Id, null);
                else MediumSelected?.Invoke(GlobalContext.Reader.GetPart(item.Id).MediumId, item.Id);
            }
        }
        private int? rightClickPivotId = null;
        private void resultList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = resultList.SelectedItem as SearchResultItem;
            if (item != null)
            {
                rightClickPivotId = item.Id;
                ContextMenu cm = FindResource("contextMenu") as ContextMenu;
                cm.PlacementTarget = (FrameworkElement)e.OriginalSource;
                cm.IsOpen = true;
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (rightClickPivotId.HasValue) PlaylistAdditionRequested?.Invoke(rightClickPivotId.Value, CurrentSearchParameters.SearchResult);
        }
        #endregion
    }
}
