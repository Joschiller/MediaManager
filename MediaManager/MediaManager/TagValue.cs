using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MediaManager
{
    public enum TagValue
    {
        Negative = -1,
        Neutral = 0,
        Positive = 1,
    }
    public static class TagUtils
    {
        public static ImageSource GetIconForTagValue(bool? value)
        {
            if (!value.HasValue) return new BitmapImage(new Uri("/Resources/checkbox_neutral.png", UriKind.Relative));
            else if (value.Value) return new BitmapImage(new Uri("/Resources/checkbox_positive.png", UriKind.Relative));
            else return new BitmapImage(new Uri("/Resources/checkbox_negative.png", UriKind.Relative));
        }
    }
}
