using Microsoft.EntityFrameworkCore;
using Shop.Server.Data;

namespace Shop.Server;

public class ServerContext : DbContext
{

	#region DbSet

	public DbSet<OrderEntity> Orders { get; set; }
	public DbSet<OrderProductEntity> OrderProducts { get; set; }

	public DbSet<UserEntity> Users { get; set; }

	public DbSet<ProductEntity> Products { get; set; }
	public DbSet<ProductLocalizationEntity> ProductsLocalization { get; set; }

	#endregion

	public ServerContext(DbContextOptions<ServerContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		#region Products

		modelBuilder.Entity<ProductEntity>()
			.Property(x => x.DiscountUnit)
			.HasConversion<int>()
			.HasColumnType("int");

		modelBuilder.Entity<ProductLocalizationEntity>()
			.HasKey(l => new { l.ProductId, l.LangCode });
		modelBuilder.Entity<ProductEntity>()
			.HasMany(p => p.Localizations)
			.WithOne(l => l.Product)
			.HasForeignKey(l => l.ProductId)
			.OnDelete(DeleteBehavior.Cascade);

		#endregion
		 
		#region Orders

		modelBuilder.Entity<OrderProductEntity>()
			.HasKey(l => new { l.OrderId, l.ProductId });

		modelBuilder.Entity<OrderProductEntity>()
			.HasOne(op => op.OrderEntity)
			.WithMany(o => o.OrderProducts)
			.HasForeignKey(op => op.OrderId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<OrderProductEntity>()
			.HasOne(op => op.ProductEntity)
			.WithMany(p => p.OrderProducts)
			.HasForeignKey(op => op.ProductId)
			.OnDelete(DeleteBehavior.Restrict);

		#endregion

	}
} 
