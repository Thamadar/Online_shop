using Shop.Dto.Users;
using Shop.Server.Data;

namespace Shop.Server.Repositories;

public interface IUsersRepository
{
	/// <summary>
	/// Получение всех пользователей.
	/// </summary> 
	Task<GetUsersResponse> GetUsers();  
	/// <summary>
	/// Получение пользователя по id.
	/// </summary> 
	Task<UserEntity> GetUserById(Guid id);

	/// <summary>
	/// КОСТЫЛЬ, ИБО НЕТ аутентификации (AuthController). - необходимо.
	/// </summary> 
	Task<UserEntity> GetUserByLogin(string login);

	/// <summary>
	/// Добавление пользователя.
	/// </summary> 
	Task<UserEntity> CreateUser(CreateUserRequest request);

	/// <summary>
	/// Добавление пользователя.
	/// </summary> 
	Task<List<UserEntity>> CreateUsersBatch(List<UserEntity> usersBatch);

	/// <summary>
	/// Существует ли какой-либо пользователь с данным логином?
	/// </summary> 
	bool IsAnyLoginExists(string login);
}
