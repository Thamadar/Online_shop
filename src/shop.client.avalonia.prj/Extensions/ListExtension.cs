using Shop.Client.Avalonia.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Client.Avalonia.Extensions;
public static class ListExtension
{
	/// <summary>
	/// Товары из заказа. ("id, количество". Разделение через ;).
	/// Пример: "2,5;3,1;6,3"
	/// </summary>  
	public static string GetProductsFromString(this IEnumerable<IProductItemVM> items)
	{ 
		StringBuilder stringBuilder = new StringBuilder(); 
		stringBuilder.AppendJoin(';', items.Where(x => x.CurrentSelectedCount > 0).Select(x => $"{x.Id},{x.CurrentSelectedCount}")); 
		return stringBuilder.ToString(); 
	}
}
