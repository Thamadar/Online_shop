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
	public async Task<string> GetAddressById(Guid id)
	{
		var user = _userContext.Users.ToList().FirstOrDefault();
		//КОСТЫЛЬ, Т.К. на клиенте нет какой-либо аутентификации.
		return user?.Address ?? "";
		//RIGHT VERSION.
		return _userContext.Users.Where(x => x.Id == id).FirstOrDefault()?.Address ?? "";
	}


	/// <inheritdoc/>
	public async Task<Guid> GetUserIdByLogin(string login)
	{
		return _userContext.Users.ToList().Where(x => x.Login == login).FirstOrDefault()?.Id ?? new Guid(); 
	}
}
