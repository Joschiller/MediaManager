using System.Windows.Controls;

namespace MediaManager.Globals.SettingsEditor
{
    /// <summary>
    /// An item that will be rendered within the <see cref="SettingsEditor"/>.
    /// <br/>
    /// Implement this interface within any <see cref="UserControl"/> to let the <see cref="UserControl"/> be rendered within any generated instance of the <see cref="SettingsEditor"/>.
    /// </summary>
    public interface SettingsEditorItem
    {
        #region Data
        /// <summary>
        /// Load the settings data into the control.
        /// </summary>
        /// <param name="accountIdentifier">Identifier of the current account</param>
        void LoadData(int? accountIdentifier);
        /// <summary>
        /// Validate the current settings value.
        /// </summary>
        /// <returns>Whether the current value is valid</returns>
        bool ValidateData();
        /// <summary>
        /// Save the current settings value.
        /// <br/>
        /// Before saving, the value should be validated with <see cref="ValidateData"/> or via <see cref="SettingsEditor.ValidateData"/> of the containing <see cref="SettingsEditor"/>.
        /// </summary>
        void SaveData();
        #endregion
        #region Rendering
        /// <returns>The tab the <see cref="SettingsEditorItem"/> should be rendered within the <see cref="SettingsEditor"/>.</returns>
        string GetTabName();
        /// <returns>
        /// The control name of the <see cref="SettingsEditorItem"/>.
        /// <br/>
        /// This property can be used to sort the controls within the <see cref="SettingsEditor"/>.
        /// </returns>
        string GetControlName();
        /// <returns>Whether the <see cref="SettingsEditorItem"/> should be rendered.</returns>
        bool IsVisible();
        #endregion
    }
}
