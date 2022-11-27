namespace MediaManager.GUI.Controls.Search
{
    public class SearchParameters
    {
        public string SearchString { get; set; }
        public bool ExactMode { get; set; }
        public bool SearchWithinDescriptions { get; set; }
        public bool OnlySearchWithinFavourites { get; set; }
        public SearchResultMode SearchResult { get; set; }
    }
}
