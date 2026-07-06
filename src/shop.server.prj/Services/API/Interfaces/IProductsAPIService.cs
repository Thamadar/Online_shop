using Shop.Dto.Products;
using Shop.Server.Data;
using Shop.Server.Services.API.Interfaces;

namespace Shop.Server.Services.API;

public interface IProductsAPIService : IAPIService
{
	/// <summary>
	/// Получение всех товаров.
	/// </summary> 
	Task<GetProductsResponse> GetProductsAsync();

	/// <summary>
	/// Получение товара по ID.
	/// </summary> 
	Task<GetProductResponse> GetProductByIdAsync(int id);

	/// <summary>
	/// Получение товара по ключу имени (ProductName).
	/// </summary> 
	Task<GetProductResponse> GetProductByNameAsync(string productName);

	/// <summary>
	/// Добавление товаров.
	/// </summary> 
	Task<CreateProductsResponse> CreateProductsAsync(CreateProductsRequest createProductsRequest);

	/// <summary>
	/// Добавление товара.
	/// </summary> 
	Task<CreateProductResponse> CreateProductAsync(CreateProductRequest createProductRequest);
	  
	/// <summary>
	/// Изменение скидки товара.
	/// </summary> 
	Task EditProductDiscountAsync(int productId, EditProductDiscountRequest editProductDiscountRequest);

	/// <summary>
	/// Удаление скидки товара.
	/// </summary> 
	Task ClearProductDiscountAsync(int productId);
}
