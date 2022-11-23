using MediaManager.Globals.DefaultDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace MediaManager.Globals
{
    public static class Navigation
    {
        // TODO: these values must be adjusted
        public static GeneralButtonBasedDialogStyle GeneralButtonBasedDialogStyle = new GeneralButtonBasedDialogStyle
        {
            HeaderBackground = new System.Windows.Media.SolidColorBrush(Color.FromRgb(255, 255, 255)),
            Background = new System.Windows.Media.SolidColorBrush(Color.FromRgb(255, 255, 255)),
            HeaderFontFamily = new FontFamily("Consolas"),
            FontFamily = new FontFamily("Consolas"),
            HeaderFontSize = 20,
            FontSize = 14,
            HeaderBorderColor = new System.Windows.Media.SolidColorBrush(Color.FromRgb(0, 0, 0)),
            HeaderBorderThickness = 1,
            NegativeColor = Color.FromRgb(255, 0, 0),
            NeutralColor = Color.FromRgb(150, 150, 150),
            PositiveColor = Color.FromRgb(0, 255, 0),
            CancelButtonBackground = Color.FromRgb(255, 0, 0)
        };
        public static void OpenWindow(Window w1, Window w2, Action doOnClose = null)
        {
            w1.Hide();
            w2.Closing += (s, e) => { if (doOnClose == null) w1.Show(); else doOnClose(); };
            w2.ShowDialog();
        }
        public static void ShowDefaultDialog(string text, SuccessMode mode = SuccessMode.Neutral, string language = null)
        {
            language = language ?? LanguageProvider.LanguageProvider.CurrentLanguage;
            new GeneralButtonBasedDialog(GeneralButtonBasedDialogStyle)
                .WithTitle(LanguageProvider.LanguageProvider.getString(language, "ApplicationName"))
                .WithText(text)
                .WithBorder(mode)
                .WithNeutralButton("_" + LanguageProvider.LanguageProvider.getString(language, "Common.Button.Ok"), isDefault: true, isCancel: true)
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
                .WithTitle(LanguageProvider.LanguageProvider.getString("ApplicationName"))
                .WithText(LanguageProvider.LanguageProvider.getString("Dialog.UnsavedChanges"))
                .WithBorder(SuccessMode.Error)
                .WithDefaultButton("_" + LanguageProvider.LanguageProvider.getString("Common.Button.Yes"), true)
                .WithNeutralButton("_" + LanguageProvider.LanguageProvider.getString("Common.Button.No"), false)
                .WithCancelButton("_" + LanguageProvider.LanguageProvider.getString("Common.Button.Cancel"), null)
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
                .WithTitle(LanguageProvider.LanguageProvider.getString("ApplicationName"))
                .WithText(string.Format(LanguageProvider.LanguageProvider.getString("Dialog.DeletionConfirmation.Intro") + (additionalHint == "" ? "" : ("\n" + additionalHint)), deletedObjectRepresentation))
                .WithBorder(SuccessMode.Error)
                .WithDefaultButton("_" + LanguageProvider.LanguageProvider.getString("Common.Button.Yes"), true)
                .WithCancelButton("_" + LanguageProvider.LanguageProvider.getString("Common.Button.No"), false)
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
            language = language ?? LanguageProvider.LanguageProvider.CurrentLanguage;
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
                else if (problems.Count > 0) sb.Append(LanguageProvider.LanguageProvider.getString(language, "Common.ValidationText.Concat"));
            }
            if (sb.Length > 0) ShowDefaultDialog(LanguageProvider.LanguageProvider.getString(language, "Common.ValidationText.Intro") + sb.ToString() + "!", SuccessMode.Error, language);
            return sb.Length == 0;
        }
    }
}
