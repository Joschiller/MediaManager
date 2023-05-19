using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaManager.Globals.SettingsEditor
{
    /// <summary>
    /// Renders all <see cref="UserControl"/>s that implement the <see cref="SettingsEditorItem"/> interface.
    /// <br/>
    /// The <see cref="SettingsEditorItem"/>s can be grouped by using the <see cref="SettingsEditorItem.GetTabName"/>.
    /// <br/>
    /// The <see cref="SettingsEditor"/> offers methods for the common loading, validation and saving of settings.
    /// </summary>
    public partial class SettingsEditor : UserControl
    {
        /// <summary>
        /// Currently rendered <see cref="SettingsEditorItem"/>s.
        /// </summary>
        private List<SettingsEditorItem> SettingsEditorItems = new List<SettingsEditorItem>();

        public SettingsEditor()
        {
            InitializeComponent();
            InstantiateAllElements();
        }

        /// <summary>
        /// Can be used to completely reload the rendered tabs.
        /// <br/>
        /// Afterwards <see cref="LoadData(int?)"/> should be run to correctly reload the data of all contained controls of the <see cref="SettingsEditor"/>.
        /// </summary>
        public void InstantiateAllElements()
        {
            var selectedTab = settingsTabs.SelectedIndex;
            settingsTabs.Items.Clear();
            SettingsEditorItems.Clear();

            var controlTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(UserControl).IsAssignableFrom(t) && typeof(SettingsEditorItem).IsAssignableFrom(t))
                .ToList();

            // instantiate
            foreach (var t in controlTypes)
            {
                var control = Activator.CreateInstance(t);
                var userControl = (UserControl)control;
                userControl.HorizontalAlignment = HorizontalAlignment.Stretch;
                userControl.VerticalAlignment = VerticalAlignment.Center;
                if (((SettingsEditorItem)control).IsVisible()) SettingsEditorItems.Add((SettingsEditorItem)control);
            }

            // render on tabs
            foreach (var tabName in SettingsEditorItems.Select(item => item.GetTabName()).Distinct())
            {
                var stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;
                SettingsEditorItems
                    .Where(item => item.GetTabName() == tabName)
                    .OrderBy(item => item.GetControlName())
                    .ToList()
                    .ForEach(item => stackPanel.Children.Add((UserControl)item));

                var scrollViewer = new ScrollViewer();
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                scrollViewer.Content = stackPanel;

                var tabitem = new TabItem();
                tabitem.Header = tabName;
                tabitem.Content = scrollViewer;

                settingsTabs.Items.Add(tabitem);
            }
            settingsTabs.SelectedIndex = settingsTabs.Items.Count > selectedTab ? selectedTab : 0;
        }

        /// <summary>
        /// Load the current settings into all rendered <see cref="SettingsEditorItem"/>s.
        /// </summary>
        /// <param name="accountIdentifier">Identifier of the current account</param>
        public void LoadData(int? identification)
        {
            SettingsEditorItems.ForEach(item => item.LoadData(identification));
        }
        /// <summary>
        /// Validate the current settings of all rendered <see cref="SettingsEditorItem"/>s.
        /// </summary>
        /// <returns>Whether all currently configured settings are valid.</returns>
        public bool ValidateData()
        {
            return SettingsEditorItems.All(item => item.ValidateData());
        }
        /// <summary>
        /// Save the current settings of all rendered <see cref="SettingsEditorItem"/>s.
        /// <br/>
        /// Before saving, the value should be validated with <see cref="ValidateData"/>.
        /// </summary>
        public void SaveData()
        {
            SettingsEditorItems.ForEach(item => item.SaveData());
        }
    }
}
