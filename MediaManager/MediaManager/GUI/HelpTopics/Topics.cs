using MediaManager.Properties;
using System.Collections.Generic;
using System.Linq;

namespace MediaManager.GUI.HelpTopics
{
    public static class Topics
    {
        private static List<ShortHelpTopic> internalHelpTopics = new List<ShortHelpTopic>
        {
            new ShortHelpTopic
            {
                LanguageFileKey = "CatalogMenu",
                PageImages = new List<System.Drawing.Bitmap> { Resources.CatalogMenu, Resources.CatalogMenu_Menu },
                Children = new List<ShortHelpTopic>
                {
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "EditCatalog",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.add_catalog, Resources.CatalogMenu_Edit, Resources.delete_catalog }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "SelectCatalog",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.CatalogMenu_Select }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "ExportCatalog",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.export }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "ImportCatalog",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.import }
                    },
                }
            },
            new ShortHelpTopic
            {
                LanguageFileKey = "OverviewMenu",
                PageImages = new List<System.Drawing.Bitmap> { Resources.OverviewMenu, Resources.OverviewMenu_Menu },
                Children = new List<ShortHelpTopic>
                {
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "Editing",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.add_medium, Resources.add_tag, Resources.OverviewMenu_Search }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "Search",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.OverviewMenu_Search_Text, Resources.OverviewMenu_Search_Tags, Resources.OverviewMenu_Search }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "Playlists",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.OverviewMenu_Playlist_Generate, Resources.OverviewMenu_Playlist_View, Resources.OverviewMenu_Playlist_AddEntry, Resources.OverviewMenu_Playlist_View }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "TitleOfTheDay",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.OverviewMenu_TitleOfTheDay }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "Statistics",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.OverviewMenu_Statistics }
                    },
                }
            },
            new ShortHelpTopic
            {
                LanguageFileKey = "EditMenu",
                PageImages = new List<System.Drawing.Bitmap> { Resources.EditMenu, Resources.EditMenu_Menu },
                Children = new List<ShortHelpTopic>
                {
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "Viewing",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.EditMenu_View }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "Editing",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.edit_blue, Resources.TagClick, Resources.delete_medium, Resources.add_part, Resources.delete_part, Resources.EditMenu_Menu },
                        Children = new List<ShortHelpTopic>
                        {
                            new ShortHelpTopic
                            {
                                LanguageFileKey = "Medium",
                                PageImages = new List<System.Drawing.Bitmap> { Resources.EditMenu, Resources.EditMenu, Resources.EditMenu }
                            },
                            new ShortHelpTopic
                            {
                                LanguageFileKey = "Part",
                                PageImages = new List<System.Drawing.Bitmap> { Resources.EditMenu_Part, Resources.EditMenu_Part, Resources.EditMenu_Part, Resources.EditMenu_Part, Resources.EditMenu_Part, Resources.EditMenu_Part }
                            },
                        }
                    },
                }
            },
            new ShortHelpTopic
            {
                LanguageFileKey = "TagMenu",
                PageImages = new List<System.Drawing.Bitmap> { Resources.TagMenu, Resources.TagMenu_Menu },
                Children = new List<ShortHelpTopic>
                {
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "EditTag",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.add_tag, Resources.TagMenu_Edit, Resources.delete_tag }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "TagValues",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.TagMenu_Medium, Resources.TagClick, Resources.save }
                    },
                }
            },
            new ShortHelpTopic
            {
                LanguageFileKey = "SettingsMenu",
                PageImages = new List<System.Drawing.Bitmap> { Resources.SettingsMenu_Global, Resources.SettingsMenu_Menu },
                Children = new List<ShortHelpTopic>
                {
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "BackupSettings",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.SettingsMenu_Backup }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "GlobalSettings",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.SettingsMenu_Global, Resources.SettingsMenu_Global, Resources.SettingsMenu_Global }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "CatalogSettings",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.SettingsMenu_Catalog, Resources.SettingsMenu_Catalog }
                    },
                }
            },
            new ShortHelpTopic
            {
                LanguageFileKey = "AnalyzeMenu",
                PageImages = new List<System.Drawing.Bitmap> { Resources.AnalyzeMenu, Resources.AnalyzeMenu_Menu },
                Children = new List<ShortHelpTopic>
                {
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "Preview",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.AnalyzeMenu, Resources.AnalyzeMenu_Doubled, Resources.AnalyzeMenu_MediumTag }
                    },
                    new ShortHelpTopic
                    {
                        LanguageFileKey = "Edit",
                        PageImages = new List<System.Drawing.Bitmap> { Resources.edit_blue, Resources.AnalyzeMenu_MergeEdit, Resources.AnalyzeMenu_MergePreview }
                    },
                }
            },
        };

        private static List<HelpTopic> generateHelpTopics(List<ShortHelpTopic> shortTopics, string parentPath = null) => shortTopics.Select(topic => {
            var basePath = (parentPath != null ? parentPath + "." : "") + topic.LanguageFileKey;
            var pages = new List<HelpTopicPage>();
            for (int i = 0; i < topic.PageImages.Count; i++)
            {
                pages.Add(new HelpTopicPage
                {
                    PageCaption = basePath + ".Pages." + i.ToString() + ".PageCaption",
                    Image = topic.PageImages[i],
                    Content = basePath + ".Pages." + i.ToString() + ".Content"
                });
            }

            return new HelpTopic
            {
                TreeCaption = basePath + ".TreeCaption",
                Pages = pages,
                Children = topic.Children != null ? generateHelpTopics(topic.Children, basePath + ".Topics") : null
            };
        }).ToList();

        public static List<HelpTopic> HelpTopics { get => generateHelpTopics(internalHelpTopics); }
    }
}
