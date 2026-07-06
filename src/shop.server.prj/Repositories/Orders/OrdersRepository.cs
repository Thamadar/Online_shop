using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Dto.Orders; 
using Shop.Server.Data;

namespace Shop.Server.Repositories;

public class OrdersRepository : IOrdersRepository
{ 
	private readonly IMapper _mapper;
	private readonly ServerContext _serverContext; 

	public OrdersRepository(IMapper mapper, ServerContext serverContext)
	{
		_mapper        = mapper;
		_serverContext = serverContext; 
	}

	/// <inheritdoc/>
	public async Task<GetOrdersResponse> GetOrdersAsync()
	{
		try
		{
			var orderResponseList = await _serverContext.Orders
				.AsNoTracking()
				.ProjectTo<GetOrderResponse>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return new GetOrdersResponse(orderResponseList);
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException("Не удалось получить список заказов", ex);
		} 
	}

	/// <inheritdoc/>
	public async Task<GetOrdersResponse> GetOrdersByUserIdAsync(Guid userId)
	{
		try
		{
			var orderResponseList = await _serverContext.Orders
				.AsNoTracking()
				.Where(x => x.UserId == userId)
				.ProjectTo<GetOrderResponse>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return new GetOrdersResponse(orderResponseList);
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось получить список заказов пользователя: {userId}", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<GetOrderResponse> GetOrderByIdAsync(Guid orderId)
	{
		try
		{
			var orderEntity = await _serverContext.Orders
				.FindAsync(orderId);

			return _mapper.Map<GetOrderResponse>(orderEntity);
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось получить заказ: {orderId}", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest createOrderRequest)
	{
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var entity = _mapper.Map<OrderEntity>(createOrderRequest); 
			await _serverContext.Orders.AddAsync(entity);
			await _serverContext.SaveChangesAsync();

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
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{ 
			var entities = _mapper.Map<List<OrderEntity>>(createOrdersRequest.Orders);
			await _serverContext.Orders.AddRangeAsync(entities);
			await _serverContext.SaveChangesAsync();

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
	public async Task<List<OrderProductResponse>> CompletionOrderAsync(Guid orderId)
	{
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var entity = await _serverContext.Orders.FindAsync(orderId);

			if(entity != null)
			{
				entity.Complete();

				var orderProducts = _mapper.Map<List<OrderProductResponse>>(entity.OrderProducts);
				entity.OrderProducts.Clear();

				await _serverContext.SaveChangesAsync();
				await transaction.CommitAsync();

				return orderProducts;
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
	public async Task<List<OrderProductResponse>> CancellationOrderAsync(Guid orderId)
	{
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var entity = await _serverContext.Orders.FindAsync(orderId);

			if(entity != null)
			{
				entity.Cancel();

				var orderProducts = _mapper.Map<List<OrderProductResponse>>(entity.OrderProducts);
				entity.OrderProducts.Clear();

				await _serverContext.SaveChangesAsync();
				await transaction.CommitAsync();

				return orderProducts; 
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

