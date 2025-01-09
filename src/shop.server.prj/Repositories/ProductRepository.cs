using Microsoft.EntityFrameworkCore;

using Shop.Model.Bracket; 

namespace Shop.Server.Repositories;

public class ProductRepository : DatabaseBase
{
	public DbSet<ProductEntity> Products { get; set; } 

	public ProductRepository(IDatabase database)
		: base(database)
	{ }

	/// <summary>
	/// Получение из базы данных (таблицы Products) всех товаров.
	/// </summary>
	/// <returns></returns>
	public async Task<IEnumerable<ProductEntity>> FetchAllProducts()
	{
		var resultProducts = new List<ProductEntity>(); 

		resultProducts = this.Products.ToList(); 

		return resultProducts;
	}
}
