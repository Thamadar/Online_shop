using Microsoft.EntityFrameworkCore;
using Shop.Model.Database.Entities;

namespace Shop.Server.Entities;

public class ProductContext : DbContext
{
	public DbSet<ProductEntity> Products { get; set; }

	public ProductContext(DbContextOptions<ProductContext> options) : base(options)
	{
	}
}
