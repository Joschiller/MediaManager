using MediaManager.GUI.Controls.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaManager.Globals
{
    public static class DataConnector
    {
        private static MediaDBEntities DBCONNECTION = new MediaDBEntities();

        public static class Reader
        {
            public static Part GetPart(int id) => DBCONNECTION.Parts.Find(id);

            public static List<SearchResultItem> SearchUsingParameters(SearchParameters parameters)
            {
                var result = new List<SearchResultItem>();

                var fittingTitle = DBCONNECTION.Parts.Where(p => p.Title.Contains(parameters.SearchString) || p.Medium.Title.Contains(parameters.SearchString));
                var fittingSearchString = parameters.SearchWithinDescriptions
                    ? fittingTitle.Union(DBCONNECTION.Parts.Where(p => p.Description.Contains(parameters.SearchString) || p.Medium.Description.Contains(parameters.SearchString)))
                    : fittingTitle;

                var fittingTags = parameters.ExactMode
                    ? fittingSearchString.Where(p => parameters.SearchTags.All(t => !t.Value.HasValue || p.PT_Relation.Any(r => r.TagId == t.Tag.Id && r.Value == t.Value.Value)))
                    : fittingSearchString.Where(p => !parameters.SearchTags.Any(t => p.PT_Relation.Any(r => t.Value.HasValue && r.TagId == t.Tag.Id && r.Value == !t.Value.Value)));

                var fittingFavourites = parameters.OnlySearchWithinFavourites
                    ? fittingTags.Where(p => p.Favourite)
                    : fittingTags;

                if (parameters.SearchResult == SearchResultMode.MediaList)
                {
                    foreach(var item in fittingFavourites.Select(p => p.Medium).Distinct().OrderBy(m => m.Title))
                    {
                        result.Add(new SearchResultItem
                        {
                            Id = item.Id,
                            Text = item.Title
                        });
                    }
                }
                else
                {
                    foreach (var item in fittingFavourites)
                    {
                        result.Add(new SearchResultItem
                        {
                            Id = item.Id,
                            Text = item.Title + " (" + item.Medium.Title + ")"
                        });
                    }
                }

                return result;
            }
        }
    }
}
