using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IngenieriaBosco.Front.Converters
{
    internal class StringToInt32Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i = (int)value;
            return $"{i}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            if (string.IsNullOrEmpty(str)) return 0;
            str = str.Replace(" ","");
            bool result = int.TryParse(str, out int rta);
            return result? rta : 0;
        }
    }
}
