﻿using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows;

namespace MediaManager.GUI.Menus
{
    /// <summary>
    /// Interaction logic for CatalogMenu.xaml
    /// </summary>
    public partial class CatalogMenu : Window, UpdatedLanguageUser
    {
        #region Setup
        public CatalogMenu()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
        }

        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddCatalog"] = LanguageProvider.getString("Menus.Catalog.Tooltip.AddCatalog");
            Resources["btnEditCatalog"] = LanguageProvider.getString("Menus.Catalog.Tooltip.EditCatalog");
            Resources["btnExportCatalog"] = LanguageProvider.getString("Menus.Catalog.Tooltip.ExportCatalog");
            Resources["btnDeleteCatalog"] = LanguageProvider.getString("Menus.Catalog.Tooltip.DeleteCatalog");
        }
        #endregion

        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnAddTagClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnExportCatalogClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnEditTagClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnDeleteTagClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
