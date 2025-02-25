using Shop.Model.Database.Entities;

using Shop.Server.Entities;

namespace Shop.Server.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly ProductContext _productContext;

	public ProductRepository(ProductContext productContext)
	{
		_productContext = productContext;
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<ProductEntity>> FetchAllProducts()
	{
		return _productContext.Products.ToList();
	}

	/// <inheritdoc/>
	public async Task<bool> IsProductsEmpty()
	{
		return !_productContext.Products.Any(); 
	}
}
