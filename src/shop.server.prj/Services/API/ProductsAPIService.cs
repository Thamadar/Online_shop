

using Shop.Server.Repositories;

namespace Shop.Server.Services.API;

public class ProductsAPIService : IProductsAPIService
{
	private readonly IProductRepository _productRepository;
	/// <inheritdoc/>
	public void Initialization()
	{ }

}
