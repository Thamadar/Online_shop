using Microsoft.EntityFrameworkCore;
using Shop.Model.Database.Entities;
using Shop.Server.Entities;

namespace Shop.Server.Repositories;

public class UsersRepository : IUsersRepository
{
	private readonly UserContext _userContext;

	public UsersRepository(UserContext userContext)
	{
		_userContext = userContext;
	}
	 
	/// <inheritdoc/>
	public async Task<IEnumerable<UserEntity>> GetUsers()
	{
		return await _userContext.Users
			.Select(x => new UserEntity()
			{
			    Id = x.Id,
				Login = x.Login,
				CreatedAt = x.CreatedAt,
			})
			.ToListAsync();
	}

	/// <inheritdoc/>
	public async Task<UserEntity?> GetUserById(Guid id)
	{
		return await _userContext.Users
			.Where(x => x.Id == id)
			.FirstOrDefaultAsync();
	}

	/// <inheritdoc/>
	public async Task<UserEntity?> GetUserByLogin(string login)
	{
		return await _userContext.Users
			.Where(x => x.Login == login)
			.FirstOrDefaultAsync();
	}

	/// <inheritdoc/>
	public async Task<bool> PostUsers(UserEntity[] userEntities)
	{
		foreach(var entity in userEntities)
		{
			if(entity.Id == default(Guid))
			{
				return false;
			} 
		}

		await _userContext.Users.AddRangeAsync(userEntities);
		await _userContext.SaveChangesAsync();

		return true;
	}
}
