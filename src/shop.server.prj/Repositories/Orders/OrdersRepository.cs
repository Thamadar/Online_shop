using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Dto.Orders;
using Shop.Dto.Users;
using Shop.Server.Data;

namespace Shop.Server.Repositories;

public class OrdersRepository : IOrdersRepository
{ 
	private readonly IMapper _mapper;
	private readonly OrderContext _orderContext;

	public OrdersRepository(IMapper mapper, OrderContext orderContext)
	{
		_mapper = mapper;
		_orderContext = orderContext;
	}
	  
	/// <inheritdoc/>
	public async Task<GetOrdersDto> GetOrdersAsync()
	{
		try
		{
			var orderResponseList = await _orderContext.Orders
				.AsNoTracking()
				.ProjectTo<GetOrderDto>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return new GetOrdersDto(orderResponseList);
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException("Не удалось получить список заказов", ex);
		} 
	}

	/// <inheritdoc/>
	public async Task<GetOrdersDto> GetOrdersByUserIdAsync(Guid userId)
	{
		try
		{
			var orders = await _orderContext.Orders
				.AsNoTracking()
				.Where(x => x.UserId == userId)
				.ProjectTo<GetOrdersDto>(_mapper.ConfigurationProvider)
				.FirstAsync();

			return orders;
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось получить список заказов пользователя: {userId}", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<GetOrderDto> GetOrderByIdAsync(Guid orderId)
	{
		try
		{
			var orderEntity = await _orderContext.Orders
				.FindAsync(orderId);

			return _mapper.Map<GetOrderDto>(orderEntity);
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось получить заказ: {orderId}", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest createOrderRequest)
	{
		await using var transaction = await _orderContext.Database.BeginTransactionAsync();
		try
		{
			var entity = _mapper.Map<OrderEntity>(createOrderRequest);
			await _orderContext.Orders.AddAsync(entity);
			await _orderContext.SaveChangesAsync();

			var orderResponse = _mapper.Map<CreateOrderResponse>(entity);

			await transaction.CommitAsync();

			return orderResponse;
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не удалось создать заказ", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<CreateOrdersResponse> CreateOrdersAsync(CreateOrdersRequest createOrdersRequest)
	{
		await using var transaction = await _orderContext.Database.BeginTransactionAsync();
		try
		{ 
			var entities = _mapper.Map<List<OrderEntity>>(createOrdersRequest.Orders);
			await _orderContext.Orders.AddRangeAsync(entities);
			await _orderContext.SaveChangesAsync();

			var ordersResponse = new CreateOrdersResponse(_mapper.Map<List<CreateOrderResponse>>(entities)); 

			await transaction.CommitAsync();

			return ordersResponse;
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не удалось создать список заказов", ex);
		}
	}

	/// <inheritdoc/>
	public async Task CompletionOrderAsync(Guid orderId)
	{
		await using var transaction = await _orderContext.Database.BeginTransactionAsync();
		try
		{
			var entity = await _orderContext.Orders.FindAsync(orderId);

			if(entity != null)
			{
				entity.Complete(); 
				await transaction.CommitAsync(); 
			}
			else
			{
				throw new InvalidOperationException("Не удалось завершить заказ");
			}
		}   
		catch
		{
			await transaction.RollbackAsync();
			throw;
		}
	}

	/// <inheritdoc/>
	public async Task CancellationOrderAsync(Guid orderId)
	{
		await using var transaction = await _orderContext.Database.BeginTransactionAsync();
		try
		{
			var entity = await _orderContext.Orders.FindAsync(orderId);

			if(entity != null)
			{
				entity.Cancel();
				await transaction.CommitAsync();
			}
			else
			{
				throw new InvalidOperationException("Не удалось отменить заказ");
			}
		}
		catch
		{
			await transaction.RollbackAsync();
			throw;
		}
	}
}

