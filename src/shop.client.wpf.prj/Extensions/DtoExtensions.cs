using ReactiveUI;
using Shop.Client.WPF.Views.Pages;
using Shop.Dto.Products;
using System.Globalization;

namespace Shop.Client.WPF.Extensions;
public static class DtoExtensions
{ 
	private static CultureInfo _currentCulture = CultureInfo.CurrentUICulture;

	public static List<ProductItemVM> ConvertToVM(
		this IEnumerable<GetProductResponse> getProductResponses)
	{ 
		var viewModels = new List<ProductItemVM>();

		foreach(var getProductResponse in getProductResponses)
		{
			viewModels.Add(getProductResponse.ConvertToVM());
		}

		return viewModels;
	}

	public static ProductItemVM ConvertToVM(this GetProductResponse getProductResponse, 
		Func<Task>? addCommand    = null,
		Func<Task>? removeCommand = null)
	{
		var cultureName = _currentCulture.Name;

		return new ProductItemVM(
		getProductResponse.Id,
		getProductResponse.Localizations.FirstOrDefault(x => x.LangCode == cultureName)?.DisplayName ?? "-",
		getProductResponse.AvailableCount,
		getProductResponse.BasePrice,
		getProductResponse.ResultPrice,
		getProductResponse.Discount.DiscountValue,
		getProductResponse.Discount.DiscountUnit,
		getProductResponse.Weight,
		getProductResponse.Image,
		addCommand: ReactiveCommand.CreateFromTask(addCommand),
		removeCommand: ReactiveCommand.CreateFromTask(removeCommand));
	}
}
