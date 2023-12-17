using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MediaManager.Globals
{
    public class KeyboardShortcutHelper
    {
        /// <summary>
        /// Analyzes the given event to check, if it matches any of the given keyboard shortcuts. If so, a given action will be executed.<br/>
        /// If called in the <c>Window_PreviewKeyDown</c> listener, all keyboard shortcuts within that window can be handled centrally.
        /// </summary>
        /// <param name="e">Event to analyze</param>
        /// <param name="keyboardShortcuts">Configuration for keyboard shortcuts to recognize</param>
        /// <param name="setHandled">Determines, whether the caught event will be set to "Handled" - set this to false if this must be determined individually</param>
        public static void runKeyboardShortcut(System.Windows.Input.KeyEventArgs e, Dictionary<(ModifierKeys Modifiers, Key Key), Action> keyboardShortcuts, bool setHandled = true)
        {
            var shortcut = keyboardShortcuts.Keys.FirstOrDefault(key => key.Modifiers == Keyboard.Modifiers && key.Key == e.Key);
            if (keyboardShortcuts.ContainsKey(shortcut))
            {
                keyboardShortcuts[shortcut]?.Invoke();
                e.Handled = setHandled;
            }
        }
    }
}
