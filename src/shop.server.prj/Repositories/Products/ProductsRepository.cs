using AutoMapper;
using AutoMapper.QueryableExtensions; 
using Microsoft.EntityFrameworkCore; 
using Shop.Dto.Products; 
using Shop.Server.Data; 

namespace Shop.Server.Repositories;

public class ProductsRepository : IProductsRepository
{
	private readonly IMapper _mapper;
	private readonly ServerContext _serverContext;

	public ProductsRepository(IMapper mapper, ServerContext serverContext)
	{
		_mapper = mapper;
		_serverContext = serverContext;
	}


	/// <inheritdoc/>
	public async Task<GetProductsResponse> GetProductsAsync(CancellationToken ct = default)
	{
		try
		{
			var productResponses = await _serverContext.Products
				.AsNoTracking()
				.Include(p => p.Localizations)
				.ProjectTo<GetProductResponse>(_mapper.ConfigurationProvider)
				.ToListAsync(ct);

			return new GetProductsResponse(productResponses);
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException("Не удалось получить товары", ex);
		}
	}
	 
	/// <inheritdoc/>
	public async Task<GetProductResponse> GetProductByIdAsync(int productId, CancellationToken ct = default)
	{
		try
		{
			var productEntity = await _serverContext.Products.FindAsync(productId);

			if(productEntity == null)
				throw new Exception();

			await _serverContext
				.Entry(productEntity)
				.Collection(p => p.Localizations)
				.LoadAsync(ct);

			var productResponse = _mapper.Map<GetProductResponse>(productEntity);

			return productResponse;
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось найти товар по id {productId}", ex);
		}
	}

	/// <inheritdoc/> 
	public async Task<GetProductResponse> GetProductByNameAsync(string productName, CancellationToken ct = default)
	{
		try
		{
			var productResponse = await _serverContext.Products
				.AsNoTracking()
				.Where(x => x.ProductName == productName)
				.Include(p => p.Localizations)
				.ProjectTo<GetProductResponse>(_mapper.ConfigurationProvider)
				.FirstAsync(ct);

			return productResponse;
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось найти товар по имени {productName}", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<CreateProductsResponse> CreateProductsAsync(CreateProductsRequest createProductsRequest)
	{ 
		try
		{
			var entities = _mapper.Map<List<ProductEntity>>(createProductsRequest.Products);

			await _serverContext.Products.AddRangeAsync(entities);
			await _serverContext.SaveChangesAsync();

			var usersResponse = new CreateProductsResponse(_mapper.Map<List<CreateProductResponse>>(entities)); 

			return usersResponse;

		}
		catch(Exception ex)
		{ 
			throw new InvalidOperationException("Не удалось создать и добавить товары в БД", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<CreateProductResponse> CreateProductAsync(CreateProductRequest createProductRequest)
	{ 
		try
		{
			var entity = _mapper.Map<ProductEntity>(createProductRequest);

			await _serverContext.Products.AddAsync(entity);
			await _serverContext.SaveChangesAsync();

			var response = _mapper.Map<CreateProductResponse>(entity); 

			return response;
		}
		catch(Exception ex)
		{ 
			throw new InvalidOperationException("Не удалось создать и добавить товар в БД", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<bool> CheckAvailabilityQuantityAsync(int productId, int quantity, CancellationToken ct = default)
	{ 
		try
		{
			var productEntity = await _serverContext.Products.FindAsync(productId, ct);

			if(productEntity == null)
				throw new Exception();

			return productEntity.AvailableCount >= quantity;
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException("Не удалось убавить кол-во товаров в ProductEntity", ex);
		}
	}

	/// <inheritdoc/>
	public async Task AddQuantityAsync(int productId, int quantity)
	{ 
		try
		{
			var productEntity = await _serverContext.Products.FindAsync(productId);

			if(productEntity == null || !productEntity.AddAvailableCount(quantity))
				throw new Exception();

			await _serverContext.SaveChangesAsync(); 
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось убавить кол-во товаров в ProductEntity на {quantity}", ex);
		}
	}

	/// <inheritdoc/>
	public async Task RemoveQuantityAsync(int productId, int quantity)
	{ 
		try
		{
			var productEntity = await _serverContext.Products.FindAsync(productId);

			if(productEntity == null || !productEntity.RemoveAvailableCount(quantity))
				throw new Exception();

			await _serverContext.SaveChangesAsync(); 
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось убавить кол-во товаров в ProductEntity на {quantity}", ex);
		}
	}

	/// <inheritdoc/>
	public async Task EditProductDiscountAsync(int productId, decimal discountValue, DiscountUnit discountUnit)
	{ 
		try
		{
			var entity = await _serverContext.Products.FindAsync(productId);
			 
			if(entity != null)
			{
				entity.SetDiscount(discountValue, discountUnit);
				await _serverContext.SaveChangesAsync(); 
			}
			else
			{
				throw new Exception();
			}
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не найти товар по Id {productId} для изменение его скидки.", ex);
		}
	}

	/// <inheritdoc/>
	public async Task ClearProductDiscountAsync(int productId)
	{ 
		try
		{
			var entity = await _serverContext.Products.FindAsync(productId);

			if(entity != null)
			{
				entity.ClearDiscount();
				await _serverContext.SaveChangesAsync(); 
			}
			else
			{
				throw new Exception();
			}
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Не найти товар по Id {productId} для удаления его скидки.", ex);
		}
	}

	/// <inheritdoc/>
	public bool IsAnyNameExist(string name)
	{
		return _serverContext.Products
			.AsNoTracking()
			.Any(x => x.ProductName == name);
	}

	/// <inheritdoc/>
	public List<string> CheckNamesExist(List<string> names)
	{
		var existNames = _serverContext.Products
			.ToList()
			.Select(x => x.ProductName);

		return names.Where(x => existNames.Any(y => y == x)).ToList();
	}

}
