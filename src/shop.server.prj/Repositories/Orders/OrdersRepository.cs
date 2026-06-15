using Microsoft.EntityFrameworkCore;
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
	public async Task<IEnumerable<OrderEntity>> GetOrders()
	{
		return await _orderContext.Orders.ToListAsync();
	}

	/// <inheritdoc/>
	public async Task<OrderEntity?> GetOrderById(Guid id)
	{
		return await _orderContext.Orders
		.Where(x => x.Id == id)
		.FirstOrDefaultAsync();
	}

	/// <inheritdoc/>
	public async Task<bool> PostOrders(OrderEntity[] orderEntities)
	{ 
		foreach(var entity in orderEntities)
		{
			if(entity.Products == null || entity.UserId == default(Guid))
			{
				return false;
			} 
		} 

		await _orderContext.Orders.AddRangeAsync(orderEntities);
		await _orderContext.SaveChangesAsync();

		return true;
	}    
}

