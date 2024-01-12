using System.Globalization;
using System.Windows.Data;

namespace DCT_CryptoMonitor.Desktop.Converters;
public class PriceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool success = decimal.TryParse(value.ToString(), out decimal price);
        if (success)
            return price.ToString("C2", CultureInfo.GetCultureInfo("en-US"));

        return "None";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool success = decimal.TryParse(value.ToString()?[1..] ?? "0", out var price);
        if (success)
            return price;

        return 0;
    }
}