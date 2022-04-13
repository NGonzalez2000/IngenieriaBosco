using System;
using System.Globalization;
using System.Windows.Data;

namespace IngenieriaBosco.Front.Converters
{
    public class ESARConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("es-AR"), "{0:C2}", (decimal)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            str = str.Replace(".", "")
                .Replace("$", "").Replace(",", "").TrimStart('0');

            decimal ul;
            decimal.TryParse(str, out ul);

            return ul / 100;
        }
    }
}
