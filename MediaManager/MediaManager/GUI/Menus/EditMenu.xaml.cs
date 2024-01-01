using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.KeyboardShortcutHelper;
using static MediaManager.Globals.Navigation;

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
                    CatalogId = CatalogContext.CurrentCatalogId.Value,
                    Title = "",
                    Description = "",
                    Location = "",
                    Tags = CatalogContext.Reader.Lists.Tags.Select(t => new ValuedTag
                    {
                        Tag = t,
                        Value = null
                    }).ToList(),
                    Parts = new System.Collections.Generic.List<Controls.Edit.PartWithTags>()
                };
                updateVisibility(true); // start editing
            }
        }
        public void RegisterAtLanguageProvider() => RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnDeleteMedium"] = getString("Menus.Edit.ToolTip.DeleteMedium");
            Resources["btnEditMedium"] = getString("Menus.Edit.ToolTip.EditMedium");
            Resources["btnUndoChanges"] = getString("Menus.Edit.ToolTip.Discard");
            Resources["btnSaveMedium"] = getString("Menus.Edit.ToolTip.Save");
        }
        private bool IsExistingMedium { get => MediumId >= 0; }
        private void reloadData()
        {
            if (IsExistingMedium)
            {
                var medium = GlobalContext.Reader.GetMedium(MediumId);
                var mediaData = new Controls.Edit.MediumWithTags
                {
                    Id = medium.Id,
                    CatalogId = medium.CatalogId,
                    Title = medium.Title,
                    Description = medium.Description,
                    Location = medium.Location,
                    Tags = GlobalContext.Reader.GetTagsForMedium(MediumId),
                    Parts = medium.Parts.Select(p => new Controls.Edit.PartWithTags
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
                };
                viewer.Medium = mediaData;
                editor.Medium = mediaData;
            }
        }
        #endregion

        #region Handler
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosingPermitted()) e.Cancel = true;
        }
        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            runKeyboardShortcut(e, new System.Collections.Generic.Dictionary<(ModifierKeys Modifiers, Key Key), Action>
            {
                [(ModifierKeys.None, Key.F1)] = () => { OpenHelpMenu(); e.Handled = true; },
                [(ModifierKeys.None, Key.Escape)] = () => { Back(); e.Handled = true; },
                [(ModifierKeys.Control, Key.E)] = () => { EditMedium(); e.Handled = true; },
                [(ModifierKeys.Control, Key.R)] = () => { UndoChanges(); e.Handled = true; },
                [(ModifierKeys.Control, Key.S)] = () => { SaveMedium(); e.Handled = true; },
                [(ModifierKeys.Control, Key.N)] = () =>
                {
                    if (editor.Visibility == Visibility.Visible)
                    {
                        editor.HandleKeycode(Key.N, false);
                        e.Handled = true;
                    }
                },
                [(ModifierKeys.Control, Key.M)] = () =>
                {
                    if (viewer.Visibility == Visibility.Visible) viewer.OpenMediumTab();
                    else editor.OpenMediumTab();
                    e.Handled = true;
                },
                [(ModifierKeys.Control, Key.F)] = () =>
                {
                    if (editor.Visibility == Visibility.Visible)
                    {
                        editor.HandleKeycode(Key.F, false);
                        e.Handled = true;
                    }
                },
                [(ModifierKeys.Control, Key.I)] = () =>
                {
                    if (editor.Visibility == Visibility.Visible)
                    {
                        editor.HandleKeycode(Key.I, false);
                        e.Handled = true;
                    }
                },
                [(ModifierKeys.Control | ModifierKeys.Shift, Key.I)] = () =>
                {
                    if (editor.Visibility == Visibility.Visible)
                    {
                        editor.HandleKeycode(Key.I, true);
                        e.Handled = true;
                    }
                },
                [(ModifierKeys.Control, Key.D)] = () =>
                {
                    if (viewer.Visibility == Visibility.Visible)
                    {
                        DeleteMedium();
                        e.Handled = true;
                    }
                },
                [(ModifierKeys.None, Key.Delete)] = () =>
                {
                    if (viewer.Visibility == Visibility.Visible)
                    {
                        DeleteMedium();
                        e.Handled = true;
                    }
                },
            }, false); // the cases are that complex, that every case must set Handled itself (especially important for the deletion events that may be needed in inner components)
        }
        private bool validate() => RunValidation(new System.Collections.Generic.List<Func<string>>
        {
            () => editor.Medium.Title.Trim().Length == 0 ? getString("Menus.Edit.Validation.MediaTitle") : null,
            () => editor.Medium.Parts.Any(p => p.Title.Trim().Length == 0) ? getString("Menus.Edit.Validation.PartTitle") : null,
            () => editor.Medium.Tags.Where(t => t.Value.HasValue).Any(t => editor.Medium.Parts.Any(p => p.Tags.Find(pt => pt.Tag.Id == t.Tag.Id).Value != t.Value)) ? getString("Menus.Edit.Validation.PartTags") : null,
        });
        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Back();
        private void NavigationBar_HelpClicked(object sender, EventArgs e) => OpenHelpMenu();

        private void updateVisibility(bool editMode)
        {
            viewer.Visibility = editMode ? Visibility.Collapsed : Visibility.Visible;
            editor.Visibility = editMode ? Visibility.Visible : Visibility.Collapsed;
            Resources["viewerButtonsVisibility"] = editMode ? Visibility.Collapsed : Visibility.Visible;
            Resources["editorButtonsVisibility"] = editMode ? Visibility.Visible : Visibility.Collapsed;
            Resources["saveEnabled"] = AnyChangeMade;
        }
        private void btnDeleteMediumClick(object sender, RoutedEventArgs e) => DeleteMedium();
        private void btnEditMediumClick(object sender, RoutedEventArgs e) => EditMedium();
        private void btnUndoChangesClick(object sender, RoutedEventArgs e) => UndoChanges();
        private void btnSaveMediumClick(object sender, RoutedEventArgs e) => SaveMedium();
        #endregion
        private void editor_MediumEdited(Controls.Edit.MediumWithTags medium)
        {
            AnyChangeMade = true;
            updateVisibility(true);
        }
        #endregion

        #region Functions
        private bool save()
        {
            if (!validate()) return false;
            try
            {
                var data = editor.Medium;
                if (IsExistingMedium)
                {
                    CatalogContext.Writer.SaveMedium(new Medium
                    {
                        Id = MediumId,
                        CatalogId = data.CatalogId,
                        Title = data.Title,
                        Description = data.Description,
                        Location = data.Location
                    }, data.Tags);
                    // remove unused parts
                    var oldParts = GlobalContext.Reader.GetMedium(MediumId).Parts;
                    var newParts = data.Parts.Select(p => p.Id).Where(p => p >= 0).ToList();
                    oldParts.Where(p => !newParts.Contains(p.Id)).ToList().ForEach(p => GlobalContext.Writer.DeletePart(p.Id));
                    // save updated parts
                    data.Parts.Where(p => p.Id >= 0).ToList().ForEach(p => CatalogContext.Writer.SavePart(new Part
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
                    data.Parts.Where(p => p.Id < 0).ToList().ForEach(p => CatalogContext.Writer.CreatePart(new Part
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
                    MediumId = CatalogContext.Writer.CreateMedium(data.Title, data.Description, data.Location, data.Tags);
                    data.Parts.ForEach(p => CatalogContext.Writer.CreatePart(new Part
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ClosingPermitted()
        {
            if (editor.Visibility == Visibility.Collapsed) return true;

            var confirmation = !AnyChangeMade;
            if (!confirmation)
            {
                var result = ShowUnsavedChangesDialog();
                confirmation = result.HasValue;
                // if should save => try saving and cancel closing, if saving fails
                if (result.HasValue && result.Value && !save()) confirmation = false;
            }
            return confirmation;
        }
        private void Back() => Close(); // the confirmation is handled in the "Window_Closing" event
        private void DeleteMedium()
        {
            var performDeletion = !GlobalContext.Reader.GetCatalog(CatalogContext.CurrentCatalogId.Value).DeletionConfirmationMedium;
            if (!performDeletion)
            {
                var confirmation = ShowDeletionConfirmationDialog(getString("Menus.Edit.MediaDeletion"));
                performDeletion = confirmation.HasValue && confirmation.Value;
            }
            if (performDeletion)
            {
                if (IsExistingMedium) GlobalContext.Writer.DeleteMedium(MediumId);
                Close();
            }
        }
        private void EditMedium()
        {
            editor.Medium = viewer.Medium;
            updateVisibility(true);
        }
        private void UndoChanges()
        {
            if (!IsExistingMedium) Close();
            else
            {
                reloadData();
                AnyChangeMade = false;
                updateVisibility(false);
            }
        }
        private void SaveMedium()
        {
            if (save())
            {
                reloadData();
                AnyChangeMade = false;
                updateVisibility(false);
            }
        }
        #endregion
    }
}
