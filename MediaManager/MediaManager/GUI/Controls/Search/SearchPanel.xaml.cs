using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.DataConnector.Reader;

namespace MediaManager.GUI.Controls.Search
{
    /// <summary>
    /// Interaction logic for SearchPanel.xaml
    /// </summary>
    public partial class SearchPanel : UserControl
    {
        public delegate void MediumSelectionHandler(int mediumId, int? partId);
        public event MediumSelectionHandler MediumSelected;

        private SearchParameters CurrentSearchParameters;
        private List<SearchResultItem> CompleteResultList = new List<SearchResultItem>();
        public ObservableCollection<SearchResultItem> SearchResult { get; set; } = new ObservableCollection<SearchResultItem>();

        public int ItemsPerPage { get; set; } = Reader.Settings.ResultListLength;

        public SearchPanel()
        {
            InitializeComponent();
            DataContext = this;
            CurrentSearchParameters = input.CurrentSearchParameters;
        }

        private void input_SearchParametersChanged(SearchParameters parameters)
        {
            CurrentSearchParameters = parameters;
            CompleteResultList = SearchUsingParameters(parameters);
            pager.CurrentPage = 1;
            pager.TotalPages = CompleteResultList.Count / ItemsPerPage + (CompleteResultList.Count % ItemsPerPage == 0 ? 0 : 1);
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
                else MediumSelected?.Invoke(GetPart(item.Id).MediumId, item.Id);
            }
        }

        public void ReloadTags() => input.reloadTagList();
        public void ReloadResultList()
        {
            ItemsPerPage = Reader.Settings.ResultListLength;
            input_SearchParametersChanged(CurrentSearchParameters);
        }
    }
}
