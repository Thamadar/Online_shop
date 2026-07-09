
using Shop.Dto.Orders; 

namespace Shop.Server.Repositories;

public interface IOrdersRepository
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
	Task<CreateOrdersResponse> CreateOrdersAsync(CreateOrdersRequest createOrdersRequest);

	/// <summary>
	/// Редактирование заказа.
	/// </summary>
	/// TO DO
	//Task<EditOrderResponse> EditOrderAsync(EditOrderRequest editOrderRequest, CancellationToken ct = default);

	/// <summary>
	/// Успешное завершение заказа.
	/// </summary> 
	Task<List<OrderProductResponse>> CompletionOrderAsync(Guid orderId);

	/// <summary>
	/// Отмена заказа.
	/// </summary> 
	Task<List<OrderProductResponse>> CancellationOrderAsync(Guid orderId);
}
