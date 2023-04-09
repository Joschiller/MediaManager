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

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for EditMenu.xaml
    /// </summary>
    public partial class EditMenu : Window, UpdatedLanguageUser
    {
        #region Setup
        public EditMenu(int? mediumId, int? partId)
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddPart"] = LanguageProvider.getString("Menus.Edit.Tooltip.AddPart");
            Resources["btnDeleteMedium"] = LanguageProvider.getString("Menus.Edit.Tooltip.DeleteMedium");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
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
            throw new NotImplementedException();
        }
        #endregion
    }
}
