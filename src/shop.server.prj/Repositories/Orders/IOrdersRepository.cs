
using Shop.Server.Data;

namespace Shop.Server.Repositories;

public interface IOrdersRepository
{  
	/// <summary>
	/// Добавление товаров.
	/// </summary> 
	Task<bool> PostOrders(OrderEntity[] entities);

	/// <summary>
	/// Получение товара по id.
	/// </summary> 
	Task<OrderEntity?> GetOrderById(Guid id);

	/// <summary>
	/// Получение из базы данных (таблицы Orders) всех товаров.
	/// </summary> 
	Task<IEnumerable<OrderEntity>> GetOrders();
}
