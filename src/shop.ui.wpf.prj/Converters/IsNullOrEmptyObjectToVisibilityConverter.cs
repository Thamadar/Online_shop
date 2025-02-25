using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Shop.UI.WPF.Converters;

public class IsNullOrEmptyObjectToVisibilityConverter : IValueConverter
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
						return string.IsNullOrWhiteSpace(stringValue) ? Visibility.Visible : Visibility.Collapsed;
					}
				case Type type when type == typeof(bool):
					{
						var boolValue = (bool)value;
						return boolValue == false ? Visibility.Visible : Visibility.Collapsed;
					}
				case Type type when type == typeof(int):
					{
						var intValue = (int)value;
						return intValue == 0 ? Visibility.Visible : Visibility.Collapsed;
					}
				case Type type when type == typeof(double):
					{
						var doubleValue = (double)value;
						return doubleValue == 0 ? Visibility.Visible : Visibility.Collapsed;
					}
				case Type type when type == typeof(decimal):
					{
						var decimalValue = (decimal)value;
						return decimalValue == 0 ? Visibility.Visible : Visibility.Collapsed;
					}
				case Type type when type == typeof(float):
					{
						var floatValue = (float)value;
						return floatValue == 0 ? Visibility.Visible : Visibility.Collapsed;
					}
			}
		}
		return Visibility.Visible;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}

