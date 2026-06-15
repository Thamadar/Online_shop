using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shop.Model.Database.Entities;

/// <summary>
/// Объект, инкапсулирующий локализацию для товара (ProductEntity).
/// </summary> 
public class ProductLocalizationEntity
{
	[Key]
	[Column(Order = 0)]
	public int ProductId { get; set; }

	/// <summary>
	/// Ключ локализации.
	/// </summary>
	[Key]
	[Column(Order = 1)]
	[Required]
	[MaxLength(10)]
	public string LangCode { get; set; }  

	/// <summary>
	/// Отображение локализации.
	/// </summary>
	[Required]
	[Column(Order = 2)]
	public string DisplayName { get; set; }
	 
	[ForeignKey(nameof(ProductId))]
	[JsonIgnore]
	public ProductEntity Product { get; set; }
}
