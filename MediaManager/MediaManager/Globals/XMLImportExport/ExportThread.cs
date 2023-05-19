using System;

namespace MediaManager.Globals.XMLImportExport
{
    /// <summary>
    /// A default <see cref="ThreadStepper"/> for an export of data.
    /// </summary>
    public abstract class ExportThread : ThreadStepper
    {
        protected string fileDestination;
        protected string exportFailedStep;
        protected string exportFailedMessage;
        private ExportThread() { }
        /// <summary>
        /// Create a new <see cref="ExportThread"/>.
        /// </summary>
        /// <param name="fileDestination">File to write to</param>
        /// <param name="exportFailedStep">Caption to show in a <see cref="ThreadProcessViewer"/> if the export fails</param>
        /// <param name="exportFailedMessage">Message of the exception to throw if the export fails</param>
        protected ExportThread(string fileDestination, string exportFailedStep, string exportFailedMessage)
        {
            this.fileDestination = fileDestination;
            this.exportFailedStep = exportFailedStep;
            this.exportFailedMessage = exportFailedMessage;
        }
        public override void run()
        {
            try
            {
                runExport();
            }
            catch (Exception e)
            {
                CallStep(1 / 1, exportFailedStep);
                CallFinished(new Exception(exportFailedMessage, e));
            }
        }
        protected abstract void runExport();
    }
}
