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
    /// Interaction logic for OverviewMenu.xaml
    /// </summary>
    public partial class OverviewMenu : Window, UpdatedLanguageUser
    {
        public OverviewMenu()
        {
            InitializeComponent();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAdd"] = LanguageProvider.getString("Menus.Overview.Tooltip.Add");
            Resources["btnAddTag"] = LanguageProvider.getString("Menus.Overview.Tooltip.AddTag");
            Resources["btnTags"] = LanguageProvider.getString("Menus.Overview.Tooltip.Tags");
            Resources["btnCatalogs"] = LanguageProvider.getString("Menus.Overview.Tooltip.Catalogs");
            Resources["btnSettings"] = LanguageProvider.getString("Menus.Overview.Tooltip.Settings");
            Resources["btnAnalyze"] = LanguageProvider.getString("Menus.Overview.Tooltip.Analyze");
        }
    }
}
