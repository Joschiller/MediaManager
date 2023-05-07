using MediaManager.Globals.DefaultDialogs;
using MediaManager.Globals.LanguageProvider;
using MediaManager.GUI.Atoms;
using MediaManager.GUI.Controls.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for MergeMenu.xaml
    /// </summary>
    public partial class MergeMenu : Window, UpdatedLanguageUser
    {
        #region Setup
        private List<MediumWithTags> mediaToMerge;
        private MediumWithTags mergedMedium;
        public MergeMenu(string mediumTitle)
        {
            InitializeComponent();
            RegisterAtLanguageProvider();

            mergedMedium = new MediumWithTags
            {
                Title = mediumTitle
            };

            mediaSelection.SetItems(CatalogContext.Reader.Analysis.GetDoubledMediaToMediumTitle(mediumTitle).Select(m => new CheckedListItem
            {
                Id = m.Id,
                Text = "#" + m.Id.ToString() + ": \"" + m.Title + "\""
            }).ToList());
            prepareMediumSelection();
        }
        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            mergeTab.Header = LanguageProvider.getString("Menus.Merge.Tabs.Merge");
            previewTab.Header = LanguageProvider.getString("Menus.Merge.Tabs.Preview");

            nextButton1.Content = LanguageProvider.getString("Common.Button.Next");
            nextButton2.Content = LanguageProvider.getString("Common.Button.Next");

            mediaSelectionSection.Header = LanguageProvider.getString("Menus.Merge.mediaSelectionSection");

            mediaDataSection.Header = LanguageProvider.getString("Menus.Merge.mediaDataSection");
            labelMediaDataPreviewSelection.Text = LanguageProvider.getString("Menus.Merge.labelMediaDataPreviewSelection") + ":";
            labelMediaDataDescriptionSelection.Text = LanguageProvider.getString("Menus.Merge.labelMediaDataDescriptionSelection") + ":";
            labelMediaDataTagSelection.Text = LanguageProvider.getString("Menus.Merge.labelMediaDataTagSelection") + ":";
            labelMediaDataLocationSelection.Text = LanguageProvider.getString("Menus.Merge.labelMediaDataLocationSelection") + ":";

            partSelectionSection.Header = LanguageProvider.getString("Menus.Merge.partSelectionSection");
            mergeButton.Content = LanguageProvider.getString("Menus.Merge.mergeButton");

            submitButton.Content = LanguageProvider.getString("Menus.Merge.submitButton");
        }
        #endregion

        #region Merge Process
        #region Medium Selection
        private void prepareMediumSelection()
        {
            mediaSelectionSection.IsEnabled = true;
            mediaDataSection.IsEnabled = false;
            partSelectionSection.IsEnabled = false;
            previewTab.IsEnabled = false;
        }
        private void mediaSelection_SelectionChanged(CheckedListItem item)
        {
            var medium = GlobalContext.Reader.GetMedium(item.Id);
            details.Medium = new Controls.Edit.EditableMedium
            {
                Id = medium.Id,
                CatalogId = medium.CatalogId,
                Title = medium.Title,
                Description = medium.Description,
                Tags = GlobalContext.Reader.GetTagsForMedium(item.Id),
                Location = medium.Location
            };
        }
        private void nextButton1_Click(object sender, RoutedEventArgs e) => prepareMediaDataSelection();
        #endregion
        #region Media Data
        private void prepareMediaDataSelection()
        {
            if (mediaSelection.GetCheckedItems().Count < 2)
            {
                ShowDefaultDialog(LanguageProvider.getString("Menus.Merge.Dialog.MissingMediaSelection"), Globals.DefaultDialogs.SuccessMode.Error);
                return;
            }

            mediaSelectionSection.IsEnabled = false;
            mediaDataSection.IsEnabled = true;
            partSelectionSection.IsEnabled = false;
            previewTab.IsEnabled = false;

            // collect media
            mediaToMerge = new List<MediumWithTags>();
            foreach(var m in mediaSelection.GetCheckedItems())
            {
                var medium = GlobalContext.Reader.GetMedium(m.Id);
                mediaToMerge.Add(new MediumWithTags
                {
                    Id = medium.Id,
                    CatalogId = medium.CatalogId,
                    Title = medium.Title,
                    Description = medium.Description,
                    Location = medium.Location,
                    Tags = GlobalContext.Reader.GetTagsForMedium(m.Id),
                    Parts = medium.Parts.Select(p => new PartWithTags
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Favourite = p.Favourite,
                        Length = p.Length,
                        Publication_Year = p.Publication_Year,
                        Image = p.Image,
                        Tags = GlobalContext.Reader.GetTagsForPart(p.Id)
                    }).ToList()
                });
            }
            mediaDataPreviewSelection.ItemsSource = mediaToMerge;
            mediaDataPreviewSelection.SelectedIndex = 0;
            mediaDataDescriptionSelection.ItemsSource = mediaToMerge;
            mediaDataDescriptionSelection.SelectedIndex = 0;
            mediaDataTagSelection.ItemsSource = mediaToMerge;
            mediaDataTagSelection.SelectedIndex = 0;
            mediaDataLocationSelection.ItemsSource = mediaToMerge;
            mediaDataLocationSelection.SelectedIndex = 0;

            // load parts into list
            var partList = new List<CheckedListItem>();
            mediaToMerge.ForEach(m => m.Parts.ForEach(p => partList.Add(new CheckedListItem
            {
                Id = p.Id,
                Text = p.Title + " (#" + m.Id.ToString() + ": \"" + m.Title + "\")"
            })));
            partSelection.SetItems(partList);
        }
        private void mediaDataPreviewSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var medium = mediaDataPreviewSelection.SelectedItem as MediumWithTags;
            details.Medium = new EditableMedium
            {
                Id = medium.Id,
                CatalogId = medium.CatalogId,
                Title = medium.Title,
                Description = medium.Description,
                Tags = medium.Tags,
                Location = medium.Location
            };
        }
        private void mediaDataDescriptionSelection_SelectionChanged(object sender, SelectionChangedEventArgs e) => mergedMedium.Description = ((MediumWithTags)mediaDataDescriptionSelection.SelectedItem).Description;
        private void mediaDataTagSelection_SelectionChanged(object sender, SelectionChangedEventArgs e) => mergedMedium.Tags = ((MediumWithTags)mediaDataTagSelection.SelectedItem).Tags;
        private void mediaDataLocationSelection_SelectionChanged(object sender, SelectionChangedEventArgs e) => mergedMedium.Location = ((MediumWithTags)mediaDataLocationSelection.SelectedItem).Location;
        private void nextButton2_Click(object sender, RoutedEventArgs e) => preparePartSelection();
        #endregion
        #region Part Selection
        private void preparePartSelection()
        {
            if (mediaDataDescriptionSelection.SelectedItem == null || mediaDataTagSelection.SelectedItem == null || mediaDataLocationSelection.SelectedItem == null)
            {
                // should not occur
                ShowDefaultDialog(LanguageProvider.getString("Menus.Merge.Dialog.MissingMediaData"), Globals.DefaultDialogs.SuccessMode.Error);
                return;
            }

            mediaSelectionSection.IsEnabled = false;
            mediaDataSection.IsEnabled = false;
            partSelectionSection.IsEnabled = true;
            previewTab.IsEnabled = false;

            // clear preview
            details.Medium = new EditableMedium
            {
                Id = -1,
                CatalogId = -1,
                Title = "",
                Description = "",
                Location = "",
                Tags = new List<ValuedTag>()
            };
        }
        private void partSelection_SelectionChanged(CheckedListItem item)
        {
            var part = GlobalContext.Reader.GetPart(item.Id);
            details.Part = new EditablePart
            {
                Id = part.Id,
                MediumId = part.MediumId,
                Title = part.Title,
                Description = part.Description,
                Favourite = part.Favourite,
                Length = part.Length,
                Publication_Year = part.Publication_Year,
                Image = part.Image,
                Tags = GlobalContext.Reader.GetTagsForPart(item.Id),
                TagsBlockedByMedium = new List<int>() // not needed here
            };
        }
        private void mergeButton_Click(object sender, RoutedEventArgs e) => prepareMergePreview();
        #endregion
        #region Preview
        private void prepareMergePreview()
        {
            if (partSelection.GetCheckedItems().Count == 0 && mediaToMerge.Select(m => m.Parts.Count).Sum() > 0)
            {
                var result = new GeneralButtonBasedDialog(Globals.Navigation.GeneralButtonBasedDialogStyle)
                    .WithTitle(LanguageProvider.getString("ApplicationName"))
                    .WithText(LanguageProvider.getString("Menus.Merge.Dialog.NoPartSelected"))
                    .WithBorder(SuccessMode.Error)
                    .WithCancelButton("_" + LanguageProvider.getString("Common.Button.No"), result: false)
                    .WithNeutralButton("_" + LanguageProvider.getString("Common.Button.Yes"), result: true)
                    .ShowForResult() as bool?;
                if (!result.HasValue || !result.Value) return;
            }

            mediaSelectionSection.IsEnabled = false;
            mediaDataSection.IsEnabled = false;
            partSelectionSection.IsEnabled = false;
            previewTab.IsEnabled = true;

            // assemble parts
            var tagsIdsSetByMedium = mergedMedium.Tags.Where(t => t.Value.HasValue).Select(t => t.Tag.Id).ToList();
            var tagValuesSetByMedium = mergedMedium.Tags.Where(t => t.Value.HasValue).ToList();
            mergedMedium.Parts = new List<PartWithTags>();
            foreach (var p in partSelection.GetCheckedItems())
            {
                var part = GlobalContext.Reader.GetPart(p.Id);
                var partTags = GlobalContext.Reader.GetTagsForPart(p.Id).Where(t => !tagsIdsSetByMedium.Contains(t.Tag.Id)).ToList();
                tagValuesSetByMedium.ForEach(t => partTags.Add(new ValuedTag
                {
                    Tag = t.Tag,
                    Value = t.Value
                }));
                mergedMedium.Parts.Add(new PartWithTags
                {
                    Id = part.Id,
                    Title = part.Title,
                    Description = part.Description,
                    Favourite = part.Favourite,
                    Length = part.Length,
                    Publication_Year = part.Publication_Year,
                    Image = part.Image,
                    Tags = partTags
                });
            }

            // load into preview
            preview.Medium = mergedMedium;

            previewTab.IsSelected = true;
        }
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            var result = new GeneralButtonBasedDialog(Globals.Navigation.GeneralButtonBasedDialogStyle)
                .WithTitle(LanguageProvider.getString("ApplicationName"))
                .WithText(LanguageProvider.getString("Menus.Merge.Dialog.SubmitMerge"))
                .WithBorder(SuccessMode.Error)
                .WithCancelButton("_" + LanguageProvider.getString("Common.Button.No"), result: false)
                .WithNeutralButton("_" + LanguageProvider.getString("Common.Button.Yes"), result: true)
                .ShowForResult() as bool?;
            if (!result.HasValue || !result.Value) return;

            // store
            // 1. Create new medium
            var mediumId = CatalogContext.Writer.CreateMedium(new Medium
            {
                CatalogId = mergedMedium.Id,
                Title = mergedMedium.Title,
                Description = mergedMedium.Description,
                Location = mergedMedium.Location,
            }, mergedMedium.Tags);
            // 2. Move old parts and update their tags
            foreach(var p in mergedMedium.Parts)
            {
                CatalogContext.Writer.SavePart(new Part
                {
                    Id = p.Id,
                    MediumId = mediumId,
                    Title = p.Title,
                    Description = p.Description,
                    Favourite = p.Favourite,
                    Length = p.Length,
                    Publication_Year = p.Publication_Year,
                    Image = p.Image,
                }, p.Tags);
            }
            // 3. Delete remaining merged media
            foreach (var m in mediaToMerge) GlobalContext.Writer.DeleteMedium(m.Id); // TODO: deleting the media fails due to FK-violation

            // finish
            ShowDefaultDialog(LanguageProvider.getString("Menus.Merge.Dialog.Success"), SuccessMode.Success);
            Close();
        }
        #endregion
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e)
        {
            var result = new GeneralButtonBasedDialog(Globals.Navigation.GeneralButtonBasedDialogStyle)
                .WithTitle(LanguageProvider.getString("ApplicationName"))
                .WithText(LanguageProvider.getString("Menus.Merge.Dialog.UnfinishedMerge"))
                .WithBorder(SuccessMode.Error)
                .WithCancelButton("_" + LanguageProvider.getString("Common.Button.No"), result: false)
                .WithNeutralButton("_" + LanguageProvider.getString("Common.Button.Yes"), result: true)
                .ShowForResult() as bool?;
            if (result.HasValue && result.Value) Close();
        }
        private void NavigationBar_HelpClicked(object sender, EventArgs e) => OpenHelpMenu();
        #endregion
    }
}
