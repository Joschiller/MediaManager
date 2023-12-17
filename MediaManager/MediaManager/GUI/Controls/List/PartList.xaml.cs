using System.Collections.Generic;
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
        #region Events
        public delegate void SelectionHandler(int? id);
        public event SelectionHandler SelectionChanged;
        #endregion

        #region Setup
        public PartList()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Getter/Setter
        public void SetPartList(string mediaTitle, List<PartListElement> parts)
        {
            title.Text = mediaTitle;
            list.ItemsSource = parts.OrderBy(part => part.Title).ToList();
        }
        public void SelectItem(int? id)
        {
            list.SelectedItem = (list.ItemsSource as List<PartListElement>).FirstOrDefault(p => p.Id == id);
            //SelectionChanged?.Invoke(id);
        }

        public int? GetSelectedItem()
        {
            if (list.SelectedItem == null) return null;
            return (list.SelectedItem as PartListElement).Id;
        }
        #endregion

        #region Handler
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            list.SelectedItem = null;
            SelectionChanged?.Invoke(null);
        }
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e) => SelectionChanged?.Invoke(GetSelectedItem());
        #endregion
    }
}
