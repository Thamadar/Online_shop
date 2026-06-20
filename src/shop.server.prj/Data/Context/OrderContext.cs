using Microsoft.EntityFrameworkCore; 

namespace Shop.Server.Data;

public class OrderContext : DbContext
{
	public DbSet<OrderEntity> Orders { get; set; }

	public OrderContext(DbContextOptions<OrderContext> options) : base(options)
	{
	}
}
