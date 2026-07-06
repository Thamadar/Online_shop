
using Shop.Dto.Products; 

namespace Shop.Server.Repositories;

public interface IProductsRepository
{ 
	/// <summary>
	/// Получение всех товаров, не находящихся в заказе.
	/// </summary> 
	Task<GetProductsResponse> GetProductsAsync();

	/// <summary>
	/// Получение товара по ID.
	/// </summary> 
	Task<GetProductResponse> GetProductByIdAsync(int productId);

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
	/// Проверка наличия заданного кол-во товаров в выбранном ProductEntity.
	/// </summary> 
	Task<bool> CheckAvailabilityQuantityAsync(int productId, int quantity);

	/// <summary>
	/// Добавление выбранного кол-во товаров с AvailableCount в выбранном ProductEntity.
	/// </summary> 
	Task AddQuantityAsync(int productId, int quantity);

	/// <summary>
	/// Удаление выбранного кол-во товаров с AvailableCount в выбранном ProductEntity.
	/// </summary> 
	Task RemoveQuantityAsync(int productId, int quantity);

	/// <summary>
	/// Изменение скидки товара.
	/// </summary> 
	Task EditProductDiscountAsync(int productId, decimal discountValue, DiscountUnit discountUnit);

	/// <summary>
	/// Удаление скидки товара.
	/// </summary> 
	Task ClearProductDiscountAsync(int productId);

	/// <summary>
	/// Существует ли какой-либо товар с данным именем?
	/// </summary> 
	bool IsAnyNameExist(string name);

	/// <summary>
	/// Существует ли какой-либо товар с каким-либо именем из списка?
	/// </summary> 
	/// <param name="names">список именем на проверку</param>
	/// <returns>возвращает список уже существующих именем</returns>
	List<string> CheckNamesExist(List<string> names);
}
