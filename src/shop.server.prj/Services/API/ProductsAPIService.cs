

using AutoMapper; 
using Shop.Dto.Products; 
using Shop.Server.Repositories;

namespace Shop.Server.Services.API;

public class ProductsAPIService : IProductsAPIService
{
	private readonly IProductsRepository _productRepository;
	private readonly IMapper _mapper;

	public ProductsAPIService(IMapper mapper, IProductsRepository repository)
	{
		_mapper = mapper;
		_productRepository = repository;
	}

	/// <inheritdoc/>
	public async Task<GetProductsResponse> GetProductsAsync()
	{ 
		var productsResponse = await _productRepository.GetProductsAsync();
		return productsResponse;
	}

	/// <inheritdoc/>
	public async Task<GetProductResponse> GetProductByIdAsync(int id)
	{ 
		var productResponse = await _productRepository.GetProductByIdAsync(id);
		return productResponse;
	}

	/// <inheritdoc/>
	public async Task<GetProductResponse> GetProductByNameAsync(string productName)
	{
		var productResponse = await _productRepository.GetProductByNameAsync(productName);
		return productResponse;
	}
	 
	/// <inheritdoc/>
	public async Task<CreateProductResponse> CreateProductAsync(CreateProductRequest createProductRequest)
	{
		if(_productRepository.IsAnyNameExist(createProductRequest.ProductName))
			throw new ArgumentException("Товар с таким именем уже существует. ");
		//TO DO: проверка на существующие локализации...

		var resultResponse = await _productRepository.CreateProductAsync(createProductRequest);
		return resultResponse;
	}

	/// <inheritdoc/>
	public async Task<CreateProductsResponse> CreateProductsAsync(CreateProductsRequest createProductsRequest)
	{ 
		var names = createProductsRequest.Products.Select(u => u.ProductName).ToList();
		var namesExist = _productRepository.CheckNamesExist(names);
		if(namesExist.Count > 0)
			throw new ArgumentException($"Товары с таким именем уже существуют: {namesExist}");
		//TO DO: проверка на существующие локализации...

		var resultResponse = await _productRepository.CreateProductsAsync(createProductsRequest);
		return resultResponse;
	} 

	/// <inheritdoc/>
	public async Task EditProductDiscountAsync(int productId, EditProductDiscountRequest editProductDiscountRequest)
	{ 
		await _productRepository.EditProductDiscountAsync(
			productId,
			editProductDiscountRequest.DiscountValue,
			editProductDiscountRequest.DiscountUnit); 
	}

	/// <inheritdoc/>
	public async Task ClearProductDiscountAsync(int productId)
	{
		await _productRepository.ClearProductDiscountAsync(productId);
	}

	/// <inheritdoc/>
	public void Initialization()
	{ }

}
