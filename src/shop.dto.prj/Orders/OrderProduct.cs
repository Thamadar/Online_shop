using System.ComponentModel.DataAnnotations; 

namespace Shop.Dto.Orders;

public record OrderProductRequest(
	[Required] int ProductId,
	[Required] int Quantity);

public record OrderProductResponse(
	Guid OrderId,
	int ProductId,
	int Quantity);
