using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.Products;
 
public record CreateProductDiscountDto(
	[Required] decimal DiscountValue,
	[Required] DiscountUnit DiscountUnit);

public record CreateProductsRequest([Required][MinLength(1)] List<CreateProductRequest> Products); 

public record CreateProductRequest(
	[Required][StringLength(150)] string ProductName,
	[Required][MinLength(1)] IReadOnlyList<ProductLocalizationRequest> Localizations,
	[Required] int AvailableCount,
	[Required] decimal BasePrice,
	[Required] CreateProductDiscountDto Discount,
	[Required] int Weight,
	[Required] byte[] Image);

public record CreateProductsResponse(List<CreateProductResponse> Created);
public record CreateProductResponse(
	int Id,
	string ProductName,
	IReadOnlyList<ProductLocalizationResponse> Localizations,
	int AvailableCount,
	decimal BasePrice,
	CreateProductDiscountDto Discount,
	decimal ResultPrice,
	int Weight,
	byte[] Image // TO DO: по-хорошему бы вынести, сделав отдельным запросом.
	);
