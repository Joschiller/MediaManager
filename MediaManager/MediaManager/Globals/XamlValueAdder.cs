using System;
using System.Globalization;
using System.Windows.Data;

namespace MediaManager.Globals
{
    public class XamlValueAdder : IValueConverter
    {
        public double Pad { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ParseDouble(value, 0) + Pad;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ParseDouble(value, 0) - Pad;

        private double ParseDouble(object parameter, double defaultValue)
        {
            double a;
            try { a = parameter == null ? defaultValue : System.Convert.ToDouble(parameter); }
            catch { a = defaultValue; }
            return a;
        }
    }
}
