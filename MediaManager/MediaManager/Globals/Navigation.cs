using static LanguageProvider.LanguageProvider;
using MediaManager.Globals.DefaultDialogs;
using MediaManager.Globals.XMLImportExport;
using MediaManager.GUI.Menus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace MediaManager.Globals
{
    public static class Navigation
    {
        public static string DefaultBackupPath { get => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MediaManager"; }
        public static string ExportFileExtension { get => ".mmf.xml"; }

        public static Window CurrentHelpMenu;
        public static void OpenHelpMenu()
        {
            if (CurrentHelpMenu == null)
            {
                CurrentHelpMenu = new HelpMenu();
                CurrentHelpMenu.Closing += CurrentHelpMenu_Closing;
            }
            CurrentHelpMenu.Show();
            CurrentHelpMenu.Focus();
        }
        private static void CurrentHelpMenu_Closing(object sender, System.ComponentModel.CancelEventArgs e) => CurrentHelpMenu = null;

        public static GeneralButtonBasedDialogStyle GeneralButtonBasedDialogStyle = new GeneralButtonBasedDialogStyle
        {
            HeaderBackground = new System.Windows.Media.SolidColorBrush(Color.FromRgb(88, 88, 89)),
            Background = new System.Windows.Media.SolidColorBrush(Color.FromRgb(247, 249, 250)),
            HeaderFontFamily = new FontFamily("Consolas"),
            FontFamily = new FontFamily("Consolas"),
            HeaderFontSize = 25,
            FontSize = 16,
            HeaderBorderColor = new System.Windows.Media.SolidColorBrush(Color.FromRgb(20, 20, 20)),
            HeaderBorderThickness = 1,
            NegativeColor = Color.FromRgb(229, 103, 34),
            NeutralColor = Color.FromRgb(215, 232, 255),
            PositiveColor = Color.FromRgb(118, 221, 0),
            CancelButtonBackground = Color.FromRgb(229, 103, 34),
            ButtonMargin = new Thickness(4)
        };
        public static ThreadProcessViewerStyle InternalThreadProcessViewerStyle = new ThreadProcessViewerStyle
        {
            HeaderBackground = GeneralButtonBasedDialogStyle.HeaderBackground,
            Background = GeneralButtonBasedDialogStyle.Background,
            HeaderFontFamily = GeneralButtonBasedDialogStyle.HeaderFontFamily,
            FontFamily = GeneralButtonBasedDialogStyle.FontFamily,
            HeaderFontSize = GeneralButtonBasedDialogStyle.HeaderFontSize,
            FontSize = GeneralButtonBasedDialogStyle.FontSize,
            HeaderBorderColor = GeneralButtonBasedDialogStyle.HeaderBorderColor,
            HeaderBorderThickness = GeneralButtonBasedDialogStyle.HeaderBorderThickness
        };
        public static void OpenWindow(Window w1, Window w2, Action doOnClose = null)
        {
            w1.Hide();
            w2.Closing += (s, e) => { if (doOnClose == null) w1.Show(); else doOnClose(); };
            w2.ShowDialog();
        }
        public static void ShowDefaultDialog(string text, SuccessMode mode = SuccessMode.Neutral, string language = null)
        {
            language = language ?? CurrentLanguage;
            new GeneralButtonBasedDialog(GeneralButtonBasedDialogStyle)
                .WithTitle(getString(language, "ApplicationName"))
                .WithText(text)
                .WithBorder(mode)
                .WithNeutralButton("_" + getString(language, "Common.Button.Ok"), isDefault: true, isCancel: true)
                .ShowDialog();
        }
        /// <summary>
        /// Shows a dialog that asks wheather the unsaved changes should be stored.
        /// </summary>
        /// <returns>
        /// Wheather the unsaved changes should be stored.
        /// <br/>
        /// - true: save and continue
        /// <br/>
        /// - false: discard and continue
        /// <br/>
        /// - null: cancel navigation
        /// </returns>
        public static bool? ShowUnsavedChangesDialog()
        {
            return new GeneralButtonBasedDialog(GeneralButtonBasedDialogStyle)
                .WithTitle(getString("ApplicationName"))
                .WithText(getString("Dialog.UnsavedChanges"))
                .WithBorder(SuccessMode.Error)
                .WithDefaultButton("_" + getString("Common.Button.Yes"), true)
                .WithNeutralButton("_" + getString("Common.Button.No"), false)
                .WithCancelButton("_" + getString("Common.Button.Cancel"), null)
                .ShowForResult() as bool?;
        }
        /// <summary>
        /// Shows a dialog that asks wheather the deletion should be continued.
        /// </summary>
        /// <param name="deletedObjectRepresentation">String representation of the object to delete</param>
        /// <param name="additionalHint">Optional additional string that provides more detail for the requested deletion</param>
        /// <returns>
        /// Wheather the deletion should continued.
        /// <br/>
        /// - true: delete
        /// <br/>
        /// - false: cancel deletion
        /// </returns>
        public static bool? ShowDeletionConfirmationDialog(string deletedObjectRepresentation, string additionalHint = "")
        {
            return new GeneralButtonBasedDialog(GeneralButtonBasedDialogStyle)
                .WithTitle(getString("ApplicationName"))
                .WithText(string.Format(getString("Dialog.DeletionConfirmation.Intro") + (additionalHint == "" ? "" : ("\n" + additionalHint)), deletedObjectRepresentation))
                .WithBorder(SuccessMode.Error)
                .WithDefaultButton("_" + getString("Common.Button.Yes"), true)
                .WithCancelButton("_" + getString("Common.Button.No"), false)
                .ShowForResult() as bool?;
        }
        /// <summary>
        /// Runs a list of validations and prompts the result if it is negative.
        /// </summary>
        /// <param name="validations">List of validations that return null of they are successful or a string otherwise</param>
        /// <param name="language">Optional parameter for a language to be used when loading the dialog contents.</param>
        /// <returns>true, if all validations succeeded, false either</returns>
        public static bool RunValidation(List<Func<string>> validations, string language = null)
        {
            language = language ?? CurrentLanguage;
            var problems = new Queue<string>();
            foreach (var v in validations)
            {
                var res = v();
                if (res != null) problems.Enqueue(res);
            }
            var sb = new StringBuilder();
            while (problems.Count > 0)
            {
                sb.Append(problems.Dequeue());
                if (problems.Count > 1) sb.Append(", ");
                else if (problems.Count > 0) sb.Append(getString(language, "Common.ValidationText.Concat"));
            }
            if (sb.Length > 0) ShowDefaultDialog(getString(language, "Common.ValidationText.Intro") + sb.ToString() + "!", SuccessMode.Error, language);
            return sb.Length == 0;
        }
    }
}
