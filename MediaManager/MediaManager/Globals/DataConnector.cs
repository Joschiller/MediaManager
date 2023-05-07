using MediaManager.Globals.XMLImportExport;
using MediaManager.GUI.Controls.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MediaManager.Globals
{
    public static class DataConnector
    {
        private static MediaDBEntities DBCONNECTION = new MediaDBEntities();

        /// <summary>All methods that are independent from the currently active <see cref="Catalog"/>.</summary>
        public static class GlobalContext
        {
            /// <summary>Offers reading access to data independent from the currently active <see cref="Catalog"/>.</summary>
            public static class Reader
            {
                /// <summary>True, if any <see cref="Catalog"/> exists</summary>
                public static bool AnyCatalogExists { get => DBCONNECTION.Catalogs.Count() > 0; }
                /// <summary><see cref="Catalog"/>s ordered by title.</summary>
                public static List<Catalog> Catalogs { get => DBCONNECTION.Catalogs.OrderBy(c => c.Title).ToList() ?? new List<Catalog>(); }
                /// <summary>
                /// Retrieves a <see cref="Catalog"/> by its id.
                /// </summary>
                /// <param name="id">Id of the <see cref="Catalog"/> to retrieve</param>
                /// <returns><see cref="Catalog"/></returns>
                public static Catalog GetCatalog(int id) => DBCONNECTION.Catalogs.Find(id);
                /// <summary>
                /// Retrieves a <see cref="Medium"/> by its id.
                /// </summary>
                /// <param name="id">Id of the <see cref="Medium"/> to retrieve</param>
                /// <returns><see cref="Medium"/></returns>
                public static Medium GetMedium(int id) => DBCONNECTION.Media.Find(id);
                /// <summary>
                /// Retrieves a <see cref="Part"/> by its id.
                /// </summary>
                /// <param name="id">Id of the <see cref="Part"/> to retrieve</param>
                /// <returns><see cref="Part"/></returns>
                public static Part GetPart(int id) => DBCONNECTION.Parts.Find(id);
                /// <summary>
                /// Retrieves a <see cref="Tag"/> by its id.
                /// </summary>
                /// <param name="id">Id of the <see cref="Tag"/> to retrieve</param>
                /// <returns><see cref="Tag"/></returns>
                public static Tag GetTag(int id) => DBCONNECTION.Tags.Find(id);

                /// <summary>
                /// Returns values for all <see cref="Tag"/>s of the <see cref="Catalog"/> of the <see cref="Medium"/> ordered by title. This includes the <see cref="Tag"/>s, that have a set value for the <see cref="Medium"/> and all neutral tags too.
                /// </summary>
                /// <param name="id">Id of the <see cref="Medium"/> to retrieve the <see cref="Tag"/>s for</param>
                /// <returns>List of <see cref="Tag"/>s of the <see cref="Medium"/></returns>
                public static List<ValuedTag> GetTagsForMedium(int id)
                {
                    var result = new List<ValuedTag>();
                    var medium = GetMedium(id);
                    var mediaTags = medium.MT_Relation;
                    foreach (var t in medium.Catalog.Tags.OrderBy(t => t.Title))
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
                /// Returns values for all <see cref="Tag"/>s of the <see cref="Catalog"/> of the <see cref="Part"/> ordered by title. This includes the <see cref="Tag"/>s, that have a set value for the <see cref="Part"/> and all neutral tags too.
                /// </summary>
                /// <param name="id">Id of the <see cref="Part"/> to retrieve the <see cref="Tag"/>s for</param>
                /// <returns>List of <see cref="Tag"/>s of the <see cref="Part"/></returns>
                public static List<ValuedTag> GetTagsForPart(int id)
                {
                    var result = new List<ValuedTag>();
                    var part = GetPart(id);
                    var partTags = part.PT_Relation;
                    foreach (var t in part.Medium.Catalog.Tags.OrderBy(t => t.Title))
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
                /// <summary>
                /// Retrieves all <see cref="Parts"/> that have a positive value for a given <see cref="Tag"/>.
                /// </summary>
                /// <param name="id">Id of the <see cref="Tag"/> to retrieve the parts for</param>
                /// <returns>List of <see cref="Part"/>s</returns>
                public static List<Part> GetPartsForTag(int id) => GetTag(id).PT_Relation.Where(r => r.Value).Select(r => r.Part).ToList() ?? new List<Part>();
            }
            /// <summary>Offers writing access to data independent from the currently active <see cref="Catalog"/>.</summary>
            public static class Writer
            {
                /// <summary>
                /// Deletes a <see cref="Catalog"/> by its id.
                /// </summary>
                /// <param name="id">Id of the <see cref="Catalog"/> to delete</param>
                public static void DeleteCatalog(int id)
                {
                    DBCONNECTION.Media.RemoveRange(DBCONNECTION.Catalogs.Find(id).Media); // must be deleted explicitly
                    DBCONNECTION.Catalogs.Remove(DBCONNECTION.Catalogs.Find(id));
                    DBCONNECTION.SaveChanges();
                }
                /// <summary>
                /// Deletes a <see cref="Medium"/> by its id.
                /// </summary>
                /// <param name="id">Id of the <see cref="Medium"/> to delete</param>
                public static void DeleteMedium(int id)
                {
                    DBCONNECTION.Media.Remove(DBCONNECTION.Media.Find(id));
                    DBCONNECTION.SaveChanges();
                }
                /// <summary>
                /// Deletes a <see cref="Part"/> by its id.
                /// </summary>
                /// <param name="id">Id of the <see cref="Part"/> to delete</param>
                public static void DeletePart(int id)
                {
                    DBCONNECTION.Parts.Remove(DBCONNECTION.Parts.Find(id));
                    DBCONNECTION.SaveChanges();
                }
                /// <summary>
                /// Deletes a <see cref="Tag"/> by its id.
                /// </summary>
                /// <param name="id">Id of the <see cref="Tag"/> to delete</param>
                public static void DeleteTag(int id)
                {
                    DBCONNECTION.Tags.Remove(DBCONNECTION.Tags.Find(id));
                    DBCONNECTION.SaveChanges();
                }
                /// <summary>
                /// Deletes a <see cref="Playlist"/> by its id.
                /// </summary>
                /// <param name="id">Id of the <see cref="Playlist"/> to delete</param>
                public static void DeletePlaylist(int id)
                {
                    DBCONNECTION.Playlists.Remove(DBCONNECTION.Playlists.Find(id));
                    DBCONNECTION.SaveChanges();
                }

                /// <summary>
                /// Stores a new <see cref="Catalog"/> to the database.
                /// </summary>
                /// <param name="catalog"><see cref="Catalog"/> to store into the database</param>
                public static void CreateCatalog(Catalog catalog)
                {
                    DBCONNECTION.Catalogs.Add(catalog);
                    DBCONNECTION.SaveChanges();
                }
                /// <summary>
                /// Saves changes to an existing <see cref="Catalog"/>.
                /// </summary>
                /// <param name="catalog"><see cref="Catalog"/> to store into the database</param>
                public static void SaveCatalog(Catalog catalog)
                {
                    var original = DBCONNECTION.Catalogs.Find(catalog.Id);
                    original.Title = catalog.Title;
                    original.Description = catalog.Description;
                    original.DeletionConfirmationMedium = catalog.DeletionConfirmationMedium;
                    original.DeletionConfirmationPart = catalog.DeletionConfirmationPart;
                    original.DeletionConfirmationPlaylist = catalog.DeletionConfirmationPlaylist;
                    original.DeletionConfirmationTag = catalog.DeletionConfirmationTag;
                    original.ShowTitleOfTheDayAsMedium = catalog.ShowTitleOfTheDayAsMedium;
                    DBCONNECTION.SaveChanges();
                }

                /// <summary>
                /// Adds a <see cref="Part"/> to a <see cref="Playlist"/>.
                /// </summary>
                /// <param name="playlistId">Id of the <see cref="Playlist"/></param>
                /// <param name="partId">Id of the <see cref="Part"/></param>
                public static void AddPartToPlaylist(int playlistId, int partId)
                {
                    DBCONNECTION.Playlists.Find(playlistId).Parts.Add(DBCONNECTION.Parts.Find(partId));
                    DBCONNECTION.SaveChanges();
                }
                /// <summary>
                /// Removes a <see cref="Part"/> from a <see cref="Playlist"/>.
                /// </summary>
                /// <param name="playlistId">Id of the <see cref="Playlist"/></param>
                /// <param name="partId">Id of the <see cref="Part"/></param>
                public static void RemovePartFromPlaylist(int playlistId, int partId)
                {
                    DBCONNECTION.Playlists.Find(playlistId).Parts.Remove(DBCONNECTION.Parts.Find(partId));
                    DBCONNECTION.SaveChanges();
                }
            }
            /// <summary>Offers reading and writing access to settings independent from the currently active <see cref="Catalog"/>.</summary>
            public static class Settings
            {
                private static T GetSettingsValue<T>(string settingsName, Func<string, T> parser, T defaultValue) where T : struct
                {
                    var val = DBCONNECTION.Settings.FirstOrDefault(s => s.Key == settingsName)?.Value;
                    return val != null ? parser(val) : defaultValue;
                }
                private static void SaveSetting(string settingsName, string value)
                {
                    var original = DBCONNECTION.Settings.FirstOrDefault(s => s.Key == settingsName);
                    if (original != null) original.Value = value;
                    else DBCONNECTION.Settings.Add(new Setting { Key = settingsName, Value = value });
                    DBCONNECTION.SaveChanges();
                }

                /// <summary>Length of the pages of any result list.</summary>
                public static int ResultListLength
                {
                    get => GetSettingsValue("RESULT_LIST_LENGTH", int.Parse, 20);
                    set => SaveSetting("RESULT_LIST_LENGTH", value.ToString());
                }
                /// <summary>True, if the playlist editor should be shown.</summary>
                public static bool PlaylistEditorVisible
                {
                    get => GetSettingsValue("VISIBILITY_PLAYLIST_EDITOR", bool.Parse, true);
                    set => SaveSetting("VISIBILITY_PLAYLIST_EDITOR", value.ToString());
                }
                /// <summary>True, if the title of the day should be shown.</summary>
                public static bool TitleOfTheDayVisible
                {
                    get => GetSettingsValue("VISIBILITY_TITLE_OF_THE_DAY", bool.Parse, true);
                    set => SaveSetting("VISIBILITY_TITLE_OF_THE_DAY", value.ToString());
                }
                /// <summary>True, if the statistics overview should be shown.</summary>
                public static bool StatisticsOverviewVisible
                {
                    get => GetSettingsValue("VISIBILITY_STATISTICS_OVERVIEW", bool.Parse, true);
                    set => SaveSetting("VISIBILITY_STATISTICS_OVERVIEW", value.ToString());
                }
            }
        }

        /// <summary>All methods that are based on the currently active <see cref="Catalog"/>.</summary>
        public static class CatalogContext
        {
            #region Current Catalog
            /// <summary>The currently active <see cref="Catalog"/>.<br/>Can be cleared by setting the value to <c>null</c>.</summary>
            private static Catalog CURRENT_CATALOG
            {
                get
                {
                    var id = DBCONNECTION.Settings.FirstOrDefault(s => s.Key == "CURRENT_CATALOG_ID")?.Value;
                    if (id == null || int.Parse(id) == -1) return null;
                    return DBCONNECTION.Catalogs.Find(int.Parse(id));
                }
                set
                {
                    var newId = (value == null ? -1 : value.Id).ToString();
                    var originalSetting = DBCONNECTION.Settings.FirstOrDefault(s => s.Key == "CURRENT_CATALOG_ID");
                    if (originalSetting != null) originalSetting.Value = newId;
                    else DBCONNECTION.Settings.Add(new Setting { Key = "CURRENT_CATALOG_ID", Value = newId });
                    DBCONNECTION.SaveChanges();
                }
            }
            /// <summary>Id of the currently active <see cref="Catalog"/>.</summary>
            public static int? CurrentCatalogId
            {
                get => CURRENT_CATALOG?.Id;
            }
            /// <summary>Sets the currently active <see cref="Catalog"/>.</summary>
            /// <param name="catalog"></param>
            public static void SetCurrentCatalog(Catalog catalog) => CURRENT_CATALOG = catalog;
            #endregion
            /// <summary>Offers reading access to data within the currently active <see cref="Catalog"/>.</summary>
            public static class Reader
            {
                public static class Lists
                {
                    /// <summary>List of all <see cref="Medium"/>.</summary>
                    public static List<Medium> UnorderedMedia { get => CURRENT_CATALOG?.Media.ToList() ?? new List<Medium>(); }
                    /// <summary>List of all <see cref="Medium"/> ordered by title.</summary>
                    public static List<Medium> Media { get => UnorderedMedia.OrderBy(m => m.Title).ToList(); }
                    /// <summary>List of all <see cref="Medium"/> containing at least one <see cref="Part"/>.</summary>
                    public static List<Medium> NonEmptyMedia { get => CURRENT_CATALOG?.Media.Where(m => m.Parts.Count > 0).ToList() ?? new List<Medium>(); }
                    /// <summary>List of all <see cref="Part"/>s.</summary>
                    public static List<Part> UnorderedParts { get => DBCONNECTION?.Parts.Where(p => p.Medium.CatalogId == CURRENT_CATALOG.Id).ToList() ?? new List<Part>(); }
                    /// <summary>List of all <see cref="Part"/>s ordered by title.</summary>
                    public static List<Part> Parts { get => UnorderedParts.OrderBy(p => p.Title).ToList(); }
                    /// <summary>List of all <see cref="Tag"/>s.</summary>
                    public static List<Tag> UnorderedTags { get => CURRENT_CATALOG?.Tags.ToList() ?? new List<Tag>(); }
                    /// <summary>List of all <see cref="Tag"/>s ordered by title.</summary>
                    public static List<Tag> Tags { get => UnorderedTags.OrderBy(p => p.Title).ToList(); }
                    /// <summary>List of all <see cref="Playlist"/>s.</summary>
                    public static List<Playlist> UnorderedPlaylists { get => CURRENT_CATALOG?.Playlists.ToList() ?? new List<Playlist>(); }
                    /// <summary>List of all <see cref="Playlist"/>s ordered by title.</summary>
                    public static List<Playlist> Playlists { get => UnorderedPlaylists.OrderBy(p => p.Title).ToList(); }
                }
                public static class Statistics
                {
                    public static int CountOfMedia { get => CURRENT_CATALOG?.Media.Count() ?? 0; }
                    public static int CountOfParts { get => CURRENT_CATALOG?.Media.Select(m => m.Parts.Count).Sum() ?? 0; }
                }
                /// <summary>
                /// Searches through all parts of the currently active <see cref="Catalog"/> using the given parameters and returns the results either as a list of parts or as a list of media.
                /// </summary>
                /// <param name="parameters">Search parameters</param>
                /// <returns>Search result list</returns>
                public static List<SearchResultItem> SearchUsingParameters(SearchParameters parameters)
                {
                    var result = new List<SearchResultItem>();

                    var fittingSearchString = Lists.Parts.Where(p =>
                        p.Title.Contains(parameters.SearchString) ||
                        p.Medium.Title.Contains(parameters.SearchString) ||
                        (parameters.SearchWithinDescriptions && (p.Description.Contains(parameters.SearchString) || p.Medium.Description.Contains(parameters.SearchString)))
                        );

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
                        }).OrderBy(m => m.Text)) { if (!result.Any(r => r.Id == item.Id)) result.Add(item); }
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
                public static class Analysis
                {
                    private static List<GUI.Controls.Analyze.AnalyzeListElement> MapMediumToAnalyzeListElement(List<Medium> list) => list.Select(m => new GUI.Controls.Analyze.AnalyzeListElement
                    {
                        Id = m.Id,
                        Text = m.Title
                    }).ToList();
                    private static List<GUI.Controls.Analyze.AnalyzeListElement> MapPartToAnalyzeListElement(List<Part> list) => list.Select(p => new GUI.Controls.Analyze.AnalyzeListElement
                    {
                        Id = p.Id,
                        Text = p.Title + " (" + p.Medium.Title + ")"
                    }).ToList();
                    /// <summary>
                    /// Generates the result list for a specific analysis.
                    /// </summary>
                    /// <param name="mode">Analysis to perform</param>
                    /// <returns>Result list for the analysis</returns>
                    public static List<GUI.Controls.Analyze.AnalyzeListElement> LoadAnalyzeResult(GUI.Controls.Analyze.AnalyzeMode mode)
                    {
                        switch (mode)
                        {
                            case GUI.Controls.Analyze.AnalyzeMode.MediumEmpty: return MapMediumToAnalyzeListElement(Lists.Media.Where(m => m.Parts.Count == 0).ToList());
                            case GUI.Controls.Analyze.AnalyzeMode.MediumDoubled: return MapMediumToAnalyzeListElement(Lists.Media.Where(m => Lists.UnorderedMedia.Where(mm => mm.Title == m.Title).Count() > 1).ToList());
                            case GUI.Controls.Analyze.AnalyzeMode.MediumCommonTags:
                                var allTagIds = Lists.UnorderedTags.Select(t => t.Id).ToList();
                                return MapMediumToAnalyzeListElement(Lists.Media.Where(m =>
                                    m.Parts.Count > 0 &&
                                        (allTagIds.Any(t => m.Parts.All(p => p.PT_Relation.FirstOrDefault(pt => pt.TagId == t)?.Value == true) && m.MT_Relation.FirstOrDefault(mt => mt.TagId == t)?.Value != true)
                                        || allTagIds.Any(t => m.Parts.All(p => p.PT_Relation.FirstOrDefault(pt => pt.TagId == t)?.Value == false) && m.MT_Relation.FirstOrDefault(mt => mt.TagId == t)?.Value != false))
                                    ).ToList());
                            case GUI.Controls.Analyze.AnalyzeMode.MediumDescription: return MapMediumToAnalyzeListElement(Lists.Media.Where(m => m.Description.Trim().Length == 0).ToList());
                            case GUI.Controls.Analyze.AnalyzeMode.MediumTags: return MapMediumToAnalyzeListElement(Lists.Media.Where(m => m.MT_Relation.Count == 0).ToList());
                            case GUI.Controls.Analyze.AnalyzeMode.MediumLocation: return MapMediumToAnalyzeListElement(Lists.Media.Where(m => m.Location.Trim().Length == 0).ToList());
                            case GUI.Controls.Analyze.AnalyzeMode.PartDescription: return MapPartToAnalyzeListElement(Lists.Parts.Where(p => p.Description.Trim().Length == 0).ToList());
                            case GUI.Controls.Analyze.AnalyzeMode.PartTags: return MapPartToAnalyzeListElement(Lists.Parts.Where(p => p.PT_Relation.Count == 0).ToList());
                            case GUI.Controls.Analyze.AnalyzeMode.PartLength: return MapPartToAnalyzeListElement(Lists.Parts.Where(p => p.Length == 0).ToList());
                            case GUI.Controls.Analyze.AnalyzeMode.PartPublication: return MapPartToAnalyzeListElement(Lists.Parts.Where(p => p.Publication_Year == 0).ToList());
                            case GUI.Controls.Analyze.AnalyzeMode.PartImage:
                                var filteredParts = new List<Part>();
                                Lists.Parts.ToList().ForEach(p => { if (p.Image == null || p.Image.Length == 0) filteredParts.Add(p); });
                                return MapPartToAnalyzeListElement(filteredParts);
                            default: return new List<GUI.Controls.Analyze.AnalyzeListElement>();
                        }
                    }
                    /// <summary>
                    /// Retrieves all <see cref="Medium"/> with a given title.
                    /// </summary>
                    /// <param name="title">Title to search for</param>
                    /// <returns>List of <see cref="Medium"/></returns>
                    public static List<Medium> GetDoubledMediaToMediumTitle(string title) => Lists.Media.Where(m => m.Title == title).ToList();
                    /// <summary>
                    /// Checks the <see cref="Tag"/>s of all <see cref="Part"/>s of a <see cref="Medium"/> and returns the <see cref="Tags"/>, that should be commonly set.
                    /// </summary>
                    /// <param name="mediumId">Id of the <see cref="Medium"/> to analyze</param>
                    /// <returns>List of <see cref="Tag"/> ids</returns>
                    public static List<int> GetNonCommonTagsOfMedium(int mediumId)
                    {
                        var m = GlobalContext.Reader.GetMedium(mediumId);
                        return Lists.Tags.Where(t =>
                            (m.Parts.All(p => p.PT_Relation.FirstOrDefault(pt => pt.TagId == t.Id)?.Value == true) && m.MT_Relation.FirstOrDefault(mt => mt.TagId == t.Id)?.Value != true)
                            || (m.Parts.All(p => p.PT_Relation.FirstOrDefault(pt => pt.TagId == t.Id)?.Value == false) && m.MT_Relation.FirstOrDefault(mt => mt.TagId == t.Id)?.Value != false)
                        ).Select(t => t.Id).ToList();
                    }
                }
            }
            /// <summary>Offers writing access to data within the currently active <see cref="Catalog"/>.</summary>
            public static class Writer
            {
                /// <summary>
                /// Creates a new <see cref="Medium"/>.
                /// </summary>
                /// <param name="title">Title of the <see cref="Medium"/> to create</param>
                /// <param name="description">Description of the <see cref="Medium"/> to create</param>
                /// <param name="location">Location of the <see cref="Medium"/> to create</param>
                /// <param name="tags"><see cref="Tag"/>s of the <see cref="Medium"/></param>
                /// <returns>Id of the newly generated <see cref="Medium"/></returns>
                public static int CreateMedium(string title, string description, string location, List<ValuedTag> tags)
                {
                    CURRENT_CATALOG.Media.Add(new Medium
                    {
                        // catalog id must not be set here, because it is set implicitly by the CURRENT_CATALOG
                        Title = title,
                        Description = description,
                        Location = location
                    });
                    DBCONNECTION.SaveChanges();
                    var mediumId = Reader.Lists.UnorderedMedia.OrderBy(m => m.Id).LastOrDefault()?.Id ?? 0;
                    foreach (var t in tags)
                    {
                        if (!t.Value.HasValue) continue;
                        DBCONNECTION.MT_Relation.Add(new MT_Relation
                        {
                            MediaId = mediumId,
                            TagId = t.Tag.Id,
                            Value = t.Value.Value
                        });
                    }
                    DBCONNECTION.SaveChanges();
                    return mediumId;
                }
                /// <summary>
                /// Creates a new <see cref="Part"/>.
                /// </summary>
                /// <param name="part"><see cref="Part"/> to create, containing all its metadata including the corresponding medium id</param>
                /// <param name="tags"><see cref="Tag"/>s of the <see cref="Part"/></param>
                public static void CreatePart(Part part, List<ValuedTag> tags)
                {
                    DBCONNECTION.Parts.Add(part);
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
                /// <summary>
                /// Creates a new <see cref="Tag"/>.
                /// </summary>
                /// <param name="title">Title of the <see cref="Tag"/> to create</param>
                public static void CreateTag(string title)
                {
                    CURRENT_CATALOG.Tags.Add(new Tag
                    {
                        // catalog id must not be set here, because it is set implicitly by the CURRENT_CATALOG
                        Title = title
                    });
                    DBCONNECTION.SaveChanges();
                }
                /// <summary>
                /// Creates a new <see cref="Playlist"/>.
                /// </summary>
                /// <param name="title">Title of the <see cref="Playlist"/> to create</param>
                /// <returns>Id of the newly generated <see cref="Playlist"/></returns>
                public static int CreatePlaylist(string title)
                {
                    CURRENT_CATALOG.Playlists.Add(new Playlist
                    {
                        // catalog id must not be set here, because it is set implicitly by the CURRENT_CATALOG
                        Title = title
                    });
                    DBCONNECTION.SaveChanges();
                    return Reader.Lists.UnorderedPlaylists.OrderBy(p => p.Id).LastOrDefault()?.Id ?? 0;
                }

                /// <summary>
                /// Saves changes to an existing <see cref="Medium"/>.
                /// </summary>
                /// <param name="medium"><see cref="Medium"/> to save</param>
                /// <param name="tags"><see cref="Tag"/>s of the <see cref="Medium"/></param>
                public static void SaveMedium(Medium medium, List<ValuedTag> tags)
                {
                    var original = GlobalContext.Reader.GetMedium(medium.Id);
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
                    var mediumTags = GlobalContext.Reader.GetTagsForMedium(medium.Id).Where(t => t.Value.HasValue).ToList();
                    var mediumTagsIds = mediumTags.Select(t => t.Tag.Id).ToList();
                    GlobalContext.Reader.GetMedium(medium.Id).Parts.ToList().ForEach(p =>
                    {
                        var partTags = GlobalContext.Reader.GetTagsForPart(p.Id).Where(t => !mediumTagsIds.Contains(t.Tag.Id)).ToList();
                        partTags.AddRange(mediumTags);
                        SavePart(p, partTags);
                    });
                }
                /// <summary>
                /// Saves changes to an existing <see cref="Part"/>.
                /// </summary>
                /// <param name="part"><see cref="Part"/> to save, containing all its metadata including the corresponding medium id</param>
                /// <param name="tags"><see cref="Tag"/>s of the <see cref="Part"/></param>
                public static void SavePart(Part part, List<ValuedTag> tags)
                {
                    var original = GlobalContext.Reader.GetPart(part.Id);
                    original.MediumId = part.MediumId;
                    original.Title = part.Title;
                    original.Favourite = part.Favourite;
                    original.Description = part.Description;
                    original.Length = part.Length;
                    original.Publication_Year = part.Publication_Year;
                    original.Image = part.Image;
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
                /// <summary>
                /// Saves changes to an existing <see cref="Tag"/>.
                /// </summary>
                /// <param name="tag"><see cref="Tag"/> to save</param>
                public static void SaveTag(Tag tag)
                {
                    var original = GlobalContext.Reader.GetTag(tag.Id);
                    original.Title = tag.Title;
                    DBCONNECTION.SaveChanges();
                }
            }
        }

        private static string CurrentExportVersion = "1.0.0";
        private static string MinimumImportVersion = "1.0.0";
        public class CatalogExportThread : ExportThread
        {
            private Catalog catalogToExport;
            private int currentStep = -1;
            private int maxSteps = 5;
            public CatalogExportThread(string fileDestination, string exportFailedStep, string exportFailedMessage, int catalogId) : base(fileDestination, exportFailedStep, exportFailedMessage)
            {
                catalogToExport = DBCONNECTION.Catalogs.Find(catalogId);
            }

            private void step()
            {
                currentStep++;
                CallStep(currentStep / (float)maxSteps, LanguageProvider.LanguageProvider.getString("Dialog.Export.Steps." + currentStep.ToString()));
            }
            protected override void runExport()
            {
                if (catalogToExport == null) throw new Exception();
                step();
                var xmlData = new XElement("MediaManagerCatalog");
                xmlData.Add(new XAttribute("ExportVersion", CurrentExportVersion));
                xmlData.Add(new XAttribute("DownwardsCompatibleTo", MinimumImportVersion));
                xmlData.Add(new XAttribute("Title", catalogToExport.Title));
                xmlData.Add(new XAttribute("Description", catalogToExport.Description));
                xmlData.Add(new XAttribute("DeletionConfirmationMedium", catalogToExport.DeletionConfirmationMedium));
                xmlData.Add(new XAttribute("DeletionConfirmationPart", catalogToExport.DeletionConfirmationPart));
                xmlData.Add(new XAttribute("DeletionConfirmationTag", catalogToExport.DeletionConfirmationTag));
                xmlData.Add(new XAttribute("DeletionConfirmationPlaylist", catalogToExport.DeletionConfirmationPlaylist));
                xmlData.Add(new XAttribute("ShowTitleOfTheDayAsMedium", catalogToExport.ShowTitleOfTheDayAsMedium));

                #region Export Tags
                step();
                if (catalogToExport.Tags.Count > 0)
                {
                    xmlData.Add(XMLExport.ExportDataFromTable(
                        DBCONNECTION.Tags,
                        filter: (i) => i.CatalogId == catalogToExport.Id,
                        columns: new List<string> { "Id", "Title" }
                        ));
                }
                #endregion
                #region Export Media and Parts
                step();
                if (catalogToExport.Media.Count > 0)
                {
                    xmlData.Add(XMLExport.ExportDataFromTable(
                        DBCONNECTION.Media,
                        filter: (i) => i.CatalogId == catalogToExport.Id,
                        columns: new List<string> { "Title", "Description", "Location", "PositiveTags", "NegativeTags" },
                        additionalComputedProperties: new Dictionary<string, Func<Medium, object>>
                        {
                        { "PositiveTags", (i) => "[" + string.Join(",", GlobalContext.Reader.GetTagsForMedium(i.Id).Where(t => t.Value.HasValue && t.Value.Value).Select(t => t.Tag.Id).ToList()) + "]" },
                        { "NegativeTags", (i) => "[" + string.Join(",", GlobalContext.Reader.GetTagsForMedium(i.Id).Where(t => t.Value.HasValue && !t.Value.Value).Select(t => t.Tag.Id).ToList()) + "]" },
                        },
                        computeChildren: (i) =>
                        {
                            var mediaTags = GlobalContext.Reader.GetTagsForMedium(i.Id).Where(t => t.Value.HasValue).Select(t => t.Tag.Id);
                            var list = new List<XElement>();
                            foreach (var p in i.Parts)
                            {
                                var xmlPart = new XElement("Part");
                                xmlPart.Add(new XAttribute("Id", p.Id));
                                xmlPart.Add(new XAttribute("Title", p.Title));
                                xmlPart.Add(new XAttribute("Description", p.Description));
                                xmlPart.Add(new XAttribute("Favourite", p.Favourite));
                                xmlPart.Add(new XAttribute("Length", p.Length));
                                xmlPart.Add(new XAttribute("Publication_Year", p.Publication_Year));
                                if (p.Image != null) xmlPart.Add(new XAttribute("Image", Convert.ToBase64String(p.Image)));
                                var relevantPartTags = GlobalContext.Reader.GetTagsForPart(p.Id).Where(t => !mediaTags.Contains(t.Tag.Id) && t.Value.HasValue);
                                xmlPart.Add(new XAttribute("PositiveTags", "[" + string.Join(",", relevantPartTags.Where(t => t.Value.Value).Select(t => t.Tag.Id).ToList()) + "]"));
                                xmlPart.Add(new XAttribute("NegativeTags", "[" + string.Join(",", relevantPartTags.Where(t => !t.Value.Value).Select(t => t.Tag.Id).ToList()) + "]"));
                                list.Add(xmlPart);
                            }
                            return list;
                        }
                        ));
                }
                #endregion
                #region Export Playlists
                step();
                if (catalogToExport.Playlists.Count > 0)
                {
                    xmlData.Add(XMLExport.ExportDataFromTable(
                        DBCONNECTION.Playlists,
                        filter: (i) => i.CatalogId == catalogToExport.Id,
                        columns: new List<string> { "Title", "PlaylistParts" },
                        additionalComputedProperties: new Dictionary<string, Func<Playlist, object>>
                        {
                            { "PlaylistParts", (i) => "[" + string.Join(",", i.Parts.Select(p => p.Id).ToList()) + "]" },
                        }
                        ));
                }
                #endregion

                step();
                new XDocument(xmlData).Save(fileDestination);
                step();
                CallFinished();
            }
        }
        public class CatalogImportThread : ImportThread
        {
            private int currentStep = -1;
            private int maxSteps = 5;
            public CatalogImportThread(string fileSource, string importFailedStep, string importFailedMessage, string formatExceptionHeader, string dbConstraintExceptionHeader, Dictionary<string, string> dbConstraintMessages) : base(fileSource, importFailedStep, importFailedMessage, formatExceptionHeader, dbConstraintExceptionHeader, dbConstraintMessages) { }

            public string importVersion
            {
                get
                {
                    try { return XDocument.Load(fileSource).Root.Attribute("ExportVersion").Value; }
                    catch (Exception) { throw AssembleFormatException(LanguageProvider.LanguageProvider.getString("Dialog.Import.Version.Missing"), null); }
                }
            }
            public string downwardsCompatibleTo
            {
                get
                {
                    try { return XDocument.Load(fileSource).Root.Attribute("DownwardsCompatibleTo").Value; }
                    catch (Exception) { throw AssembleFormatException(LanguageProvider.LanguageProvider.getString("Dialog.Import.Version.Missing"), null); }
                }
            }

            private List<int> stringToIntList(string xmlList)
            {
                xmlList = xmlList.EndsWith("]") ? xmlList.Substring(0, xmlList.Length - 1) : xmlList;
                xmlList = xmlList.StartsWith("[") ? xmlList.Substring(1) : xmlList;
                return xmlList.Length == 0 ? new List<int>() : xmlList.Split(',').Select(t => int.Parse(t)).ToList();
            }
            private List<ValuedTag> tagIdListToTagList(List<int> ids, bool tagValue) => ids.Select(t => new ValuedTag
            {
                Tag = GlobalContext.Reader.GetTag(t),
                Value = tagValue
            }).ToList();
            private void step()
            {
                currentStep++;
                CallStep(currentStep / (float)maxSteps, LanguageProvider.LanguageProvider.getString("Dialog.Import.Steps." + currentStep.ToString()));
            }
            protected override void runImport()
            {
                step();
                if (!CheckVersionImportable(downwardsCompatibleTo, CurrentExportVersion) || !CheckVersionImportable(MinimumImportVersion, importVersion))
                {
                    CallStep(1, LanguageProvider.LanguageProvider.getString("Dialog.Import.Version.IncompatibleStep"));
                    CallFinished(new Exception(LanguageProvider.LanguageProvider.getString("Dialog.Import.Version.IncompatibleMessage")));
                    return;
                }

                var xmlData = XDocument.Load(fileSource).Root;
                var catalogId = -1;

                #region Import Catalog
                step();
                var importedCatalog = new Catalog { };
                try
                {
                    importedCatalog.Title = xmlData.Attribute("Title").Value;
                }
                catch (Exception) { throw AssembleFormatException(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Catalog.MissingTitle"), null); }
                try
                {
                    importedCatalog.Description = xmlData.Attribute("Description")?.Value ?? "";
                    importedCatalog.DeletionConfirmationMedium = !(xmlData.Attribute("DeletionConfirmationMedium")?.Value ?? "true").ToLower().Equals("false");
                    importedCatalog.DeletionConfirmationPart = !(xmlData.Attribute("DeletionConfirmationPart")?.Value ?? "true").ToLower().Equals("false");
                    importedCatalog.DeletionConfirmationTag = !(xmlData.Attribute("DeletionConfirmationTag")?.Value ?? "true").ToLower().Equals("false");
                    importedCatalog.DeletionConfirmationPlaylist = !(xmlData.Attribute("DeletionConfirmationPlaylist")?.Value ?? "true").ToLower().Equals("false");
                    importedCatalog.ShowTitleOfTheDayAsMedium = (xmlData.Attribute("ShowTitleOfTheDayAsMedium")?.Value ?? "false").ToLower().Equals("true");
                }
                catch (Exception) { throw AssembleFormatException(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Catalog.WrongFormat"), null); }
                try
                {
                    catalogId = CreateCatalog(importedCatalog);
                }
                catch (Exception) { throw ParseDBConstraintException(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Catalog.Writing"), null); }
                #endregion

                var tagIdMappings = new Dictionary<int, int>();
                var partIdMappings = new Dictionary<int, int>();
                // TODO: add explicit exceptions if one of the mappings fails due to a non existent id

                var tagList = xmlData.Element("Tags")?.Elements().ToList() ?? new List<XElement>();
                var mediaList = xmlData.Element("Mediums")?.Elements().ToList() ?? new List<XElement>();
                var playlistList = xmlData.Element("Playlists")?.Elements().ToList() ?? new List<XElement>();

                #region Import Tags
                step();
                foreach (var xmlTag in tagList)
                {
                    var xmlId = -1;
                    var xmlTitle = "";

                    try
                    {
                        xmlId = int.Parse(xmlTag.Attribute("Id").Value);
                    }
                    catch (Exception) { throw AssembleFormatException(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Tag.MissingId"), null); }
                    try
                    {
                        xmlTitle = xmlTag.Attribute("Title").Value;
                    }
                    catch (Exception) { throw AssembleFormatException(string.Format(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Tag.MissingTitle"), xmlId), null); }
                    try
                    {
                        tagIdMappings.Add(xmlId, CreateTag(new Tag
                        {
                            CatalogId = catalogId,
                            Title = xmlTitle
                        }));
                    }
                    catch (Exception) { throw ParseDBConstraintException(string.Format(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Tag.Writing"), xmlId), null); }
                }
                #endregion
                #region Import Media
                step();
                foreach(var xmlMedium in mediaList)
                {
                    var xmlMediumTitle = "";
                    var xmlMediumDescription = "";
                    var xmlMediumLocation = "";
                    var xmlMediumTags = new List<ValuedTag>();

                    try
                    {
                        xmlMediumTitle = xmlMedium.Attribute("Title").Value;
                    }
                    catch (Exception) { throw AssembleFormatException(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Medium.MissingTitle"), null); }
                    try
                    {
                        xmlMediumDescription = xmlMedium.Attribute("Description")?.Value ?? "";
                        xmlMediumLocation = xmlMedium.Attribute("Location")?.Value ?? "";
                        var positiveTagsString = xmlMedium.Attribute("PositiveTags")?.Value ?? "[]";
                        var negativeTagsString = xmlMedium.Attribute("NegativeTags")?.Value ?? "[]";
                        xmlMediumTags.AddRange(tagIdListToTagList(stringToIntList(positiveTagsString).Select(t => tagIdMappings[t]).ToList(), true));
                        xmlMediumTags.AddRange(tagIdListToTagList(stringToIntList(negativeTagsString).Select(t => tagIdMappings[t]).ToList(), false));
                    }
                    catch (Exception) { throw AssembleFormatException(string.Format(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Medium.WrongFormat"), xmlMediumTitle), null); }
                    var mediumId = -1;
                    try
                    {
                        DBCONNECTION.Media.Add(new Medium
                        {
                            CatalogId = catalogId,
                            Title = xmlMediumTitle,
                            Description = xmlMediumDescription,
                            Location = xmlMediumLocation,
                        });
                        DBCONNECTION.SaveChanges();
                        mediumId = DBCONNECTION.Media.OrderBy(m => m.Id).ToList().LastOrDefault()?.Id ?? 0;
                        foreach (var t in xmlMediumTags)
                        {
                            if (!t.Value.HasValue) continue;
                            DBCONNECTION.MT_Relation.Add(new MT_Relation
                            {
                                MediaId = mediumId,
                                TagId = t.Tag.Id,
                                Value = t.Value.Value
                            });
                        }
                        DBCONNECTION.SaveChanges();
                    }
                    catch (Exception) { throw ParseDBConstraintException(string.Format(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Medium.Writing"), xmlMediumTitle), null); }

                    var partList = xmlMedium.Elements()?.ToList() ?? new List<XElement>();

                    foreach (var xmlPart in partList)
                    {
                        var xmlPartId = -1;
                        var xmlPartTitle = "";
                        var xmlPartDescription = "";
                        var xmlPartFavourite = false;
                        var xmlPartLength = 0;
                        var xmlPartPublication_Year = 0;
                        byte[] xmlPartImage = null;
                        var xmlPartTags = new List<ValuedTag>();
                        xmlPartTags.AddRange(xmlMediumTags);

                        try
                        {
                            xmlPartId = int.Parse(xmlPart.Attribute("Id").Value);
                        }
                        catch (Exception) { throw AssembleFormatException(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Part.MissingId"), null); }
                        try
                        {
                            xmlPartTitle = xmlPart.Attribute("Title").Value;
                        }
                        catch (Exception) { throw AssembleFormatException(string.Format(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Part.MissingTitle"), xmlPartId), null); }
                        try
                        {
                            xmlPartDescription = xmlPart.Attribute("Description")?.Value ?? "";
                            xmlPartFavourite = (xmlPart.Attribute("Favourite")?.Value ?? "false").ToLower().Equals("true");
                            xmlPartLength = int.Parse(xmlPart.Attribute("Length")?.Value ?? "0");
                            xmlPartPublication_Year = int.Parse(xmlPart.Attribute("Publication_Year")?.Value ?? "0");
                            var imgAttribute = xmlPart.Attribute("Image")?.Value;
                            xmlPartImage = imgAttribute != null ? Convert.FromBase64String(imgAttribute) : null;
                            var positiveTagsString = xmlPart.Attribute("PositiveTags")?.Value ?? "[]";
                            var negativeTagsString = xmlPart.Attribute("NegativeTags")?.Value ?? "[]";
                            xmlPartTags.AddRange(tagIdListToTagList(stringToIntList(positiveTagsString).Select(t => tagIdMappings[t]).ToList(), true));
                            xmlPartTags.AddRange(tagIdListToTagList(stringToIntList(negativeTagsString).Select(t => tagIdMappings[t]).ToList(), false));
                        }
                        catch (Exception) { throw AssembleFormatException(string.Format(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Part.WrongFormat"), xmlPartId), null); }
                        try
                        {
                            partIdMappings.Add(xmlPartId, CreatePart(new Part
                            {
                                MediumId = mediumId,
                                Title = xmlPartTitle,
                                Description = xmlPartDescription,
                                Favourite = xmlPartFavourite,
                                Length = xmlPartLength,
                                Publication_Year = xmlPartPublication_Year,
                                Image = xmlPartImage,
                            }, xmlPartTags));
                        }
                        catch (Exception) { throw ParseDBConstraintException(string.Format(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Part.Writing"), xmlPartId), null); }
                    }
                }
                #endregion
                #region Import Playlists
                step();
                foreach(var xmlPlaylist in playlistList)
                {
                    var xmlTitle = "";
                    var xmlPlaylistParts = new List<int>();

                    try
                    {
                        xmlTitle = xmlPlaylist.Attribute("Title").Value;
                    }
                    catch (Exception) { throw AssembleFormatException(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Playlist.MissingTitle"), null); }
                    try
                    {
                        var partsString = xmlPlaylist.Attribute("PlaylistParts")?.Value ?? "[]";
                        xmlPlaylistParts = stringToIntList(partsString).Select(p => partIdMappings[p]).ToList();
                    }
                    catch (Exception) { throw AssembleFormatException(string.Format(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Playlist.WrongFormat"), xmlTitle), null); }
                    try
                    {
                        DBCONNECTION.Playlists.Add(new Playlist
                        {
                            CatalogId = catalogId,
                            Title = xmlTitle
                        });
                        DBCONNECTION.SaveChanges();
                        var playlistId = DBCONNECTION.Playlists.OrderBy(p => p.Id).ToList().LastOrDefault()?.Id ?? 0;
                        xmlPlaylistParts.ForEach(p => GlobalContext.Writer.AddPartToPlaylist(playlistId, p));
                    }
                    catch (Exception) { throw ParseDBConstraintException(string.Format(LanguageProvider.LanguageProvider.getString("Dialog.Import.Exceptions.Playlist.Writing"), xmlTitle), null); }
                }
                #endregion

                step();
                CallFinished();
            }

            private int CreateCatalog(Catalog catalog)
            {
                DBCONNECTION.Catalogs.Add(catalog);
                DBCONNECTION.SaveChanges();
                return DBCONNECTION.Catalogs.OrderBy(c => c.Id).ToList().LastOrDefault()?.Id ?? 0;
            }
            private int CreateTag(Tag tag)
            {
                DBCONNECTION.Tags.Add(tag);
                DBCONNECTION.SaveChanges();
                return DBCONNECTION.Tags.OrderBy(t => t.Id).ToList().LastOrDefault()?.Id ?? 0;
            }
            private int CreatePart(Part part, List<ValuedTag> tags)
            {
                DBCONNECTION.Parts.Add(part);
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
                return DBCONNECTION.Parts.OrderBy(p => p.Id).ToList().LastOrDefault()?.Id ?? 0;
            }
        }
    }
}
