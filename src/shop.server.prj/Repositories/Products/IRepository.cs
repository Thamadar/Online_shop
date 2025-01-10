using Shop.Model.Database.Entities;

namespace Shop.Server.Repositories;

public interface IProductRepository
{
	/// <summary>
	/// Получение из базы данных (таблицы Products) всех товаров.
	/// </summary>
	/// <returns></returns>
	Task<IEnumerable<ProductEntity>> FetchAllProducts();

	/// <summary>
	/// Пуста ли таблица товаров?
	/// </summary>
	/// <returns></returns>
	Task<bool> IsProductsEmpty();
}
