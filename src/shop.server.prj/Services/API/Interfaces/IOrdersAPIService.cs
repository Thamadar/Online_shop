using Shop.Dto;
using Shop.Dto.Orders;
using Shop.Dto.Users;
using Shop.Server.Services.API.Interfaces;

namespace Shop.Server.Services.API;

public interface IOrdersAPIService : IAPIService
{
	/// <summary>
	/// Получение всех заказов.
	/// </summary> 
	Task<GetOrdersResponse> GetOrdersAsync(CancellationToken ct = default);

	/// <summary>
	/// Получение всех заказов пользователя по его userId.
	/// </summary> 
	Task<GetOrdersResponse> GetOrdersByUserIdAsync(Guid userId, CancellationToken ct = default);

	/// <summary>
	/// Получение заказа по ID заказа.
	/// </summary> 
	Task<GetOrderResponse> GetOrderByIdAsync(Guid orderId, CancellationToken ct = default);

	/// <summary>
	/// Создание заказа.
	/// </summary> 
	Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest createOrderRequest);

	/// <summary>
	/// Создание заказов.
	/// </summary> 
	//Task<CreateOrdersResponse> CreateOrdersAsync(CreateOrdersRequest createOrdersRequest);

	/// <summary>
	/// Редактирование заказа.
	/// </summary>
	/// TO DO
	//Task<EditOrderResponse> EditOrderAsync(EditOrderRequest editOrderRequest);
	 
	/// <summary>
	/// Успешное завершение заказа.
	/// </summary> 
	Task CompletionOrderAsync(Guid orderId);

	/// <summary>
	/// Отмена заказа.
	/// </summary> 
	Task CancellationOrderAsync(Guid orderId);  
}
