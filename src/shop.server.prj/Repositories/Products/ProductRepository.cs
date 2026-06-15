using Microsoft.EntityFrameworkCore;
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
	public async Task<IEnumerable<ProductEntity>> GetProducts()
	{
		return await _productContext.Products
		.Include(p => p.Localizations)
		.AsNoTracking()
		.ToListAsync();
	}

	/// <inheritdoc/>
	public async Task<ProductEntity?> GetProductById(int id)
	{
		return await _productContext.Products
		.Where(x => x.Id == id)
		.FirstOrDefaultAsync();
	}

	/// <inheritdoc/>
	public async Task<bool> PostProducts(ProductEntity[] entities)
	{
		foreach(var entity in entities)
		{ 
			if(entity.Localizations.Count == 0)
			{
				return false;
			}
		}

		await _productContext.Products.AddRangeAsync(entities);
		await _productContext.SaveChangesAsync();

		return true;
	}
}
