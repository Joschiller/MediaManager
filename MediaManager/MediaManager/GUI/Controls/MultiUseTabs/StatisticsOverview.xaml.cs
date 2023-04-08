﻿using MediaManager.Globals.LanguageProvider;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static MediaManager.Globals.DataConnector.Reader;

namespace MediaManager.GUI.Controls.MultiUseTabs
{
    /// <summary>
    /// Interaction logic for StatisticsOverview.xaml
    /// </summary>
    public partial class StatisticsOverview : UserControl, MultiUseTabsControl, UpdatedLanguageUser
    {
        public StatisticsOverview()
        {
            InitializeComponent();
            Reload();
            RegisterAtLanguageProvider();
        }

        public bool Visible { get; set; } = true;

        public void Reload()
        {
            valueMediaCount.Text = CountOfMedia.ToString();
            valuePartCount.Text = CountOfParts.ToString();
        }

        public ImageSource GetHeader() => new BitmapImage(new Uri("/Resources/statistics.png", UriKind.Relative));
        public bool GetIsVisible() => Visible;
        public void RegisterAtLanguageProvider() => LanguageProvider.RegisterUnique(this);
        public void LoadTexts(string language)
        {
            labelMediaCount.Text = LanguageProvider.getString("Controls.MultiUseTabs.StatisticsOverview.CountMedia") + ":";
            labelPartCount.Text = LanguageProvider.getString("Controls.MultiUseTabs.StatisticsOverview.CountParts") + ":";
        }
    }
}
