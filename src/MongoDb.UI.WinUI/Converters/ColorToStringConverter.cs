using CommunityToolkit.WinUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;

namespace MongoDb.UI.WinUI.Converters
{
    public class ColorToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var color = ToColorFromHex(value?.ToString());
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var color = ToColor((Color)value);
            return color;
        }

        private static Color ToColorFromHex(string s)
        {
            var color = s?.ToColor() ?? Colors.White;
            return color;
        }

        private static string ToColor(Color color)
        {
            var s = color.ToHex();
            return s;
        }
    }
}
