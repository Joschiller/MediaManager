using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
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
            for (int i = 0; i < ItemsPerPage; i++)
            {
                if ((newPage - 1) * ItemsPerPage + i >= CompleteResultList.Count) break;
                SearchResult.Add(CompleteResultList[(newPage - 1) * ItemsPerPage + i]);
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: double click does not work properly
            if (e.ClickCount == 2)
            {
                var id = (int)((TextBlock)sender).Tag;
                if (CurrentSearchParameters.SearchResult == SearchResultMode.MediaList) MediumSelected?.Invoke(id, null);
                else MediumSelected?.Invoke(GetPart(id).MediumId, id);
            }
        }

        public void ReloadResultList() => input_SearchParametersChanged(CurrentSearchParameters);
    }
}
