using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.Products;

//public record GetProductsRequest(
//	[Required] List<GetProductRequest> Products);
//public record GetProductRequest(
//	[Required][StringLength(150)] string ProductName);

public record GetProductsResponse(
	[Required] List<GetProductResponse> Products);
public record GetProductResponse(
	int Id,
	string ProductName,
	IReadOnlyList<ProductLocalizationResponse> Localizations,
	int CurrentCount,
	decimal BasePrice,
	CreateProductDiscountDto Discount,
	decimal ResultPrice,
	int Weight,
	byte[] Image // TO DO: по-хорошему бы вынести, сделав отдельным запросом.
	);
