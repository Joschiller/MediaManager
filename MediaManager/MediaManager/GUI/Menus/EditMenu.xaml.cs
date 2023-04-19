using MediaManager.Globals.LanguageProvider;
using System;
using System.Linq;
using System.Windows;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

/* TODO:
 * Alternate design idea:
 * - the view/edit mode is set for the whole menu
 * - the menu stores all data (medium and parts) temporarly and overhands them to the sub controls
 * - when saving, new media/parts are generated and old ones are updated/deleted
 */
namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for EditMenu.xaml
    /// </summary>
    public partial class EditMenu : Window, UpdatedLanguageUser
    {
        #region Setup
        private int MediumId;
        private bool AnyChangeMade = false;

        /// <summary>
        /// Opens the editor or viewer for the given medium.
        /// </summary>
        /// <param name="mediumId">Medium to open - creates a new one if null</param>
        /// <param name="partId">Part within the medium to open - only possible if medium exists</param>
        /// <param name="edit">Whether to directly start editing - will always be the case for new media</param>
        public EditMenu(int? mediumId, int? partId, bool edit)
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
            MediumId = mediumId ?? -1;
            updateVisibility(edit);

            if (IsExistingMedium)
            {
                reloadData();
                if (partId.HasValue)
                {
                    viewer.OpenPartTab(partId.Value);
                    editor.OpenPartTab(partId.Value);
                }
            }
            else
            {
                editor.Medium = new Controls.Edit.MediumWithTags
                {
                    Id = MediumId,
                    CatalogueId = CURRENT_CATALOGUE.Id,
                    Title = "",
                    Description = "",
                    Location = "",
                    Tags = CURRENT_CATALOGUE.Tags.Select(t => new ValuedTag
                    {
                        Tag = t,
                        Value = null
                    }).ToList(),
                    Parts = new System.Collections.Generic.List<Controls.Edit.PartWithTags>()
                };
                updateVisibility(true); // start editing
            }
        }
        private bool IsExistingMedium { get => MediumId >= 0; }
        private void reloadData()
        {
            if (IsExistingMedium)
            {
                var medium = Reader.GetMedium(MediumId);
                var mediaData = new Controls.Edit.MediumWithTags
                {
                    Id = medium.Id,
                    CatalogueId = medium.CatalogueId,
                    Title = medium.Title,
                    Description = medium.Description,
                    Location = medium.Location,
                    Tags = Reader.GetTagsForMedium(MediumId),
                    Parts = medium.Parts.Select(p => new Controls.Edit.PartWithTags
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Favourite = p.Favourite,
                        Length = p.Length,
                        Publication_Year = p.Publication_Year,
                        Image = p.Image,
                        Tags = Reader.GetTagsForPart(p.Id)
                    }).ToList()
                };
                viewer.Medium = mediaData;
                editor.Medium = mediaData;
            }
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnDeleteMedium"] = LanguageProvider.getString("Menus.Edit.ToolTip.DeleteMedium");
        }
        #endregion

        private bool validate() => RunValidation(new System.Collections.Generic.List<Func<string>>
        {
            () => editor.Medium.Title.Trim().Length == 0 ? LanguageProvider.getString("Menus.Edit.Validation.MediaTitle") : null,
            () => editor.Medium.Parts.Any(p => p.Title.Trim().Length == 0) ? LanguageProvider.getString("Menus.Edit.Validation.PartTitle") : null,
            () => editor.Medium.Tags.Where(t => t.Value.HasValue).Any(t => editor.Medium.Parts.Any(p => p.Tags.Find(pt => pt.Tag.Id == t.Tag.Id).Value != t.Value)) ? LanguageProvider.getString("Menus.Edit.Validation.PartTags") : null,
        });
        private bool save()
        {
            if (!validate()) return false;
            try
            {
                var data = editor.Medium;
                if (IsExistingMedium)
                {
                    Writer.SaveMedium(new Medium
                    {
                        Id = MediumId,
                        CatalogueId = data.CatalogueId,
                        Title = data.Title,
                        Description = data.Description,
                        Location = data.Location
                    }, data.Tags);
                    // remove unused parts
                    var oldParts = Reader.GetMedium(MediumId).Parts;
                    var newParts = data.Parts.Select(p => p.Id).Where(p => p >= 0).ToList();
                    oldParts.Where(p => !newParts.Contains(p.Id)).ToList().ForEach(p => Writer.DeletePart(p.Id));
                    // save updated parts
                    data.Parts.Where(p => p.Id >= 0).ToList().ForEach(p => Writer.SavePart(new Part
                    {
                        Id = p.Id,
                        MediumId = MediumId,
                        Title = p.Title,
                        Description = p.Description,
                        Favourite = p.Favourite,
                        Length = p.Length,
                        Publication_Year = p.Publication_Year,
                        Image = p.Image,
                    }, p.Tags));
                    // add new parts
                    data.Parts.Where(p => p.Id < 0).ToList().ForEach(p => Writer.CreatePart(new Part
                    {
                        MediumId = MediumId,
                        Title = p.Title,
                        Description = p.Description,
                        Favourite = p.Favourite,
                        Length = p.Length,
                        Publication_Year = p.Publication_Year,
                        Image = p.Image,
                    }, p.Tags));
                }
                else
                {
                    var mediumId = Writer.CreateMedium(new Medium
                    {
                        CatalogueId = data.CatalogueId,
                        Title = data.Title,
                        Description = data.Description,
                        Location = data.Location
                    }, data.Tags);
                    data.Parts.ForEach(p => Writer.CreatePart(new Part
                    {
                        MediumId = mediumId,
                        Title = p.Title,
                        Description = p.Description,
                        Favourite = p.Favourite,
                        Length = p.Length,
                        Publication_Year = p.Publication_Year,
                        Image = p.Image,
                    }, p.Tags));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e)
        {
            if (editor.Visibility == Visibility.Collapsed)
            {
                Close();
                return;
            }

            var confirmation = !AnyChangeMade;
            if (!confirmation)
            {
                var result = ShowUnsavedChangesDialog();
                confirmation = result.HasValue;
                // if should save => try saving and cancel closing, if saving fails
                if (result.HasValue && result.Value && !save()) confirmation = false;
            }
            if (confirmation) Close();
        }
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void updateVisibility(bool editMode)
        {
            viewer.Visibility = editMode ? Visibility.Collapsed : Visibility.Visible;
            editor.Visibility = editMode ? Visibility.Visible : Visibility.Collapsed;
            Resources["viewerButtonsVisibility"] = editMode ? Visibility.Collapsed : Visibility.Visible;
            Resources["editorButtonsVisibility"] = editMode ? Visibility.Visible : Visibility.Collapsed;
            Resources["saveEnabled"] = AnyChangeMade;
        }
        private void btnDeleteMediumClick(object sender, RoutedEventArgs e)
        {
            var performDeletion = !CURRENT_CATALOGUE.DeletionConfirmationMedium;
            if (!performDeletion)
            {
                var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString("Menus.Edit.MediaDeletion"));
                performDeletion = confirmation.HasValue && confirmation.Value;
            }
            if (performDeletion)
            {
                if (IsExistingMedium) Writer.DeleteMedium(MediumId);
                Close();
            }
        }
        private void btnEditMediumClick(object sender, RoutedEventArgs e)
        {
            editor.Medium = viewer.Medium;
            updateVisibility(true);
        }
        private void btnUndoChangesClick(object sender, RoutedEventArgs e)
        {
            if (!IsExistingMedium) Close();
            AnyChangeMade = false;
            updateVisibility(false);
        }
        private void btnSaveMediumClick(object sender, RoutedEventArgs e)
        {
            save();
            reloadData();
            AnyChangeMade = false;
            updateVisibility(false);
        }
        #endregion

        private void editor_MediumEdited(Controls.Edit.MediumWithTags medium)
        {
            AnyChangeMade = true;
            updateVisibility(true);
        }
    }
}
