using DynamicData;

using Shop.Client.Views.Pages; 

namespace Shop.Client.Services;
/// <summary>
/// Интерфейс сервиса по товарам.
/// </summary>
public interface IProductsService : IShopService
{
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
	Task<bool> UpdateProductsItems();

	/// <summary>
	/// Добавление единицы товаров в корзину.
	/// </summary> 
	Task<bool> AddProductToBasket(int[] idProducts);

	/// <summary>
	/// Добавление единицы товара в корзину.
	/// </summary> 
	Task<bool> AddProductToBasket(int idProduct);

	/// <summary>
	/// Удаление единицы товаров из корзины.
	/// </summary> 
	Task<bool> RemoveProductFromBasket(int[] idProducts);

	/// <summary>
	/// Удаление единицы товара из корзины.
	/// </summary> 
	Task<bool> RemoveProductFromBasket(int idProduct);

	/// <summary>
	/// Полное удаление из корзины (всех единиц).
	/// </summary>
	/// <param name="idProduct"></param>
	/// <returns></returns>
	Task<bool> TotalRemoveProductFromBasket(int idProduct);

	/// <summary>
	/// Удаление всех товаров из корзины.
	/// </summary> 
	Task<bool> RemoveAllProductsFromBasket();
	 
	/// <summary>
	/// Создание заказа, отправка его через HTTP на сервер.
	/// </summary> 
	Task<bool> CreateOrder(Guid userId, string address);
}
