
using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.Orders;

public record GetOrdersRequest(
	[Required] List<GetOrderRequest> Orders);
public record GetOrderRequest(
	[Required] Guid Id,
	[Required] Guid UserId);

public record GetOrdersResponse(
	[Required] List<GetOrderResponse> Orders);
public record GetOrderResponse(
	[Required] Guid Id,
	[Required] Guid UserId,
	[Required] List<OrderProductResponse> OrderProducts,  
	[Required] string OrderAddress,
	[Required] DateTime CreatedAt);

