using AutoMapper;
using AutoMapper.QueryableExtensions; 
using Microsoft.EntityFrameworkCore; 
using Shop.Dto.Products; 
using Shop.Server.Data; 

namespace Shop.Server.Repositories;

public class ProductsRepository : IProductsRepository
{
	private readonly IMapper _mapper;
	private readonly ProductContext _productContext;

	public ProductsRepository(IMapper mapper, ProductContext productContext)
	{
		_mapper = mapper;
		_productContext = productContext;
	}


	/// <inheritdoc/>
	public async Task<GetProductsResponse> GetProductsAsync()
	{
		try
		{
			var productResponses = await _productContext.Products
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
			var productEntity = await _productContext.Products.FindAsync(productId);

			if(productEntity == null)
				throw new Exception();

			await _productContext
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
			var productResponse = await _productContext.Products
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
		await using var transaction = await _productContext.Database.BeginTransactionAsync();
		try
		{
			var entities = _mapper.Map<List<ProductEntity>>(createProductsRequest.Products);

			await _productContext.Products.AddRangeAsync(entities);
			await _productContext.SaveChangesAsync();

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
		await using var transaction = await _productContext.Database.BeginTransactionAsync();
		try
		{
			var entity = _mapper.Map<ProductEntity>(createProductRequest);

			await _productContext.Products.AddAsync(entity);
			await _productContext.SaveChangesAsync();

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
	public async Task EditProductDiscountAsync(int productId, decimal discountValue, DiscountUnit discountUnit)
	{
		await using var transaction = await _productContext.Database.BeginTransactionAsync();
		try
		{
			var entity = await _productContext.Products.FindAsync(productId);
			 
			if(entity != null)
			{
				entity.SetDiscount(discountValue, discountUnit);
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
		await using var transaction = await _productContext.Database.BeginTransactionAsync();
		try
		{
			var entity = await _productContext.Products.FindAsync(productId);

			if(entity != null)
			{
				entity.ClearDiscount();
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
		return _productContext.Products
			.AsNoTracking()
			.Any(x => x.ProductName == name);
	}

	/// <inheritdoc/>
	public List<string> CheckNamesExist(List<string> names)
	{
		var existNames = _productContext.Products
			.ToList()
			.Select(x => x.ProductName);

		return names.Where(x => existNames.Any(y => y == x)).ToList();
	}

}
