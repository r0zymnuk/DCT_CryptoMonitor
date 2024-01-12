using System.Globalization;
using System.Windows.Data;

namespace DCT_CryptoMonitor.Desktop.Converters;

public class GraphHeightConvertor : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is double windowActualHeight && values[1] is double topStackPanelActualHeight)
        {
            return windowActualHeight - topStackPanelActualHeight;
        }

        return 0;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}