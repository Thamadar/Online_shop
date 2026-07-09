
using AutoMapper; 
using Shop.Dto.Orders; 

namespace Shop.Server.Services.API;

public class OrdersAPIService : IOrdersAPIService
{
	private readonly IMapper _mapper; 
	private readonly IUnitOfWork _unitOfWork; 

	public OrdersAPIService(
		IMapper mapper,
		IUnitOfWork unitOfWork)
	{
		_mapper     = mapper; 
		_unitOfWork = unitOfWork;
	}

	/// <inheritdoc/>
	public async Task<GetOrdersResponse> GetOrdersAsync(CancellationToken ct = default)
	{
		var ordersResponse = await _unitOfWork.Orders.GetOrdersAsync(ct);
		return ordersResponse;
	}


	/// <inheritdoc/>
	public async Task<GetOrdersResponse> GetOrdersByUserIdAsync(Guid userId, CancellationToken ct = default)
	{ 
		var orderResponse = await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId, ct);
		return orderResponse;
	}

	/// <inheritdoc/>
	public async Task<GetOrderResponse> GetOrderByIdAsync(Guid orderId, CancellationToken ct = default)
	{
		var orderResponse = await _unitOfWork.Orders.GetOrderByIdAsync(orderId, ct);
		return orderResponse;
	}

	/// <inheritdoc/>
	public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest createOrderRequest)
	{
		await using var transaction = await _unitOfWork.BeginTransactionAsync(); 
		try
		{
			foreach(var orderProductRequest in createOrderRequest.OrderProducts)
			{
				var isAvailable = await _unitOfWork.Products.CheckAvailabilityQuantityAsync(
					orderProductRequest.ProductId,
					orderProductRequest.Quantity);

				if(!isAvailable)
					throw new InvalidOperationException(
						$"Товар {orderProductRequest.ProductId} недоступен в количестве {orderProductRequest.Quantity}");
			} 

			var orderResponse = await _unitOfWork.Orders.CreateOrderAsync(createOrderRequest);
			foreach(var orderProductRequest in createOrderRequest.OrderProducts)
			{
				await _unitOfWork.Products.RemoveQuantityAsync(orderProductRequest.ProductId, orderProductRequest.Quantity);
			}

			await transaction.CommitAsync();

			return orderResponse;
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не создать завершить заказ", ex);
		}
	}

	/// <inheritdoc/>
	//public async Task<CreateOrdersResponse> CreateOrdersAsync(CreateOrdersRequest createOrdersRequest)
	//{ 
	//	var ordersResponse = await _ordersRepository.CreateOrdersAsync(createOrdersRequest);
	//	return ordersResponse;
	//} 

	/// <inheritdoc/>
	public async Task CompletionOrderAsync(Guid orderId)
	{
		await using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{ 
			var orderProductResponses = await _unitOfWork.Orders.CompletionOrderAsync(orderId);

			foreach(var orderProductResponse in orderProductResponses)
			{
				await _unitOfWork.Products.RemoveQuantityAsync(orderProductResponse.ProductId, orderProductResponse.Quantity);
			}

			await transaction.CommitAsync();
		}
		catch(Exception ex)
		{ 
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не удалось завершить заказ", ex);
		}
	}

	/// <inheritdoc/>
	public async Task CancellationOrderAsync(Guid orderId)
	{
		await using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{
			var orderProductResponses = await _unitOfWork.Orders.CancellationOrderAsync(orderId);

			foreach(var orderProductResponse in orderProductResponses)
			{
				await _unitOfWork.Products.RemoveQuantityAsync(orderProductResponse.ProductId, orderProductResponse.Quantity);
			}

			await transaction.CommitAsync();
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не отменить завершить заказ", ex);
		}
	}

	/// <inheritdoc/>
	public void Initialization()
	{ }
}
