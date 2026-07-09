using Shop.Dto.Users;
using Shop.Server.Data;

namespace Shop.Server.Repositories;

public interface IUsersRepository
{
	/// <summary>
	/// Получение всех пользователей.
	/// </summary> 
	Task<GetUsersDto> GetUsersAsync(CancellationToken ct = default);  
	/// <summary>
	/// Получение пользователя по id.
	/// </summary> 
	Task<GetUserDto> GetUserByIdAsync(Guid id, CancellationToken ct = default);

	/// <summary>
	/// КОСТЫЛЬ, ИБО НЕТ аутентификации (AuthController). - необходимо.
	/// </summary> 
	Task<GetUserDto> GetUserByLoginAsync(string login, CancellationToken ct = default);

	/// <summary>
	/// Добавление пользователя.
	/// </summary> 
	Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);

	/// <summary>
	/// Добавление пользователей.
	/// </summary> 
	Task<CreateUsersResponse> CreateUsersBatchAsync(CreateUsersRequest request);

	/// <summary>
	/// Существует ли какой-либо пользователь с данным логином?
	/// </summary> 
	bool IsAnyLoginExist(string login);

	/// <summary>
	/// Существует ли какой-либо пользователь с каким-либо логином из списка?
	/// </summary> 
	/// <param name="logins">список логинов на проверку</param>
	/// <returns>возвращает список уже существующих логинов</returns>
	List<string> CheckLoginsExist(List<string> logins);
}
