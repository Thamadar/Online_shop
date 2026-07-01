using System.ComponentModel.DataAnnotations; 

namespace Shop.Dto.Products;

public record ProductLocalizationRequest(
	[Required][StringLength(10)]  string LangCode,
	[Required][StringLength(150)] string DisplayName);

public record ProductLocalizationResponse(
	int ProductId,
	string LangCode,
	string DisplayName);
