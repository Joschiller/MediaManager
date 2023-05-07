using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static MediaManager.Globals.DataConnector;

namespace MediaManager.GUI.Controls.MultiUseTabs
{
    /// <summary>
    /// Interaction logic for StatisticsOverview.xaml
    /// </summary>
    public partial class StatisticsOverview : UserControl, MultiUseTabsControl, UpdatedLanguageUser
    {
        #region Setup
        public StatisticsOverview()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
            ReloadGUI();
        }
        public void RegisterAtLanguageProvider() => LanguageProvider.Register(this);
        public void LoadTexts(string language)
        {
            labelMediaCount.Text = LanguageProvider.getString("Controls.MultiUseTabs.StatisticsOverview.CountMedia") + ":";
            labelPartCount.Text = LanguageProvider.getString("Controls.MultiUseTabs.StatisticsOverview.CountParts") + ":";
        }
        ~StatisticsOverview() => LanguageProvider.Unregister(this);
        public ImageSource GetHeader() => new BitmapImage(new Uri("/Resources/statistics.png", UriKind.Relative));
        public bool GetIsVisible() => GlobalContext.Settings.StatisticsOverviewVisible;
        public void ReloadGUI()
        {
            valueMediaCount.Text = CatalogContext.Reader.Statistics.CountOfMedia.ToString();
            valuePartCount.Text = CatalogContext.Reader.Statistics.CountOfParts.ToString();
        }
        #endregion
    }
}
