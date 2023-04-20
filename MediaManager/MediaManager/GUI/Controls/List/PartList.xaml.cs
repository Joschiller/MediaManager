using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaManager.GUI.Controls.List
{
    /// <summary>
    /// Interaction logic for PartList.xaml
    /// </summary>
    public partial class PartList : UserControl
    {
        public delegate void SelectionHandler(int? id);
        public event SelectionHandler SelectionChanged;

        public ObservableCollection<PartListElement> Parts { get; private set; } = new ObservableCollection<PartListElement>();
        public void SetPartList(string mediaTitle, List<PartListElement> parts)
        {
            title.Text = mediaTitle;
            Parts.Clear();
            parts.OrderBy(part => part.Title).ToList().ForEach(Parts.Add);
        }
        public void SelectItem(int? id)
        {
            list.SelectedItem = Parts.FirstOrDefault(p => p.Id == id);
            //SelectionChanged?.Invoke(id);
        }

        public int? GetSelectedItem()
        {
            if (list.SelectedItem == null) return null;
            return (list.SelectedItem as PartListElement).Id;
        }

        public PartList()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            list.SelectedItem = null;
            SelectionChanged?.Invoke(null);
        }
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e) => SelectionChanged?.Invoke(GetSelectedItem());
    }
}
