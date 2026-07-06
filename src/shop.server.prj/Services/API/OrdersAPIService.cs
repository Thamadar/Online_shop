
using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore.Storage;
using Shop.Dto.Orders;
using Shop.Server.Data;
using Shop.Server.Repositories;
using System.Transactions;

namespace Shop.Server.Services.API;

public class OrdersAPIService : IOrdersAPIService
{
	private readonly IMapper _mapper;

	private readonly IOrdersRepository _ordersRepository;
	private readonly IProductsRepository _productsRepository;

	public OrdersAPIService(
		IMapper mapper,
		IOrdersRepository ordersRepository,
		IProductsRepository productsRepository)
	{
		_mapper             = mapper;
		_ordersRepository   = ordersRepository;
		_productsRepository = productsRepository;
	}

	/// <inheritdoc/>
	public async Task<GetOrdersResponse> GetOrdersAsync()
	{
		var ordersResponse = await _ordersRepository.GetOrdersAsync();
		return ordersResponse;
	}


	/// <inheritdoc/>
	public async Task<GetOrdersResponse> GetOrdersByUserIdAsync(Guid userId)
	{ 
		var orderResponse = await _ordersRepository.GetOrdersByUserIdAsync(userId);
		return orderResponse;
	}

	/// <inheritdoc/>
	public async Task<GetOrderResponse> GetOrderByIdAsync(Guid orderId)
	{
		var orderResponse = await _ordersRepository.GetOrderByIdAsync(orderId);
		return orderResponse;
	}

	/// <inheritdoc/>
	public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest createOrderRequest)
	{
		//TO DO: лучше обработать логику ошибок, дабы в случае ошибки, вернулось все в исходное состояние, без потери данных.
		try
		{
			foreach(var orderProductRequest in createOrderRequest.OrderProducts)
			{
				var isAvailable = await _productsRepository.CheckAvailabilityQuantityAsync(
					orderProductRequest.ProductId,
					orderProductRequest.Quantity);

				if(!isAvailable)
					throw new InvalidOperationException(
						$"Товар {orderProductRequest.ProductId} недоступен в количестве {orderProductRequest.Quantity}");
			} 

			var orderResponse = await _ordersRepository.CreateOrderAsync(createOrderRequest);
			foreach(var orderProductRequest in createOrderRequest.OrderProducts)
			{
				await _productsRepository.RemoveQuantityAsync(orderProductRequest.ProductId, orderProductRequest.Quantity);
			}

			return orderResponse;
		}
		catch
		{ 
			throw;
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
		var orderProductResponses = await _ordersRepository.CompletionOrderAsync(orderId);

		foreach(var orderProductResponse in orderProductResponses)
		{
			await _productsRepository.RemoveQuantityAsync(orderProductResponse.ProductId, orderProductResponse.Quantity);
		}
	}

	/// <inheritdoc/>
	public async Task CancellationOrderAsync(Guid orderId)
	{
		var orderProductResponses = await _ordersRepository.CancellationOrderAsync(orderId);

		foreach(var orderProductResponse in orderProductResponses)
		{
			await _productsRepository.RemoveQuantityAsync(orderProductResponse.ProductId, orderProductResponse.Quantity);
		}
	}

	/// <inheritdoc/>
	public void Initialization()
	{ }
}
