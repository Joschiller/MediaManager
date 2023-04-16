using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using static MediaManager.TagUtils;

namespace MediaManager.GUI.Components
{
    /// <summary>
    /// Interaction logic for TagList.xaml
    /// </summary>
    public partial class TagList : UserControl
    {
        public delegate void TagValueChangeHandler(List<ValuedTag> tags);
        public event TagValueChangeHandler TagValueChanged;

        public ObservableCollection<TagListElement> Tags { get; private set; } = new ObservableCollection<TagListElement>();
        public void SetTagList(List<ValuedTag> tags) => SetTagList(tags, new List<int>());
        public void SetTagList(List<ValuedTag> tags, List<int> disabledTagIds)
        {
            Tags.Clear();
            tags.OrderBy(tag => tag.Tag.Title).ToList().ForEach(t =>
            {
                Tags.Add(new TagListElement
                {
                    Tag = t.Tag,
                    Value = t.Value,
                    Icon = GetIconForTagValue(t.Value),
                    Enabled = !disabledTagIds.Contains(t.Tag.Id)
                });
            });
        }
        public List<ValuedTag> GetTagList()
        {
            var list = new List<ValuedTag>();
            Tags.ToList().ForEach(t =>
            {
                list.Add(new ValuedTag
                {
                    Tag = t.Tag,
                    Value = t.Value
                });
            });
            return list;
        }

        public TagList()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Grid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var tagId = (int)((Grid)sender).Tag;
            var changedTag = Tags.FirstOrDefault(t => t.Tag.Id == tagId);
            if (changedTag == null) return;

            var currentTags = GetTagList();
            var tagToEdit = currentTags.FirstOrDefault(t => t.Tag.Id == tagId);

            if (e.ChangedButton == MouseButton.Left)
            {
                if (!tagToEdit.Value.HasValue) tagToEdit.Value = true;
                else if (tagToEdit.Value.Value) tagToEdit.Value = false;
                else if (!tagToEdit.Value.Value) tagToEdit.Value = null;
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                if (!tagToEdit.Value.HasValue) tagToEdit.Value = false;
                else if (tagToEdit.Value.Value) tagToEdit.Value = null;
                else if (!tagToEdit.Value.Value) tagToEdit.Value = true;
            }

            SetTagList(currentTags);
            TagValueChanged?.Invoke(currentTags);
        }
    }
}
