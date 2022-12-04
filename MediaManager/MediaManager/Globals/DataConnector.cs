using MediaManager.GUI.Controls.Search;
using System.Collections.Generic;
using System.Linq;

namespace MediaManager.Globals
{
    public static class DataConnector
    {
        private static MediaDBEntities DBCONNECTION = new MediaDBEntities();

        public static Catalogue CURRENT_CATALOGUE
        {
            get
            {
                var id = DBCONNECTION.Settings.Where(s => s.Key == "CURRENT_CATALOGUE_ID").FirstOrDefault()?.Value;
                if (id == null || int.Parse(id) == -1) return null;
                return DBCONNECTION.Catalogues.Find(int.Parse(id));
            }
        }
        // TODO put all requests that are based on a catalogue inside common static class

        public static class Reader
        {
            public static Medium GetMedium(int id) => DBCONNECTION.Media.Find(id);
            public static List<ValuedTag> GetTagsForMedium(int id)
            {
                var result = new List<ValuedTag>();
                var mediaTags = GetMedium(id).MT_Relation;
                foreach (var t in CURRENT_CATALOGUE?.Tags ?? new List<Tag>())
                {
                    var val = mediaTags.FirstOrDefault(v => v.TagId == t.Id);
                    result.Add(new ValuedTag
                    {
                        Tag = t,
                        Value = val != null ? val.Value : (bool?)null
                    });
                }
                return result;
            }
            public static Part GetPart(int id) => DBCONNECTION.Parts.Find(id);
            public static List<ValuedTag> GetTagsForPart(int id)
            {
                var result = new List<ValuedTag>();
                var partTags = GetPart(id).PT_Relation;
                foreach (var t in CURRENT_CATALOGUE?.Tags ?? new List<Tag>())
                {
                    var val = partTags.FirstOrDefault(v => v.TagId == t.Id);
                    result.Add(new ValuedTag
                    {
                        Tag = t,
                        Value = val != null ? val.Value : (bool?)null
                    });
                }
                return result;
            }

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

            public static class Settings
            {
                public static int ResultListLength
                {
                    get
                    {
                        var val = DBCONNECTION.Settings.FirstOrDefault(s => s.Key == "RESULT_LIST_LENGTH")?.Value;
                        return val != null ? int.Parse(val) : 20;
                    }
                }
            }
        }
        public static class Writer
        {
            public static void DeleteMedium(int id)
            {
                DBCONNECTION.Media.Remove(DBCONNECTION.Media.Find(id));
                DBCONNECTION.SaveChanges();
            }
            public static void DeletePart(int id)
            {
                DBCONNECTION.Parts.Remove(DBCONNECTION.Parts.Find(id));
                DBCONNECTION.SaveChanges();
            }

            public static void SaveMedium(Medium medium, List<ValuedTag> tags)
            {
                var original = DBCONNECTION.Media.Find(medium.Id);
                original.Title = medium.Title;
                original.Location = medium.Location;
                original.Description = medium.Description;
                DBCONNECTION.MT_Relation.RemoveRange(DBCONNECTION.MT_Relation.Where(r => r.MediaId == medium.Id));
                foreach (var t in tags)
                {
                    if (!t.Value.HasValue) continue;
                    DBCONNECTION.MT_Relation.Add(new MT_Relation
                    {
                        MediaId = medium.Id,
                        TagId = t.Tag.Id,
                        Value = t.Value.Value
                    });
                }
                DBCONNECTION.SaveChanges();
            }
            public static void SavePart(Part part, List<ValuedTag> tags)
            {
                var original = DBCONNECTION.Parts.Find(part.Id);
                original.Title = part.Title;
                original.Favourite = part.Favourite;
                original.Description = part.Description;
                original.Length = part.Length;
                original.Publication_Year = part.Publication_Year;
                DBCONNECTION.PT_Relation.RemoveRange(DBCONNECTION.PT_Relation.Where(r => r.PartId == part.Id));
                foreach (var t in tags)
                {
                    if (!t.Value.HasValue) continue;
                    DBCONNECTION.PT_Relation.Add(new PT_Relation
                    {
                        PartId = part.Id,
                        TagId = t.Tag.Id,
                        Value = t.Value.Value
                    });
                }
                DBCONNECTION.SaveChanges();
            }
        }
    }
}
