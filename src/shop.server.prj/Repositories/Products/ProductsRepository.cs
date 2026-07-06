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
	public async Task<GetProductsResponse> GetProductsAsync()
	{
		try
		{
			var productResponses = await _serverContext.Products
				.AsNoTracking()
				.Include(p => p.Localizations)
				.ProjectTo<GetProductResponse>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return new GetProductsResponse(productResponses);
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException("Не удалось получить товары", ex);
		}
	}
	 
	/// <inheritdoc/>
	public async Task<GetProductResponse> GetProductByIdAsync(int productId)
	{
		try
		{
			var productEntity = await _serverContext.Products.FindAsync(productId);

			if(productEntity == null)
				throw new Exception();

			await _serverContext
				.Entry(productEntity)
				.Collection(p => p.Localizations)
				.LoadAsync();

			var productResponse = _mapper.Map<GetProductResponse>(productEntity);

			return productResponse;
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось найти товар по id {productId}", ex);
		}
	}

	/// <inheritdoc/> 
	public async Task<GetProductResponse> GetProductByNameAsync(string productName)
	{
		try
		{
			var productResponse = await _serverContext.Products
				.AsNoTracking()
				.Where(x => x.ProductName == productName)
				.Include(p => p.Localizations)
				.ProjectTo<GetProductResponse>(_mapper.ConfigurationProvider)
				.FirstAsync();

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
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var entities = _mapper.Map<List<ProductEntity>>(createProductsRequest.Products);

			await _serverContext.Products.AddRangeAsync(entities);
			await _serverContext.SaveChangesAsync();

			var usersResponse = new CreateProductsResponse(_mapper.Map<List<CreateProductResponse>>(entities));
			await transaction.CommitAsync();

			return usersResponse;

		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не удалось создать и добавить товары в БД", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<CreateProductResponse> CreateProductAsync(CreateProductRequest createProductRequest)
	{
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var entity = _mapper.Map<ProductEntity>(createProductRequest);

			await _serverContext.Products.AddAsync(entity);
			await _serverContext.SaveChangesAsync();

			var response = _mapper.Map<CreateProductResponse>(entity);

			await transaction.CommitAsync();

			return response;
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не удалось создать и добавить товар в БД", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<bool> CheckAvailabilityQuantityAsync(int productId, int quantity)
	{ 
		try
		{
			var productEntity = await _serverContext.Products.FindAsync(productId);

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
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var productEntity = await _serverContext.Products.FindAsync(productId);

			if(productEntity == null || !productEntity.AddAvailableCount(quantity))
				throw new Exception();

			await _serverContext.SaveChangesAsync();
			await transaction.CommitAsync(); 
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось убавить кол-во товаров в ProductEntity на {quantity}", ex);
		}
	}

	/// <inheritdoc/>
	public async Task RemoveQuantityAsync(int productId, int quantity)
	{
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var productEntity = await _serverContext.Products.FindAsync(productId);

			if(productEntity == null || !productEntity.RemoveAvailableCount(quantity))
				throw new Exception();

			await _serverContext.SaveChangesAsync();
			await transaction.CommitAsync(); 
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось убавить кол-во товаров в ProductEntity на {quantity}", ex);
		}
	}

	/// <inheritdoc/>
	public async Task EditProductDiscountAsync(int productId, decimal discountValue, DiscountUnit discountUnit)
	{
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var entity = await _serverContext.Products.FindAsync(productId);
			 
			if(entity != null)
			{
				entity.SetDiscount(discountValue, discountUnit);
				await _serverContext.SaveChangesAsync();
				await transaction.CommitAsync();
			}
			else
			{
				throw new InvalidOperationException($"Не найти товар по Id {productId} для изменение его скидки.");
			}
		}
		catch
		{
			await transaction.RollbackAsync();
			throw;
		}
	}

	/// <inheritdoc/>
	public async Task ClearProductDiscountAsync(int productId)
	{
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var entity = await _serverContext.Products.FindAsync(productId);

			if(entity != null)
			{
				entity.ClearDiscount();
				await _serverContext.SaveChangesAsync();
				await transaction.CommitAsync();
			}
			else
			{
				throw new InvalidOperationException($"Не найти товар по Id {productId} для удаления его скидки.");
			}
		}
		catch
		{
			await transaction.RollbackAsync();
			throw;
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
