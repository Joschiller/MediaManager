﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MediaManager.GUI.Controls.Analyze
{
    /// <summary>
    /// Interaction logic for AnalyzeList.xaml
    /// </summary>
    public partial class AnalyzeList : UserControl
    {
        public delegate void AnalyzeElementHandler(AnalyzeListElement element);
        public event AnalyzeElementHandler SelectionChanged;

        public ObservableCollection<AnalyzeListElement> Items { get; set; } = new ObservableCollection<AnalyzeListElement>();

        public void setItems(List<AnalyzeListElement> items)
        {
            Items.Clear();
            items.ForEach(Items.Add);
        }

        public AnalyzeList()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) => SelectionChanged?.Invoke(((ListView)sender).SelectedItem as AnalyzeListElement);
    }
}