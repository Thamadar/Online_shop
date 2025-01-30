using Microsoft.EntityFrameworkCore;
using Shop.Model.Database.Entities;

namespace Shop.Server.Entities; 

public class UserContext : DbContext
{
	public DbSet<UserEntity> Users { get; set; }

	public UserContext(DbContextOptions<UserContext> options) : base(options)
	{
	}
}

