using Shop.Model.Database.Entities;

namespace Shop.Server.Repositories;

public interface IUsersRepository
{
	/// <summary>
	/// Получение всех пользователей.
	/// </summary> 
	Task<IEnumerable<UserEntity>> GetUsers();

	/// <summary>
	/// Добавление пользователей.
	/// </summary> 
	Task<bool> PostUsers(UserEntity[] userEntities);

	/// <summary>
	/// Получение пользователя по id.
	/// </summary> 
	Task<UserEntity?> GetUserById(Guid id);

	/// <summary>
	/// КОСТЫЛЬ, ИБО НЕТ аутентификации (AuthController). - необходимо.
	/// </summary> 
	Task<UserEntity?> GetUserByLogin(string login);
}
