using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Shop.UI.WPF.Converters;

public class ComparisionConverter<T> : IValueConverter
{
	public T Comparand { get; set; }

	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return (value is T x) ? Comparer<T>.Default.Compare(x, Comparand) == 0 : DependencyProperty.UnsetValue;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
