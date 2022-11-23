using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace MediaManager.Globals.XMLImportExport
{
    /// <summary>
    /// A dialog with a header, progress bar and closing button that guides a <see cref="ThreadStepper"/> through it's process execution.
    /// <br/>
    /// The dialog shows the current progress in percent including the message broadcasted from the <see cref="ThreadStepper"/> and offers events to catch whenever the process finishes either successful (<see cref="ProcessSucceeded"/>) or not (<see cref="ProcessFailed"/>).
    /// <br/>
    /// The appearance of the dialog can be configured using the <see cref="ThreadProcessViewerConfig"/>. The dialog should be shown via the <see cref="Window.ShowDialog"/>-method.
    /// </summary>
    public partial class ThreadProcessViewer : Window
    {
        /// <summary>
        /// Handles that the process has failed.
        /// </summary>
        /// <param name="message">Status message of the failed process</param>
        public delegate void ProcessFailedHandler(string message);
        /// <summary>
        /// The process has failed.
        /// </summary>
        public event ProcessFailedHandler ProcessFailed;
        /// <summary>
        /// Handles that the process succeeded.
        /// </summary>
        public delegate void ProcessSucceededHandler();
        /// <summary>
        /// The process has succeeded.
        /// </summary>
        public event ProcessSucceededHandler ProcessSucceeded;

        /// <summary>
        /// Create a new <see cref="ThreadProcessViewer"/>.
        /// </summary>
        /// <param name="stepper"><see cref="ThreadStepper"/> to refer to in this dialog</param>
        /// <param name="config"><see cref="ThreadProcessViewerConfig"/> of the dialog</param>
        public ThreadProcessViewer(ThreadStepper stepper, ThreadProcessViewerConfig config)
        {
            InitializeComponent();
            DataContext = this;

            header.Text = config.Title;
            btnFinish.Content = config.FinishButtonCaption;

            Background = config.Style.Background;
            FontSize = config.Style.FontSize;
            FontFamily = config.Style.FontFamily;

            headerContainer.Background = config.Style.HeaderBackground;
            header.FontSize = config.Style.HeaderFontSize;
            header.FontFamily = config.Style.HeaderFontFamily;

            headerBorder.BorderBrush = config.Style.HeaderBorderColor;
            headerBorder.BorderThickness = new Thickness(config.Style.HeaderBorderThickness);

            var th = new Thread(new ThreadStart(stepper.run));
            stepper.StepDone += Stepper_StepDone;
            stepper.Finished += Stepper_Finished;
            th.Start();
        }

        #region Steps
        private void Stepper_StepDone(float percent, string message) => Dispatcher.Invoke(() => { progress.Value = percent; description.Text = message; });
        private void Stepper_Finished(Exception result) => Dispatcher.Invoke(() => { btnFinish.IsEnabled = true; if (result != null) ProcessFailed?.Invoke(result.Message); else ProcessSucceeded?.Invoke(); });
        #endregion
        #region Navigation
        private void btnFinish_Click(object sender, RoutedEventArgs e) => Close();
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => e.Cancel = e.Cancel || !btnFinish.IsEnabled;
        #endregion
    }
}
