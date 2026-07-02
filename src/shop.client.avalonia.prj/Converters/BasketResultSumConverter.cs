using Avalonia.Data.Converters; 
using System.Globalization; 

namespace Shop.Client.Avalonia.Converters;

/// <summary>
/// Конвертер для итоговой суммы в корзине товаров: цена товара * кол-во выбранного товара.
/// Первое значение - цена товара (decimal).
/// Второе значение - кол-во выбранного товара (int).
/// </summary>
public class BasketResultSumConverter : IMultiValueConverter
{
	public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
	{
		if(values.Count == 2 && values[0] is decimal price && values[1] is int count)
		{
			return $"{price * count}";
		}
		return "0";
	}
}
