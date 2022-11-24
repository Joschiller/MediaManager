using System;
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
        public string TagName { get; set; } = "";
        private bool? _Value = null;
        public bool? Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                if (!value.HasValue)
                {
                    checkbox.Source = new BitmapImage(new Uri("/Resources/checkbox_neutral.png", UriKind.Relative));
                }
                else if (value.Value)
                {
                    checkbox.Source = new BitmapImage(new Uri("/Resources/checkbox_positive.png", UriKind.Relative));
                }
                else
                {
                    checkbox.Source = new BitmapImage(new Uri("/Resources/checkbox_negative.png", UriKind.Relative));
                }
            }
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
        }
    }
}
