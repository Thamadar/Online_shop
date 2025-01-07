
using Microsoft.EntityFrameworkCore;
using Shop.Model.Bracket;

namespace Shop.Model.Database.Contexts;

public class OnlineShopContext : DbContext
{
	public DbSet<ProductEntity> Products { get; set; }  
}
