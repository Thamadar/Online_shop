using Shop.Client.Views.Pages;
using Shop.Model.Database.Entities;

namespace Shop.Client.Extensions;
public static class EntityExtensions
{
	 
	public static List<ProductItemVM> ConvertToVM(this IEnumerable<ProductEntity> productEntity)
	{
		var viewModels = new List<ProductItemVM>();

		foreach(var entity in productEntity)
		{
			viewModels.Add(entity.ConvertToVM());
		}

		return viewModels;
	}

	public static ProductItemVM ConvertToVM(this ProductEntity productEntity)
	{
		return new ProductItemVM(
			productEntity.Id,
			productEntity.ProductName,
			productEntity.CurrentCount,
			productEntity.Price,
			productEntity.PriceBeforeSale,
			productEntity.Weight,
			productEntity.Image);
	}
}
