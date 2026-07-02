using System.Globalization; 
using System.Windows.Data;

namespace Shop.Client.WPF.Converters;

/// <summary>
/// Конвертер для итоговой суммы в корзине товаров: цена товара * кол-во выбранного товара.
/// Первое значение - цена товара (decimal).
/// Второе значение - кол-во выбранного товара (int).
/// </summary>
public class BasketResultSumConverter : IMultiValueConverter
{
	public object? Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
	{
		if(values.Length == 2 && values[0] is decimal price && values[1] is int count)
		{
			return $"{price * count}";
		}
		return "0";
	}

	public object[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
