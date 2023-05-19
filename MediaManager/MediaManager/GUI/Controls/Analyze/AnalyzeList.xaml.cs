using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MediaManager.GUI.Controls.Analyze
{
    /// <summary>
    /// Interaction logic for AnalyzeList.xaml
    /// </summary>
    public partial class AnalyzeList : UserControl
    {
        #region Events
        public delegate void AnalyzeElementHandler(AnalyzeListElement element);
        public event AnalyzeElementHandler SelectionChanged;
        #endregion

        #region Bindings
        public ObservableCollection<AnalyzeListElement> Items { get; set; } = new ObservableCollection<AnalyzeListElement>();
        #endregion

        #region Setup
        public AnalyzeList()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Getter/Setter
        public void SetItems(List<AnalyzeListElement> items)
        {
            Items.Clear();
            items.ForEach(Items.Add);
            if (items.Count > 0) listView.SelectedIndex = 0;
        }
        #endregion

        #region Handler
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e) => SelectionChanged?.Invoke(((ListView)sender).SelectedItem as AnalyzeListElement);
        #endregion
    }
}
