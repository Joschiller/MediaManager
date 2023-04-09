using MediaManager.Globals.LanguageProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MediaManager.Globals.DataConnector.Writer;
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
        private int? SelectedPartId;

        public EditMenu(int? mediumId, int? partId)
        {
            InitializeComponent();
            RegisterAtLanguageProvider();

            // TODO: assign mediumId to MediumId OR create medium and use generated id
            // MediumId = mediumId ??
            SelectedPartId = mediumId != null ? partId : null;
            // TODO: select part or "select" medium to show it's information
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddPart"] = LanguageProvider.getString("Menus.Edit.Tooltip.AddPart");
            Resources["btnDeleteMedium"] = LanguageProvider.getString("Menus.Edit.Tooltip.DeleteMedium");
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

            // there are possibly unsaved changes
            var confirmation = ShowUnsavedChangesDialog();
            if (confirmation.HasValue)
            {
                if (confirmation.Value) editor.saveChanges();
                Close();
            }
        }
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnAddPartClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnDeleteMediumClick(object sender, RoutedEventArgs e)
        {
            var confirmation = ShowDeletionConfirmationDialog(LanguageProvider.getString("Controls.Edit.MediaDeletion"));
            if (confirmation.HasValue && confirmation.Value)
            {
                DeleteMedium(MediumId);
                Close();
            }
        }
        #endregion

        private void viewer_EditClicked(Controls.Edit.ElementMode mode, int id)
        {
            viewer.Visibility = Visibility.Collapsed;
            editor.Visibility = Visibility.Visible;
            editor.LoadElement(mode, id);
        }
        private void viewer_DeleteClicked(Controls.Edit.ElementMode mode, int id)
        {
            if (mode == Controls.Edit.ElementMode.Medium) Close();
            else
            {
                throw new NotImplementedException();
                // TODO: unselect current element: SelectedPartId = null;
            }
        }
        private void editor_QuitEditing(Controls.Edit.ElementMode mode, int id)
        {
            viewer.Visibility = Visibility.Visible;
            editor.Visibility = Visibility.Collapsed;
            viewer.LoadElement(mode, id);
            if (mode == Controls.Edit.ElementMode.Medium)
            {
                throw new NotImplementedException();
                // TODO: reload medium title
            }
            else
            {
                throw new NotImplementedException();
                // TODO: reload part list and re-select current part
            }
        }
    }
}
