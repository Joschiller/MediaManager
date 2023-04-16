using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        public void SetTagList(List<ValuedTag> tags)
        {
            Tags.Clear();
            tags.OrderBy(tag => tag.Tag.Title).ToList().ForEach(t =>
            {
                Tags.Add(new TagListElement
                {
                    Tag = t.Tag,
                    Value = t.Value,
                    Icon = GetIconForTagValue(t.Value)
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

        private ImageSource GetIconForTagValue(bool? value)
        {
            if (!value.HasValue) return new BitmapImage(new Uri("/Resources/checkbox_neutral.png", UriKind.Relative));
            else if (value.Value) return new BitmapImage(new Uri("/Resources/checkbox_positive.png", UriKind.Relative));
            else return new BitmapImage(new Uri("/Resources/checkbox_negative.png", UriKind.Relative));
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
