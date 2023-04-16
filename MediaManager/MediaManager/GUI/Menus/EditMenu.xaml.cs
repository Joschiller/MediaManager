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
        private int? SelectedPartId;

        public EditMenu(int? mediumId, int? partId)
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
            updateElementChangeOptions(true);

            MediumId = mediumId ?? Writer.CreateMedium();
            SelectedPartId = mediumId != null ? partId : null;
            reloadData();
            if (!mediumId.HasValue) startEditing(); // directly edit new medium
        }
        private void reloadData()
        {
            var medium = Reader.GetMedium(MediumId);
            list.SetPartList(medium.Title, medium.Parts.Select(m => new Controls.List.PartListElement
            {
                Id = m.Id,
                Title = m.Title
            }).ToList());
            list.SelectItem(SelectedPartId);
        }
        private void updateElementChangeOptions(bool available)
        {
            Resources["btnAddPartVisible"] = available ? Visibility.Visible : Visibility.Collapsed;
            list.IsEnabled = available;
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddPart"] = LanguageProvider.getString("Menus.Edit.ToolTip.AddPart");
            Resources["btnDeleteMedium"] = LanguageProvider.getString("Menus.Edit.ToolTip.DeleteMedium");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e)
        {
            if (editor.Visibility == Visibility.Collapsed)
            {
                Close();
                return;
            }

            // there are possibly unsaved changes => ask user
            var confirmation = ShowUnsavedChangesDialog();
            // no value => discard
            // false => close
            // true => try saving => close, if successful
            if (confirmation.HasValue && (!confirmation.Value || (confirmation.Value && editor.saveChanges())))
            {
                Close();
                Writer.CleanupEmptyMediaAndParts();
            }
        }
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnAddPartClick(object sender, RoutedEventArgs e)
        {
            var newPartId = Writer.CreatePart(MediumId);
            reloadData();
            list.SelectItem(newPartId);
            startEditing();
        }
        private void btnDeleteMediumClick(object sender, RoutedEventArgs e)
        {
            var performDeletion = !CURRENT_CATALOGUE.DeletionConfirmationMedium;
            if (!performDeletion)
            {
                var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString("Controls.Edit.MediaDeletion"));
                performDeletion = confirmation.HasValue && confirmation.Value;
            }
            if (performDeletion)
            {
                Writer.DeleteMedium(MediumId);
                Close();
            }
        }
        #endregion

        #region Change View
        private void list_SelectionChanged(int? id)
        {
            SelectedPartId = id;
            viewer.Visibility = Visibility.Visible;
            editor.Visibility = Visibility.Collapsed;
            viewer.LoadElement(
                SelectedPartId.HasValue ? Controls.Edit.ElementMode.Part : Controls.Edit.ElementMode.Medium,
                SelectedPartId.HasValue ? SelectedPartId.Value : MediumId
                );
        }
        private void startEditing()
        {
            viewer.Visibility = Visibility.Collapsed;
            editor.Visibility = Visibility.Visible;
            editor.LoadElement(
                SelectedPartId.HasValue ? Controls.Edit.ElementMode.Part : Controls.Edit.ElementMode.Medium,
                SelectedPartId.HasValue ? SelectedPartId.Value : MediumId
                );
            updateElementChangeOptions(false);
        }

        private void viewer_EditClicked(Controls.Edit.ElementMode mode, int id)
        {
            viewer.Visibility = Visibility.Collapsed;
            editor.Visibility = Visibility.Visible;
            editor.LoadElement(mode, id);
            updateElementChangeOptions(false);
        }
        private void viewer_DeleteClicked(Controls.Edit.ElementMode mode, int id)
        {
            if (mode == Controls.Edit.ElementMode.Medium) Close();
            else
            {
                list.SelectItem(null);
                reloadData();
            }
            updateElementChangeOptions(true);
        }
        private void editor_QuitEditing(Controls.Edit.ElementMode mode, int id)
        {
            viewer.Visibility = Visibility.Visible;
            editor.Visibility = Visibility.Collapsed;
            viewer.LoadElement(mode, id);
            var medium = Reader.GetMedium(MediumId);
            reloadData();
            updateElementChangeOptions(true);
        }
        #endregion
    }
}
