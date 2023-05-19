using System.Windows.Media;

namespace MediaManager.Globals.XMLImportExport
{
    /// <summary>
    /// Configurable style information for a <see cref="ThreadProcessViewer"/>
    /// </summary>
    public class ThreadProcessViewerStyle
    {
        /// <summary>
        /// Background color of the dialog header
        /// </summary>
        public SolidColorBrush HeaderBackground { get; set; }
        /// <summary>
        /// Background color of the dialog content
        /// </summary>
        public SolidColorBrush Background { get; set; }
        /// <summary>
        /// Font family of the dialog header
        /// </summary>
        public FontFamily HeaderFontFamily { get; set; }
        /// <summary>
        /// Font family of the dialog content
        /// </summary>
        public FontFamily FontFamily { get; set; }
        /// <summary>
        /// Font size of the dialog header
        /// </summary>
        public int HeaderFontSize { get; set; }
        /// <summary>
        /// Font size of the dialog content
        /// </summary>
        public int FontSize { get; set; }
        /// <summary>
        /// Color of the border between the header and the content
        /// </summary>
        public SolidColorBrush HeaderBorderColor { get; set; }
        /// <summary>
        /// Thickness of the border between the header and the content
        /// </summary>
        public int HeaderBorderThickness { get; set; } = 1;
    }
}
