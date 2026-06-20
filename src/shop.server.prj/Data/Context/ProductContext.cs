using Microsoft.EntityFrameworkCore; 
 
namespace Shop.Server.Data;

public class ProductContext : DbContext
{
	public DbSet<ProductEntity> Products { get; set; } 
	public DbSet<ProductLocalizationEntity> ProductsLocalization { get; set; }

	public ProductContext(DbContextOptions<ProductContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{ 
		modelBuilder.Entity<ProductLocalizationEntity>()
			.HasKey(l => new { l.ProductId, l.LangCode });

		modelBuilder.Entity<ProductEntity>()
			.HasMany(p => p.Localizations)
			.WithOne(l => l.Product)
			.HasForeignKey(l => l.ProductId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
