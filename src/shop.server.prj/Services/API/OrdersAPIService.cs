
using AutoMapper;
using Azure.Core;
using Shop.Dto.Orders;
using Shop.Server.Repositories;

namespace Shop.Server.Services.API;

public class OrdersAPIService : IOrdersAPIService
{
	private readonly IOrdersRepository _ordersRepository;
	private readonly IProductsRepository _productsRepository;
	private readonly IMapper _mapper;

	public OrdersAPIService(IMapper mapper, IOrdersRepository ordersRepository, IProductsRepository productsRepository)
	{
		_mapper             = mapper;
		_ordersRepository   = ordersRepository;
		_productsRepository = productsRepository;
	}

	/// <inheritdoc/>
	public async Task<GetOrdersDto> GetOrdersAsync()
	{
		var ordersResponse = await _ordersRepository.GetOrdersAsync();
		return ordersResponse;
	}


	/// <inheritdoc/>
	public async Task<GetOrdersDto> GetOrdersByUserIdAsync(Guid userId)
	{ 
		var orderResponse = await _ordersRepository.GetOrdersByUserIdAsync(userId);
		return orderResponse;
	}

	/// <inheritdoc/>
	public async Task<GetOrderDto> GetOrderByIdAsync(Guid orderId)
	{
		var orderResponse = await _ordersRepository.GetOrderByIdAsync(orderId);
		return orderResponse;
	}

	/// <inheritdoc/>
	public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest createOrderRequest)
	{
		var orderResponse = await _ordersRepository.CreateOrderAsync(createOrderRequest);
		return orderResponse;
	}

	/// <inheritdoc/>
	public async Task<CreateOrdersResponse> CreateOrdersAsync(CreateOrdersRequest createOrdersRequest)
	{ 
		var ordersResponse = await _ordersRepository.CreateOrdersAsync(createOrdersRequest);
		return ordersResponse;
	} 

	/// <inheritdoc/>
	public async Task CompletionOrderAsync(Guid orderId)
	{
		await _ordersRepository.CompletionOrderAsync(orderId);
		//TO DO: удалить кол-во товаров из БД, входящих в заказ.
	}

	/// <inheritdoc/>
	public async Task CancellationOrderAsync(Guid orderId)
	{
		await _ordersRepository.CancellationOrderAsync(orderId);
	}

	/// <inheritdoc/>
	public void Initialization()
	{ }
}
