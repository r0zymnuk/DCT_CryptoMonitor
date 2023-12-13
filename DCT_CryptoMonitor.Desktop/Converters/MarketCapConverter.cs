using System.Globalization;
using System.Windows.Data;

namespace DCT_CryptoMonitor.Desktop.Converters;
public class MarketCapConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string[] scales = { "", "K", "M", "B", "T", "Q"};

        bool success = decimal.TryParse(value.ToString(), out decimal cap);
        int scale = 0;
        
        while (cap > 1000)
        {
            cap /= 1000;
            scale++;
        }

        return $"{cap.ToString("C2", CultureInfo.GetCultureInfo("en-US"))}{scales[scale]}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
