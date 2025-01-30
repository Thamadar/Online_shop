using Shop.Model.Database.Entities;

namespace Shop.Server.Repositories;

public interface IOrdersRepository
{
	/// <summary>
	/// Создание заказа.
	/// </summary>
	/// <param name="orderEntity"></param>
	/// <returns>Success</returns>
	Task<bool> CreateOrder(OrderEntity orderEntity);
}
