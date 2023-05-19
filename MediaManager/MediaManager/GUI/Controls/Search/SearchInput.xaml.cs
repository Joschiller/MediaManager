using MediaManager.Globals.LanguageProvider;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaManager.GUI.Controls.Search
{
    /// <summary>
    /// Interaction logic for SearchInput.xaml
    /// </summary>
    public partial class SearchInput : UserControl, UpdatedLanguageUser
    {
        #region Events
        public delegate void SearchParametersChangeHandler(SearchParameters parameters);
        public event SearchParametersChangeHandler SearchParametersChanged;
        #endregion

        #region Properties
        public SearchParameters CurrentSearchParameters
        {
            get => new SearchParameters
            {
                SearchString = searchText.Text.Trim(),
                ExactMode = exactMode.IsChecked.HasValue && exactMode.IsChecked.Value,
                SearchWithinDescriptions = searchDescriptionMode.IsChecked.HasValue && searchDescriptionMode.IsChecked.Value,
                OnlySearchWithinFavourites = favouriteOnlyMode.IsChecked.HasValue && favouriteOnlyMode.IsChecked.Value,
                SearchResult = CurrentSearchResultMode,
                SearchTags = new System.Collections.Generic.List<ValuedTag>(tagList.GetTagList())
            };
        }
        #endregion

        #region Setup
        private SearchResultMode CurrentSearchResultMode = SearchResultMode.MediaList;
        public SearchInput()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }
        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            exactMode.Content = LanguageProvider.getString("Controls.Search.ExactMode");
            searchDescriptionMode.Content = LanguageProvider.getString("Controls.Search.SearchDescriptionMode");
            favouriteOnlyMode.Content = LanguageProvider.getString("Controls.Search.FavouriteOnlyMode");
            resultListMode.Content = CurrentSearchResultMode == SearchResultMode.MediaList ? LanguageProvider.getString("Controls.Search.ShowParts") : LanguageProvider.getString("Controls.Search.ShowMedia");
            startSearch.Content = LanguageProvider.getString("Controls.Search.Search");
            reset.Tooltip = LanguageProvider.getString("Controls.Search.Reset");
        }
        ~SearchInput() => LanguageProvider.Unregister(this);
        public void reloadTagList()
        {
            var tags = new List<ValuedTag>();
            var currentFilter = tagList.GetTagList();
            Globals.DataConnector.CatalogContext.Reader.Lists.Tags.ForEach(t => tags.Add(new ValuedTag
            {
                Tag = t,
                Value = currentFilter.FirstOrDefault(f => f.Tag.Id == t.Id)?.Value ?? null
            }));
            tagList.SetTagList(tags);
        }
        #endregion

        #region Handler
        private void searchText_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter) SearchParametersChanged?.Invoke(CurrentSearchParameters);
        }
        private void startSearch_Click(object sender, RoutedEventArgs e) => SearchParametersChanged?.Invoke(CurrentSearchParameters);
        private void modeChanged(object sender, RoutedEventArgs e) => SearchParametersChanged?.Invoke(CurrentSearchParameters);
        private void resultListMode_Click(object sender, RoutedEventArgs e)
        {
            CurrentSearchResultMode = CurrentSearchResultMode == SearchResultMode.MediaList ? SearchResultMode.PartList : SearchResultMode.MediaList;
            resultListMode.Content = CurrentSearchResultMode == SearchResultMode.MediaList ? LanguageProvider.getString("Controls.Search.ShowParts") : LanguageProvider.getString("Controls.Search.ShowMedia");
            SearchParametersChanged?.Invoke(CurrentSearchParameters);
        }
        private void tagList_TagValueChanged(System.Collections.Generic.List<ValuedTag> tags) => SearchParametersChanged?.Invoke(CurrentSearchParameters);
        private void reset_Click(object sender, RoutedEventArgs e)
        {
            searchText.Text = "";
            exactMode.IsChecked = false;
            searchDescriptionMode.IsChecked = false;
            favouriteOnlyMode.IsChecked = false;
            CurrentSearchResultMode = SearchResultMode.MediaList;
            resultListMode.Content = LanguageProvider.getString("Controls.Search.ShowParts");
            var tags = new List<ValuedTag>();
            Globals.DataConnector.CatalogContext.Reader.Lists.Tags.ForEach(t => tags.Add(new ValuedTag
            {
                Tag = t,
                Value = null
            }));
            tagList.SetTagList(tags);
            SearchParametersChanged?.Invoke(CurrentSearchParameters);
        }
        #endregion
    }
}
