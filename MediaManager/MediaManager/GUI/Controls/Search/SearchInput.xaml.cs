using MediaManager.Globals.LanguageProvider;
using System.Windows;
using System.Windows.Controls;

namespace MediaManager.GUI.Controls.Search
{
    /// <summary>
    /// Interaction logic for SearchInput.xaml
    /// </summary>
    public partial class SearchInput : UserControl, UpdatedLanguageUser
    {
        public delegate void SearchParametersChangeHandler(SearchParameters parameters);
        public event SearchParametersChangeHandler SearchParametersChanged;
        public SearchParameters CurrentSearchParameters
        {
            get => new SearchParameters
            {
                SearchString = searchText.Text.Trim(),
                ExactMode = exactMode.IsChecked.HasValue && exactMode.IsChecked.Value,
                SearchWithinDescriptions = searchDescriptionMode.IsChecked.HasValue && searchDescriptionMode.IsChecked.Value,
                OnlySearchWithinFavourites = favouriteOnlyMode.IsChecked.HasValue && favouriteOnlyMode.IsChecked.Value,
                SearchResult = CurrentSearchResultMode,
                SearchTags = new System.Collections.Generic.List<ValuedTag>(tagList.Tags)
            };
        }

        private SearchResultMode CurrentSearchResultMode = SearchResultMode.MediaList;

        public SearchInput()
        {
            InitializeComponent();
        }

        private void searchText_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter) SearchParametersChanged?.Invoke(CurrentSearchParameters);
        }
        private void startSearch_Click(object sender, RoutedEventArgs e) => SearchParametersChanged?.Invoke(CurrentSearchParameters);
        private void modeChanged(object sender, RoutedEventArgs e) => SearchParametersChanged?.Invoke(CurrentSearchParameters);
        private void resultListMode_Click(object sender, RoutedEventArgs e)
        {
            CurrentSearchResultMode = CurrentSearchResultMode == SearchResultMode.MediaList ? SearchResultMode.PartList : SearchResultMode.MediaList;
            resultListMode.Content = CurrentSearchResultMode == SearchResultMode.MediaList ? LanguageProvider.getString("Controls.Search.ShowMedia") : LanguageProvider.getString("Controls.Search.ShowParts");
            SearchParametersChanged?.Invoke(CurrentSearchParameters);
        }
        private void tagList_TagValueChanged(System.Collections.Generic.List<ValuedTag> tags) => SearchParametersChanged?.Invoke(CurrentSearchParameters);

        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            exactMode.Content = LanguageProvider.getString("Controls.Search.ExactMode");
            searchDescriptionMode.Content = LanguageProvider.getString("Controls.Search.SearchDescriptionMode");
            favouriteOnlyMode.Content = LanguageProvider.getString("Controls.Search.FavouriteOnlyMode");
            resultListMode.Content = CurrentSearchResultMode == SearchResultMode.MediaList ? LanguageProvider.getString("Controls.Search.ShowMedia") : LanguageProvider.getString("Controls.Search.ShowParts");
            startSearch.Content = LanguageProvider.getString("Controls.Search.Search");
        }
    }
}
