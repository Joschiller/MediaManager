using DefaultDialogs;
using System;
using System.Windows;
using static LanguageProvider.LanguageProvider;
using static MediaManager.Globals.Navigation;

namespace MediaManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException; ;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var defaultEx = "An unexpected error occurred. Please close and restart the application!\nYou can take a screenshot to restore your data later on or to report the error.";
            try
            {
                var readEx = getString("Common.DispatcherUnhandledException");
                if (string.IsNullOrEmpty(readEx)) throw new Exception();
                ShowDefaultDialog(readEx, SuccessMode.Error);
            }
            catch (Exception)
            {
                ShowDefaultDialog(defaultEx, SuccessMode.Error);
            }
            e.Handled = true;
        }
    }
}
