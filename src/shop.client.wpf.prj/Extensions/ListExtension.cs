using Shop.Client.WPF.Views.Pages;
using System.Text;

namespace Shop.Client.WPF.Extensions;
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
