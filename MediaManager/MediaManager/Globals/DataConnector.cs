using MediaManager.GUI.Controls.Search;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaManager.Globals
{
    public static class DataConnector
    {
        private static MediaDBEntities DBCONNECTION = new MediaDBEntities();

        /// <summary>
        /// The currently active <see cref="Catalogue"/>.
        /// 
        /// Can be cleared by setting the value to <c>null</c>.
        /// </summary>
        public static Catalogue CURRENT_CATALOGUE
        {
            get
            {
                var id = DBCONNECTION.Settings.FirstOrDefault(s => s.Key == "CURRENT_CATALOGUE_ID")?.Value;
                if (id == null || int.Parse(id) == -1) return null;
                return DBCONNECTION.Catalogues.Find(int.Parse(id));
            }
            set
            {
                var newId = (value == null ? -1 : value.Id).ToString();
                var originalSetting = DBCONNECTION.Settings.FirstOrDefault(s => s.Key == "CURRENT_CATALOGUE_ID");
                if (originalSetting != null) originalSetting.Value = newId;
                else DBCONNECTION.Settings.Add(new Setting { Key = "CURRENT_CATALOGUE_ID", Value = newId });
                DBCONNECTION.SaveChanges();
            }
        }
        // TODO put all requests that are based on a catalogue inside common static class

        public static class Reader
        {
            public static bool AnyCatalogExists() => DBCONNECTION.Catalogues.Count() > 0;
            public static Catalogue GetCatalog(int id) => DBCONNECTION.Catalogues.Find(id);
            public static Medium GetMedium(int id) => DBCONNECTION.Media.Find(id);
            public static Tag GetTag(int id) => DBCONNECTION.Tags.Find(id);
            public static List<ValuedTag> GetTagsForMedium(int id)
            {
                var result = new List<ValuedTag>();
                var mediaTags = GetMedium(id).MT_Relation;
                foreach (var t in Tags)
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
            /// <summary>
            /// Catalogs ordered by title.
            /// </summary>
            public static List<Catalogue> Catalogs { get => DBCONNECTION.Catalogues.OrderBy(c => c.Title).ToList() ?? new List<Catalogue>(); }
            /// <summary>
            /// Media ordered by title.
            /// </summary>
            public static List<Medium> Media { get => CURRENT_CATALOGUE?.Media.OrderBy(m => m.Title).ToList() ?? new List<Medium>(); }
            public static int CountOfMedia { get => CURRENT_CATALOGUE?.Media.Count() ?? 0; }
            public static Part GetPart(int id) => DBCONNECTION.Parts.Find(id);
            public static List<ValuedTag> GetTagsForPart(int id)
            {
                var result = new List<ValuedTag>();
                var partTags = GetPart(id).PT_Relation;
                foreach (var t in Tags)
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
            public static List<Part> GetPartsForTag(int id) => DBCONNECTION?.Tags.Find(id).PT_Relation.Select(r => r.Part).ToList() ?? new List<Part>();
            public static int CountOfParts { get => CURRENT_CATALOGUE?.Media.Select(m => m.Parts.Count).Sum() ?? 0; }
            public static List<Playlist> Playlists { get => CURRENT_CATALOGUE?.Playlists.ToList() ?? new List<Playlist>(); }
            public static List<Tag> Tags { get => CURRENT_CATALOGUE?.Tags.ToList() ?? new List<Tag>(); }

            public static List<SearchResultItem> SearchUsingParameters(SearchParameters parameters)
            {
                var result = new List<SearchResultItem>();

                var fittingSearchString = DBCONNECTION.Parts.Where(p => p.Title.Contains(parameters.SearchString) || p.Medium.Title.Contains(parameters.SearchString) || (parameters.SearchWithinDescriptions && (p.Description.Contains(parameters.SearchString) || p.Medium.Description.Contains(parameters.SearchString))));

                var positiveTags = parameters.SearchTags.Where(t => t.Value.HasValue && t.Value.Value).Select(t => t.Tag.Id).ToList();
                var negativeTags = parameters.SearchTags.Where(t => t.Value.HasValue && !t.Value.Value).Select(t => t.Tag.Id).ToList();
                var fittingTags = parameters.ExactMode
                    ? fittingSearchString.Where(p => positiveTags.All(t => p.PT_Relation.Any(r => r.TagId == t && r.Value)) && negativeTags.All(t => p.PT_Relation.Any(r => r.TagId == t && !r.Value)))
                    : fittingSearchString.Where(p => (positiveTags.Count == 0 || !positiveTags.Any(t => p.PT_Relation.Any(r => r.TagId == t && !r.Value))) && (negativeTags.Count == 0 || !negativeTags.Any(t => p.PT_Relation.Any(r => r.TagId == t && r.Value))));

                var fittingFavourites = parameters.OnlySearchWithinFavourites
                    ? fittingTags.Where(p => p.Favourite)
                    : fittingTags;

                if (parameters.SearchResult == SearchResultMode.MediaList)
                {
                    foreach (var item in fittingFavourites.Select(p => new SearchResultItem
                    {
                        Id = p.Medium.Id,
                        Text = p.Medium.Title,
                    }).Distinct().OrderBy(m => m.Text)) result.Add(item);
                }
                else
                {
                    foreach (var item in fittingFavourites.Select(p => new SearchResultItem
                    {
                        Id = p.Id,
                        Text = p.Title + " (" + p.Medium.Title + ")"
                    }).OrderBy(p => p.Text)) result.Add(item);
                }

                return result;
            }

            public static class Settings
            {
                private static T GetSettingsValue<T>(string settingsName, Func<string, T> parser, T defaultValue) where T : struct
                {
                    var val = DBCONNECTION.Settings.FirstOrDefault(s => s.Key == settingsName)?.Value;
                    return val != null ? parser(val) : defaultValue;
                }
                public static int ResultListLength { get => GetSettingsValue("RESULT_LIST_LENGTH", int.Parse, 20); }
                public static bool PlaylistEditorVisible { get => GetSettingsValue("VISIBILITY_PLAYLIST_EDITOR", bool.Parse, true); }
                public static bool TitleOfTheDayVisible { get => GetSettingsValue("VISIBILITY_TITLE_OF_THE_DAY", bool.Parse, true); }
                public static bool StatisticsOverviewVisible { get => GetSettingsValue("VISIBILITY_STATISTICS_OVERVIEW", bool.Parse, true); }
            }
        }
        public static class Writer
        {
            public static void DeleteCatalog(int id)
            {
                DBCONNECTION.Catalogues.Remove(DBCONNECTION.Catalogues.Find(id));
                DBCONNECTION.SaveChanges();
            }
            public static void DeleteTag(int id)
            {
                DBCONNECTION.Tags.Remove(DBCONNECTION.Tags.Find(id));
                DBCONNECTION.SaveChanges();
            }
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
            public static void CleanupEmptyMediaAndParts()
            {
                DBCONNECTION.Parts.RemoveRange(DBCONNECTION.Parts.Where(p => p.Title.Length == 0));
                DBCONNECTION.Media.RemoveRange(DBCONNECTION.Media.Where(m => m.Title.Length == 0));
                DBCONNECTION.SaveChanges();
            }
            public static void DeletePlaylist(int id)
            {
                DBCONNECTION.Playlists.Remove(DBCONNECTION.Playlists.Find(id));
                DBCONNECTION.SaveChanges();
            }

            public static void CreateCatalog(Catalogue catalog)
            {
                DBCONNECTION.Catalogues.Add(catalog);
                DBCONNECTION.SaveChanges();
            }
            public static void CreateTag(Tag tag)
            {
                DBCONNECTION.Tags.Add(tag);
                DBCONNECTION.SaveChanges();
            }
            public static int CreateMedium()
            {
                CURRENT_CATALOGUE.Media.Add(new Medium { Title = "", Location = "", Description = "" });
                DBCONNECTION.SaveChanges();
                return CURRENT_CATALOGUE.Media.OrderBy(m => m.Id).LastOrDefault()?.Id ?? 0;
            }
            public static int CreatePart(int mediumId)
            {
                DBCONNECTION.Parts.Add(new Part { MediumId = mediumId, Title = "", Favourite = false, Description = "", Length = 0, Publication_Year = 0 });
                DBCONNECTION.SaveChanges();
                // set media tags for part
                var newId = DBCONNECTION.Parts.ToList().OrderBy(p => p.Id).LastOrDefault()?.Id ?? 0;
                var mediaTags = Reader.GetTagsForMedium(mediumId);
                var part = Reader.GetPart(newId);
                SavePart(part, mediaTags);
                return newId;
            }

            public static void SaveCatalog(Catalogue catalog)
            {
                var original = DBCONNECTION.Catalogues.Find(catalog.Id);
                original.Title = catalog.Title;
                original.Description = catalog.Description;
                original.DeletionConfirmationMedium = catalog.DeletionConfirmationMedium;
                original.DeletionConfirmationPart = catalog.DeletionConfirmationPart;
                original.DeletionConfirmationPlaylist = catalog.DeletionConfirmationPlaylist;
                original.DeletionConfirmationTag = catalog.DeletionConfirmationTag;
                original.ShowTitleOfTheDayAsMedium = catalog.ShowTitleOfTheDayAsMedium;
                DBCONNECTION.SaveChanges();
            }
            public static void SaveTag(Tag tag)
            {
                var original = DBCONNECTION.Tags.Find(tag.Id);
                original.Title = tag.Title;
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
                // overwrite relevant part tags
                var mediumTags = Reader.GetTagsForMedium(medium.Id).Where(t => t.Value.HasValue).ToList();
                var mediumTagsIds = mediumTags.Select(t => t.Tag.Id).ToList();
                Reader.GetMedium(medium.Id).Parts.ToList().ForEach(p =>
                {
                    var partTags = Reader.GetTagsForPart(p.Id).Where(t => !mediumTagsIds.Contains(t.Tag.Id)).ToList();
                    partTags.AddRange(mediumTags);
                    SavePart(p, partTags);
                });
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
            public static int CreatePlaylist(string title)
            {
                DBCONNECTION.Playlists.Add(new Playlist
                {
                    CatalogueId = CURRENT_CATALOGUE.Id,
                    Title = title
                });
                DBCONNECTION.SaveChanges();
                return DBCONNECTION.Playlists.ToList().Last().Id;
            }
            public static void AddPartToPlaylist(int playlistId, int partId)
            {
                DBCONNECTION.Playlists.Find(playlistId).Parts.Add(DBCONNECTION.Parts.Find(partId));
                DBCONNECTION.SaveChanges();
            }
            public static void RemovePartFromPlaylist(int playlistId, int partId)
            {
                DBCONNECTION.Playlists.Find(playlistId).Parts.Remove(DBCONNECTION.Parts.Find(partId));
                DBCONNECTION.SaveChanges();
            }
            public static class Settings
            {
                private static void SaveSetting(string settingsName, string value)
                {
                    var original = DBCONNECTION.Settings.FirstOrDefault(s => s.Key == settingsName);
                    if (original != null) original.Value = value;
                    else DBCONNECTION.Settings.Add(new Setting { Key = settingsName, Value = value });
                    DBCONNECTION.SaveChanges();
                }
                public static int ResultListLength { set => SaveSetting("RESULT_LIST_LENGTH", value.ToString()); }
                public static bool PlaylistEditorVisible { set => SaveSetting("VISIBILITY_PLAYLIST_EDITOR", value.ToString()); }
                public static bool TitleOfTheDayVisible { set => SaveSetting("VISIBILITY_TITLE_OF_THE_DAY", value.ToString()); }
                public static bool StatisticsOverviewVisible { set => SaveSetting("VISIBILITY_STATISTICS_OVERVIEW", value.ToString()); }
            }
        }
    }
}
