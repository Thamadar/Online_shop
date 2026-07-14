using Shop.Client.WPF.Views.Pages;
using Shop.Dto.Orders;
using System.Text;

namespace Shop.Client.WPF.Extensions;
public static class ListExtension
{
	/// <summary>
	/// Товары для заказа: id-товара, кол-во.
	/// </summary>  
	public static List<OrderProductRequest> GetOrderProducts(this IEnumerable<IProductItemVM> items)
	{
		var result = items.Where(x => x.CurrentSelectedCount > 0).Select(x => new OrderProductRequest(x.Id, x.CurrentSelectedCount)).ToList();

		return result;
	}
}
