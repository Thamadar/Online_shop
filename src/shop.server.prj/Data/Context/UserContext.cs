using Microsoft.EntityFrameworkCore;

namespace Shop.Server.Data;

public class UserContext : DbContext
{
	public DbSet<UserEntity> Users { get; set; }

	public UserContext(DbContextOptions<UserContext> options) : base(options)
	{
	}
}

