using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace MediaManager.Globals
{
    class XamlPartConcatenator : IValueConverter
    {
        private Part defaultPart = new Part
        {
            Title = "",
            Medium = new Medium
            {
                Title = ""
            }
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var part = ParsePart(value);
            return part.Title + " (" + part.Medium.Title + ")";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parsed = value as string;
            if (parsed == null) return defaultPart;
            var part = Regex.Split(parsed, @"\(.*\)");
            var medium = Regex.Match(parsed, @"\(.*\)");
            return new Part
            {
                Title = part[0].Trim(),
                Medium = new Medium
                {
                    Title = medium.Value.Substring(1, medium.Value.Length - 1).Trim()
                }
            };
        }

        private Part ParsePart(object parameter) => parameter as Part ?? defaultPart;
    }
}
