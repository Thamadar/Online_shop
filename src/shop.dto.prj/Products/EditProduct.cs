using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.Products;

public record EditProductDiscountRequest( 
	[Required] decimal DiscountValue,
	[Required] DiscountUnit DiscountUnit);
