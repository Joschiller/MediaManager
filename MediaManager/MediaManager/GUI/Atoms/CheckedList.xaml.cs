using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaManager.GUI.Atoms
{
    /// <summary>
    /// Interaction logic for CheckedList.xaml
    /// </summary>
    public partial class CheckedList : UserControl
    {
        #region Events
        public delegate void CheckedListItemHandler(CheckedListItem item);
        public event CheckedListItemHandler SelectionChanged;
        public delegate void CheckedListItemListHandler(List<CheckedListItem> item);
        public event CheckedListItemListHandler CheckedChanged;
        #endregion

        #region Bindings
        public ObservableCollection<CheckedListItem> Items { get; set; } = new ObservableCollection<CheckedListItem>();
        private List<int> checkedItemIds = new List<int>();
        #endregion

        #region Setup
        public CheckedList()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Getter/Setter
        public void SetItems(List<CheckedListItem> items)
        {
            Items.Clear();
            checkedItemIds.Clear();
            items.ForEach(Items.Add);
        }
        public List<CheckedListItem> GetCheckedItems() => Items.Where(i => checkedItemIds.Contains(i.Id)).ToList();
        #endregion

        #region Handler
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            checkedItemIds.Add((int)((CheckBox)sender).Tag);
            CheckedChanged?.Invoke(GetCheckedItems());
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            checkedItemIds.RemoveAll(i => i == (int)((CheckBox)sender).Tag);
            CheckedChanged?.Invoke(GetCheckedItems());
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) => SelectionChanged?.Invoke((CheckedListItem)((ListView)sender).SelectedItem);
        #endregion
    }
}
