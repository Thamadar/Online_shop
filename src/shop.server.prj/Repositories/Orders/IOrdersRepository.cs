
using Shop.Dto.Orders; 

namespace Shop.Server.Repositories;

public interface IOrdersRepository
{      
	/// <summary>
	/// Получение всех заказов.
	/// </summary> 
	Task<GetOrdersDto> GetOrdersAsync();

	/// <summary>
	/// Получение всех заказов пользователя по его userId.
	/// </summary> 
	Task<GetOrdersDto> GetOrdersByUserIdAsync(Guid userId);

	/// <summary>
	/// Получение заказа по ID заказа.
	/// </summary> 
	Task<GetOrderDto> GetOrderByIdAsync(Guid orderId);

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
