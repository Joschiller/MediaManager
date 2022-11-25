using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MediaManager.GUI.Atoms
{
    /// <summary>
    /// Interaction logic for TagCheckbox.xaml
    /// </summary>
    public partial class TagCheckbox : UserControl
    {
        public delegate void TagValueChangeHandler(TagCheckbox sender, bool? newValue);
        public event TagValueChangeHandler TagValueChanged;

        public string TagName
        {
            get => (string)GetValue(TagNameProperty);
            set => SetValue(TagNameProperty, value);
        }
        public static readonly DependencyProperty TagNameProperty = DependencyProperty.Register("TagName", typeof(string), typeof(TagCheckbox));
        public bool? Value
        {
            get => (bool?)GetValue(TagValueProperty);
            set => SetValue(TagValueProperty, value);
        }
        public static readonly DependencyProperty TagValueProperty = DependencyProperty.Register("Value", typeof(bool?), typeof(TagCheckbox), new PropertyMetadata(null, OnTagValuePropertyChanged));
        public static void OnTagValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (bool?)e.NewValue;
            if (!value.HasValue) ((TagCheckbox)d).checkbox.Source = new BitmapImage(new Uri("/Resources/checkbox_neutral.png", UriKind.Relative));
            else if (value.Value) ((TagCheckbox)d).checkbox.Source = new BitmapImage(new Uri("/Resources/checkbox_positive.png", UriKind.Relative));
            else ((TagCheckbox)d).checkbox.Source = new BitmapImage(new Uri("/Resources/checkbox_negative.png", UriKind.Relative));
        }

        public TagCheckbox()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (!Value.HasValue) Value = true;
                else if (Value.Value) Value = false;
                else if (!Value.Value) Value = null;
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                if (!Value.HasValue) Value = false;
                else if (Value.Value) Value = null;
                else if (!Value.Value) Value = true;
            }
            TagValueChanged?.Invoke(this, Value);
        }
    }
}
