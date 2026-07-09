using Shop.Dto;
using Shop.Dto.Users;
using Shop.Server.Services.API.Interfaces;

namespace Shop.Server.Services.API;

public interface IUsersAPIService : IAPIService
{

	/// <summary>
	/// Получение всех пользователей.
	/// </summary> 
	Task<GetUsersDto> GetUsersAsync(CancellationToken ct = default);

	/// <summary>
	/// Получение пользователя по Id.
	/// </summary> 
	Task<GetUserDto> GetUserByIdAsync(Guid id, CancellationToken ct = default);

	/// <summary>
	/// Получение пользователя по Login.
	/// </summary> 
	Task<GetUserDto> GetUserByLoginAsync(string login, CancellationToken ct = default);

	/// <summary>
	/// Создание пользователя.
	/// </summary> 
	Task<CreateUserResponse> CreateUserAsync(CreateUserRequest createUserRequest);
	/// <summary>
	/// Создание пользователей.
	/// </summary> 
	Task<CreateUsersResponse> CreateUsersAsync(CreateUsersRequest createUsersRequest);

	/// <summary>
	/// Редактирование пользователя.
	/// </summary> 
	Task<EditUserResponse> EditUserAsync(EditUserRequest editUserRequest);

	/// <summary>
	/// Удаление пользователя.
	/// </summary> 
	Task DeleteUserAsync(Guid userId);

	/// <summary>
	/// Удаление пользователей.
	/// </summary> 
	Task<List<BatchError>> DeleteUsersAsync(List<Guid> usersId);

}
