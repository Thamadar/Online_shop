using System.Globalization;
using System.Windows.Data; 

namespace Shop.UI.WPF.Converters;

/// <summary> Конвертер, преобразующий все буквы в заглавные</summary>
public class StringAllSymbolsToUpperConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if(value != null && value is string)
		{
			return ((string)value).ToUpper();
		}

		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return null;
	}
}
