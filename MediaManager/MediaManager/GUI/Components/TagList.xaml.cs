﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace MediaManager.GUI.Components
{
    /// <summary>
    /// Interaction logic for TagList.xaml
    /// </summary>
    public partial class TagList : UserControl
    {
        public delegate void TagValueChangeHandler(List<ValuedTag> tags);
        public event TagValueChangeHandler TagValueChanged;

        public ObservableCollection<ValuedTag> Tags { get; private set; } = new ObservableCollection<ValuedTag>();
        public void SetTagList(List<ValuedTag> tags)
        {
            Tags.Clear();
            tags.OrderBy(tag => tag.Tag.Title).ToList().ForEach(Tags.Add);
        }

        public TagList()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void TagCheckbox_TagValueChanged(Atoms.TagCheckbox sender, bool? newValue)
        {
            var changedTag = Tags.FirstOrDefault(t => t.Tag.Id == (int)sender.Tag);
            if (changedTag != null) changedTag.Value = newValue;
            TagValueChanged?.Invoke(Tags.ToList());
        }
    }
}