using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shop.Server.Data;

public class OrderProductEntity
{   
	public Guid OrderId { get; private set; }
	  
	public int ProductId { get; private set; }

	[Required] 
	public int Quantity { get; set; } = 1;
	 
	[ForeignKey(nameof(OrderId))]
	[JsonIgnore]
	public OrderEntity OrderEntity { get; private set; }

	[ForeignKey(nameof(ProductId))]
	[JsonIgnore]
	public ProductEntity ProductEntity { get; private set; }
}
