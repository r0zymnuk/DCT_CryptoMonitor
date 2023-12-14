using System.Globalization;
using System.Windows.Data;

namespace DCT_CryptoMonitor.Desktop.Converters;
public class PercentageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool success = decimal.TryParse(value.ToString(), out decimal price);
        if (success)
            return $"{(price>0?"+":"")}{price/100:P2}";

        return "0%";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
