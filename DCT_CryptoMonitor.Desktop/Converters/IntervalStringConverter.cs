using DCT_CryptoMonitor.Core.Models.Enums;
using System.Globalization;
using System.Windows.Data;

namespace DCT_CryptoMonitor.Desktop.Converters;
internal class IntervalStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string intervalString = ((PriceInterval)value).ToString();

        if (intervalString == "m1")
        {
            return "1 minute";
        }
        else if (intervalString == "m5")
        {
            return "5 minutes";
        }
        else if (intervalString == "m15")
        {
            return "15 minutes";
        }
        else if (intervalString == "m30")
        {
            return "30 minutes";
        }
        else if (intervalString == "h1")
        {
            return "1 hour";
        }
        else if (intervalString == "h2")
        {
            return "2 hours";
        }
        else if (intervalString == "h6")
        {
            return "6 hours";
        }
        else if (intervalString == "h12")
        {
            return "12 hours";
        }
        else if (intervalString == "d1")
        {
            return "1 day";
        }

        return "Unknown interval";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
