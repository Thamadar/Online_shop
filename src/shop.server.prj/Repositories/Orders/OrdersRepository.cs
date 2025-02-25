using Shop.Model.Database.Entities;
using Shop.Server.Entities;

namespace Shop.Server.Repositories;

public class OrdersRepository : IOrdersRepository
{ 
	private readonly OrderContext _orderContext; 

	public OrdersRepository(OrderContext orderContext)
	{
		_orderContext = orderContext;
	}

	/// <inheritdoc/>
	public async Task<bool> CreateOrder(OrderEntity orderEntity)
	{
		if(orderEntity.Products == null || orderEntity.UserId == default(Guid))
		{
			return false;
		}

		orderEntity.CreatedAt = DateTime.Now;

		_orderContext.Orders.Add(orderEntity);
		await _orderContext.SaveChangesAsync();

		return true; 
	}
 }

