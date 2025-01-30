using Microsoft.EntityFrameworkCore;

using Shop.Model.Database.Entities;

namespace Shop.Server.Entities;

public class OrderContext : DbContext
{
	public DbSet<OrderEntity> Orders { get; set; }

	public OrderContext(DbContextOptions<OrderContext> options) : base(options)
	{
	}
}
