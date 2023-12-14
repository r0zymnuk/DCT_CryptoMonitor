using System.Globalization;
using System.Windows.Data;

namespace DCT_CryptoMonitor.Desktop.Converters;
public class IsGainingConditionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var sValue = value as string;
        if (!string.IsNullOrWhiteSpace(sValue)) 
        {
            return sValue[0] == '+';
        }
        
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
