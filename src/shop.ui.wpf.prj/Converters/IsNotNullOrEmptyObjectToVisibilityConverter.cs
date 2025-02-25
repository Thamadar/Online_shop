using System.Globalization; 
using System.Windows.Data;
using System.Windows;

namespace Shop.UI.WPF.Converters;
public class IsNotNullOrEmptyObjectToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if(value != null)
		{
			var valueType = value.GetType();
			switch(valueType)
			{
				case Type type when type == typeof(string):
					{
						var stringValue = (string)value;
						return string.IsNullOrWhiteSpace(stringValue) ? Visibility.Collapsed : Visibility.Visible;
					}
				case Type type when type == typeof(bool):
					{
						var boolValue = (bool)value;
						return boolValue == false ? Visibility.Collapsed : Visibility.Visible;
					}
				case Type type when type == typeof(int):
					{
						var intValue = (int)value;
						return intValue == 0 ? Visibility.Collapsed : Visibility.Visible;
					}
				case Type type when type == typeof(double):
					{
						var doubleValue = (double)value;
						return doubleValue == 0 ? Visibility.Collapsed : Visibility.Visible;
					}
				case Type type when type == typeof(decimal):
					{
						var decimalValue = (decimal)value;
						return decimalValue == 0 ? Visibility.Collapsed : Visibility.Visible;
					}
				case Type type when type == typeof(float):
					{
						var floatValue = (float)value;
						return floatValue == 0 ? Visibility.Collapsed : Visibility.Visible;
					}
			} 
		}
		return Visibility.Collapsed;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}

