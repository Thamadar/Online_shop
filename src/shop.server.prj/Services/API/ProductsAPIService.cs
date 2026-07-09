

using AutoMapper; 
using Shop.Dto.Products;  

namespace Shop.Server.Services.API;

public class ProductsAPIService : IProductsAPIService
{
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public ProductsAPIService(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}

	/// <inheritdoc/>
	public async Task<GetProductsResponse> GetProductsAsync(CancellationToken ct = default)
	{ 
		var productsResponse = await _unitOfWork.Products.GetProductsAsync(ct);
		return productsResponse;
	}

	/// <inheritdoc/>
	public async Task<GetProductResponse> GetProductByIdAsync(int id, CancellationToken ct = default)
	{ 
		var productResponse = await _unitOfWork.Products.GetProductByIdAsync(id, ct);
		return productResponse;
	}

	/// <inheritdoc/>
	public async Task<GetProductResponse> GetProductByNameAsync(string productName, CancellationToken ct = default)
	{
		var productResponse = await _unitOfWork.Products.GetProductByNameAsync(productName, ct);
		return productResponse;
	}
	 
	/// <inheritdoc/>
	public async Task<CreateProductResponse> CreateProductAsync(CreateProductRequest createProductRequest)
	{
		await using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{ 
			if(_unitOfWork.Products.IsAnyNameExist(createProductRequest.ProductName))
				throw new ArgumentException("Товар с таким именем уже существует. ");
			//TO DO: проверка на существующие локализации...

			var resultResponse = await _unitOfWork.Products.CreateProductAsync(createProductRequest);

			await transaction.CommitAsync();

			return resultResponse; 
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не удалось создать товар", ex);
		} 
	}

	/// <inheritdoc/>
	public async Task<CreateProductsResponse> CreateProductsAsync(CreateProductsRequest createProductsRequest)
	{
		await using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{ 
			var names = createProductsRequest.Products.Select(u => u.ProductName).ToList();
			var namesExist = _unitOfWork.Products.CheckNamesExist(names);
			if(namesExist.Count > 0)
				throw new ArgumentException($"Товары с таким именем уже существуют: {namesExist}");
			//TO DO: проверка на существующие локализации...

			var resultResponse = await _unitOfWork.Products.CreateProductsAsync(createProductsRequest);

			await transaction.CommitAsync();

			return resultResponse; 
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не удалось создать товары", ex);
		} 
	} 

	/// <inheritdoc/>
	public async Task EditProductDiscountAsync(int productId, EditProductDiscountRequest editProductDiscountRequest)
	{
		await using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{

			await _unitOfWork.Products.EditProductDiscountAsync(
				productId,
				editProductDiscountRequest.DiscountValue,
				editProductDiscountRequest.DiscountUnit);

			await transaction.CommitAsync();
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не изменить скидку товара", ex);
		} 
	}

	/// <inheritdoc/>
	public async Task ClearProductDiscountAsync(int productId)
	{
		await using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{ 
			await _unitOfWork.Products.ClearProductDiscountAsync(productId);

			await transaction.CommitAsync();
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не удалось удалить скидку товара", ex);
		} 
	}

	/// <inheritdoc/>
	public void Initialization()
	{ }

}
