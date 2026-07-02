using DynamicData;

using Shop.Client.Avalonia.Views.Pages; 

namespace Shop.Client.Avalonia.Services;

/// <summary>
/// Интерфейс сервиса по товарам.
/// </summary>
public interface IProductsService : IShopService
{
	/// <summary>
	/// Отслеживание изменений цены всех выбранных товаров, находящихся в корзине.
	/// </summary>
	IObservable<decimal?> ResultBasketPriceObservable { get; }

	/// <summary> 
	/// Подключение к списку товаров в корзине.
	/// </summary> 
	IObservable<IChangeSet<IProductItemVM>> ConnectToProductsInBasketItems();

	/// <summary> 
	/// Подключение к списку товаров.
	/// </summary> 
	IObservable<IChangeSet<IProductItemVM>> ConnectToProductsItems();

	/// <summary>
	/// Обновление списка товаров.
	/// </summary> 
	Task<bool> UpdateProductsItemsAsync();

	/// <summary>
	/// Добавление товаров в корзину.
	/// </summary> 
	Task<bool> AddProductToBasket(int[] idProducts);

	/// <summary>
	/// Добавление единицы товара в корзину.
	/// </summary> 
	Task<bool> AddProductToBasket(int idProduct);

	/// <summary>
	/// Удаление товаров из корзины.
	/// </summary> 
	Task<bool> RemoveProductFromBasket(int[] idProducts);

	/// <summary>
	/// Удаление единицы товара из корзины.
	/// </summary> 
	Task<bool> RemoveProductFromBasket(int idProduct);

	/// <summary>
	/// Полное удаление из корзины товара по ID.
	/// </summary> 
	void TotalRemoveProductFromBasket(int idProduct);

	/// <summary>
	/// Удаление всех товаров из корзины.
	/// </summary> 
	void RemoveAllProductsFromBasket();
	 
	/// <summary>
	/// Создание заказа, отправка его через HTTP на сервер.
	/// </summary> 
	Task<bool> CreateOrderAsync();
}
