using Shop.Model.Database.Entities;

namespace Shop.Server.Repositories;

public interface IProductRepository
{  
	/// <summary>
	/// Добавление товаров.
	/// </summary> 
	Task<bool> PostProducts(ProductEntity[] entities);

	/// <summary>
	/// Получение товара по id.
	/// </summary> 
	Task<ProductEntity?> GetProductById(int id); 

	/// <summary>
	/// Получение из базы данных (таблицы Products) всех товаров.
	/// </summary>
	/// <returns></returns>
	Task<IEnumerable<ProductEntity>> GetProducts(); 
}
