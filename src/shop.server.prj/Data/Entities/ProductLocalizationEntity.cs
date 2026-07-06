using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shop.Server.Data;

/// <summary>
/// Объект, инкапсулирующий локализацию для товара (ProductEntity).
/// </summary> 
public class ProductLocalizationEntity
{  
	public int ProductId { get; set; }

	/// <summary>
	/// Ключ локализации.
	/// </summary>  
	[Required]
	[MaxLength(10)]
	public string LangCode { get; set; }  

	/// <summary>
	/// Отображение локализации.
	/// </summary>
	[Required] 
	public string DisplayName { get; set; }
	 
	[ForeignKey(nameof(ProductId))]
	[JsonIgnore]
	public ProductEntity Product { get; set; }
}
