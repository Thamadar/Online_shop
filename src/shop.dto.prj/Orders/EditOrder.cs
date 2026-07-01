using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.Orders;
 
public record EditOrderRequest(
	[Required] Guid Id,
	[Required] string NewAddress); 
public record EditOrderResponse(
	Guid Id,
	string NewAddress,
	DateTime UpdatedAt); // TO DO: можно добавить опцию изменить сам заказ.
