using System.Windows.Media;

namespace MediaManager.GUI.Components
{
    public class TagListElement
    {
        public Tag Tag { get; set; }
        public bool? Value { get; set; }
        public ImageSource Icon { get; set; }
        public bool Enabled { get; set; }
    }
}
