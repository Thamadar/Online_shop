using Shop.Model.Database.Entities;

namespace Shop.Server.Repositories;

public interface IUsersRepository
{
	/// <summary>
	/// Получение адреса пользователя по его идентификатору.
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task<string> GetAddressById(Guid id);

	/// <summary>
	/// КОСТЫЛЬ, ИБО НЕТ аутентификации. - необходимо.
	/// </summary> 
	Task<Guid> GetUserIdByLogin(string login);
}
