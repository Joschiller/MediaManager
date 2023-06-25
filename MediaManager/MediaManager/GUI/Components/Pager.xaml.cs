﻿using LanguageProvider;
using static LanguageProvider.LanguageProvider;
using System.Windows;
using System.Windows.Controls;

namespace MediaManager.GUI.Components
{
    /// <summary>
    /// Interaction logic for Pager.xaml
    /// </summary>
    public partial class Pager : UserControl, UpdatedLanguageUser
    {
        #region Events
        public delegate void PageChangeHandler(int newPage);
        public event PageChangeHandler PageChanged;
        #endregion

        #region Properties
        private int _CurrentPage = 1;
        private int _TotalPages = 1;
        public int CurrentPage
        {
            get => _CurrentPage; set
            {
                _CurrentPage = value > TotalPages ? TotalPages : (value < 1 ? 1 : value);
                UpdateGUI();
                PageChanged?.Invoke(_CurrentPage);
            }
        }
        public int TotalPages
        {
            get => _TotalPages; set
            {
                _TotalPages = value < 1 ? 1 : value;
                if (CurrentPage > value) CurrentPage = value;
                UpdateGUI();
            }
        }
        /// <summary>
        /// Placement of the page and item counter within the control.<br/>
        /// By default, <see cref="Dock.Top"/> will be used.
        /// </summary>
        public Dock TextPlacement { get => DockPanel.GetDock(textPanel); set => DockPanel.SetDock(textPanel, value); }
        #endregion

        #region Setup
        public Pager()
        {
            InitializeComponent();
            RegisterAtLanguageProvider();
            UpdateGUI();
        }
        public void RegisterAtLanguageProvider() => Register(this);
        public void LoadTexts(string language)
        {
            prev.ToolTip = getString("Component.Pager.Previous");
            next.ToolTip = getString("Component.Pager.Next");
        }
        ~Pager() => Unregister(this);
        private void UpdateGUI()
        {
            page.Text = CurrentPage.ToString() + "/" + TotalPages.ToString();
            prev.IsEnabled = CurrentPage > 1;
            next.IsEnabled = CurrentPage < TotalPages;
        }
        #endregion

        #region Getter/Setter
        public void setItemCount(int count)
        {
            itemCount.Visibility = Visibility.Visible;
            itemCount.Text = count.ToString();
        }
        #endregion

        #region Handler
        private void prev_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage--;
            UpdateGUI();
        }
        private void next_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage++;
            UpdateGUI();
        }
        #endregion
    }
}
