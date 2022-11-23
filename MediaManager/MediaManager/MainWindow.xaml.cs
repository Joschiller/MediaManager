using System.Collections.Generic;
using System.Windows;

namespace MediaManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Globals.LanguageProvider.LanguageProvider.ConfigureLanguages(new Dictionary<string, byte[]>
            {
                { "English", Properties.Resources.English },
                { "Deutsch", Properties.Resources.Deutsch }
            }, "English");
            InitializeComponent();
        }
    }
}
