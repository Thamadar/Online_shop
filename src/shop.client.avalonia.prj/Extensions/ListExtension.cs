using Shop.Client.Avalonia.Views.Pages;
using Shop.Dto.Orders; 

namespace Shop.Client.Avalonia.Extensions;
public static class ListExtension
{
	/// <summary>
	/// Товары для заказа: id-товара, кол-во.
	/// </summary>  
	public static List<OrderProductRequest> GetOrderProducts(this IEnumerable<IProductItemVM> items)
	{ 
		var result = items.Where(x => x.CurrentSelectedCount > 0).Select(x => new OrderProductRequest(x.Id,x.CurrentSelectedCount)).ToList();

		return result; 
	}
}
