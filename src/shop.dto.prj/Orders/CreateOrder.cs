using System.ComponentModel.DataAnnotations; 

namespace Shop.Dto.Orders;
 
public record CreateOrdersRequest([Required][MinLength(1)] List<CreateOrderRequest> Orders);
public record CreateOrderRequest(
	[Required] Guid UserId,
	[Required] string Products, //TO DO: переделать. Добавить ProductOrderEntity
	[Required][StringLength(150)] string OrderAddress);

public record CreateOrdersResponse(List<CreateOrderResponse> Created);
public record CreateOrderResponse(
	Guid Id,
	Guid UserId,
	string Products, //TO DO: переделать. Добавить ProductOrderEntity
	string OrderAddress,
	DateTime CreatedAt);
