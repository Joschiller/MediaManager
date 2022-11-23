namespace MediaManager.Globals.XMLImportExport
{
    /// <summary>
    /// Configuration for a <see cref="ThreadProcessViewer"/>
    /// </summary>
    public class ThreadProcessViewerConfig
    {
        /// <summary>
        /// Title of the process dialog
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Caption of the process dialog (can contain mnemonics by using an underscore in front of the letter)
        /// </summary>
        public string FinishButtonCaption { get; set; }
        /// <summary>
        /// Style information of the process dialog
        /// </summary>
        public ThreadProcessViewerStyle Style { get; set; }
    }
}
