
using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.Orders;

public record GetOrdersDto(
	[Required] List<GetOrderDto> Orders);
public record GetOrderDto(
	[Required] Guid Id,
	[Required] Guid UserId,
	[Required] string Products, //TO DO: Переделать на ProductOrderEntity...
	[Required] string OrderAddress,
	[Required] DateTime CreatedAt);

