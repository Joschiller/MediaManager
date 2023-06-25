﻿using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using XMLImportExport;
using MediaManager.GUI.Dialogs;
using Microsoft.Win32;
using System;
using System.Windows;
using static MediaManager.Globals.DataConnector;
using static MediaManager.Globals.Navigation;

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
        public void RegisterAtLanguageProvider() => RegisterUnique(this);
        public void LoadTexts(string language)
        {
            Resources["btnAddCatalog"] = getString("Menus.Catalog.ToolTip.AddCatalog");
            Resources["btnImportCatalog"] = getString("Menus.Catalog.ToolTip.ImportCatalog");
            Resources["btnEditCatalog"] = getString("Menus.Catalog.ToolTip.EditCatalog");
            Resources["btnExportCatalog"] = getString("Menus.Catalog.ToolTip.ExportCatalog");
            Resources["btnDeleteCatalog"] = getString("Menus.Catalog.ToolTip.DeleteCatalog");
            Resources["btnSettings"] = getString("Menus.Catalog.ToolTip.Settings");
        }
        #endregion

        #region Handler
        #region Navbar
        private void NavigationBar_BackClicked(object sender, EventArgs e) => Close();
        private void NavigationBar_HelpClicked(object sender, EventArgs e) => OpenHelpMenu();
        private void btnAddCatalogClick(object sender, RoutedEventArgs e)
        {
            var result = new EditCatalogDialog(null).ShowDialog();
            if (result.HasValue && result.Value) catalogList.LoadCatalogs();
        }
        private string showLoadFileDialog()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = getString("Common.ExportFileType") + " (*" + ExportFileExtension + ")|*" + ExportFileExtension;
            ofd.ShowDialog();
            return ofd.FileName;
        }
        private void btnImportCatalogClick(object sender, RoutedEventArgs e)
        {
            var fileName = showLoadFileDialog();
            if (fileName == null || fileName == "") return;
            var viewer = new ThreadProcessViewer(
                new CatalogImportThread(
                    fileName,
                    getString("Dialog.Import.importFailedStep"),
                    getString("Dialog.Import.importFailedMessage"),
                    getString("Dialog.Import.formatExceptionHeader"),
                    getString("Dialog.Import.dbConstraintExceptionHeader"),
                    new System.Collections.Generic.Dictionary<string, string>()),
                new ThreadProcessViewerConfig
                {
                    Title = getString("Dialog.Import.DialogTitle"),
                    FinishButtonCaption = getString("Dialog.Import.FinishButton"),
                    Style = InternalThreadProcessViewerStyle
                });
            viewer.ProcessFailed += Viewer_ProcessFailed;
            viewer.ShowDialog();
            if (GlobalContext.Reader.Catalogs.Count == 1) CatalogContext.SetCurrentCatalog(GlobalContext.Reader.Catalogs[0]); // activate imported catalog if it is the only existing one
            catalogList.LoadCatalogs();
        }
        private string showSaveFileDialog(string catalogTitle)
        {
            var sfd = new SaveFileDialog();
            sfd.FileName = catalogTitle;
            sfd.Filter = getString("Common.ExportFileType") + " (*" + ExportFileExtension + ")|*" + ExportFileExtension;
            var res = sfd.ShowDialog();
            if (!res.HasValue || !res.Value) return "";
            return sfd.FileName;
        }
        private void btnExportCatalogClick(object sender, RoutedEventArgs e)
        {
            if (catalogList.SelectedCatalog == null) return;
            var fileName = showSaveFileDialog(catalogList.SelectedCatalog.Title);
            if (fileName == null || fileName == "") return;
            var viewer = new ThreadProcessViewer(
                new CatalogExportThread(
                    fileName,
                    getString("Dialog.Export.exportFailedStep"),
                    getString("Dialog.Export.exportFailedMessage"),
                    catalogList.SelectedCatalog.Id),
                new ThreadProcessViewerConfig
                {
                    Title = getString("Dialog.Export.DialogTitle"),
                    FinishButtonCaption = getString("Dialog.Export.FinishButton"),
                    Style = InternalThreadProcessViewerStyle
                });
            viewer.ProcessFailed += Viewer_ProcessFailed;
            viewer.ShowDialog();
        }
        private void Viewer_ProcessFailed(string message) => ShowDefaultDialog(message, Globals.DefaultDialogs.SuccessMode.Error);
        private void btnEditCatalogClick(object sender, RoutedEventArgs e)
        {
            var result = new EditCatalogDialog(catalogList.SelectedCatalog.Id).ShowDialog();
            if (result.HasValue && result.Value) catalogList.LoadCatalogs();
        }
        private void btnDeleteCatalogClick(object sender, RoutedEventArgs e)
        {
            if (catalogList.SelectedCatalog == null) return;

            // deletion confirmation must always be shown
            var confirmation = ShowDeletionConfirmationDialog(getString("Menus.Catalog.CatalogDeletion"));
            if (confirmation.HasValue && confirmation.Value)
            {
                GlobalContext.Writer.DeleteCatalog(catalogList.SelectedCatalog.Id);
                if (!CatalogContext.CurrentCatalogId.HasValue && GlobalContext.Reader.AnyCatalogExists) CatalogContext.SetCurrentCatalog(GlobalContext.Reader.Catalogs[0]);
                catalogList.LoadCatalogs();
            }
        }
        private void btnSettingsClick(object sender, RoutedEventArgs e) => OpenWindow(this, new SettingsMenu());
        #endregion
        private void catalogList_SelectionChanged(Catalog catalog) => Resources["catalogDependentVisibility"] = catalog == null ? Visibility.Collapsed : Visibility.Visible;
        private void catalogList_CatalogDoubleClick(Catalog catalog)
        {
            var oldCatalog = CatalogContext.CurrentCatalogId;
            CatalogContext.SetCurrentCatalog(catalog);
            if (GlobalContext.Settings.BackupEnabled)
            {
                if (oldCatalog.HasValue) RunBackgroundBackup(oldCatalog.Value);
                RunBackgroundBackup(catalog.Id);
            }
            catalogList.LoadCatalogs();
        }
        #endregion
    }
}
