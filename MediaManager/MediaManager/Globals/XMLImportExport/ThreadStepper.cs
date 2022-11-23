using System;

namespace MediaManager.Globals.XMLImportExport
{
    /// <summary>
    /// The <see cref="ThreadStepper"/> runs a specific process using <see cref="run"/> that is guided by progress events using <see cref="CallStep(float, string)"/> and <see cref="CallFinished(Exception)"/>.
    /// <br/>
    /// The events triggered by those methods can be accessed using <see cref="StepDone"/> and <see cref="Finished"/> and can be visualized using a <see cref="ThreadProcessViewer"/>.
    /// </summary>
    public abstract class ThreadStepper
    {
        /// <summary>
        /// Handles the completion of a step.
        /// </summary>
        /// <param name="percentage">Completion percentage</param>
        /// <param name="message">Message describing the step</param>
        public delegate void StepHandler(float percentage, string message);
        /// <summary>
        /// A step of the current process was finished.
        /// </summary>
        public event StepHandler StepDone;
        /// <summary>
        /// Handles the completion of the overall process.
        /// </summary>
        /// <param name="result">null if the process was successfully finished; an exception otherwise</param>
        public delegate void ResultHandler(Exception result);
        /// <summary>
        /// The overall process was finished.
        /// </summary>
        public event ResultHandler Finished;
        /// <summary>
        /// Broadcasts the current process to all subscribers which can be shown in a <see cref="ThreadProcessViewer"/>.
        /// </summary>
        /// <param name="percentage">Current progress</param>
        /// <param name="message">Current status</param>
        protected void CallStep(float percentage, string message) { StepDone?.Invoke(percentage, message); }
        /// <summary>
        /// Broadcasts, that the process was finished which can be shown in a <see cref="ThreadProcessViewer"/>.
        /// </summary>
        /// <param name="exc">null if the process was successfully finished; an exception otherwise</param>
        protected void CallFinished(Exception exc = null) { Finished?.Invoke(exc); }
        /// <summary>
        /// Start the execution of the defined process.
        /// </summary>
        public abstract void run();
    }
}
