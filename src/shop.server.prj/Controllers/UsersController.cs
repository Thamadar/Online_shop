using Microsoft.AspNetCore.Mvc;
using Shop.Model.Database.Entities;
using Shop.Model;
using Shop.Server.Repositories;

namespace Shop.Server.Controllers;

/// <summary>
/// Контроллер заказов.
/// </summary>
[ApiController]
[Route(HttpConstants.users)]
public sealed class UsersController : ControllerBase
{
	private readonly IUsersRepository _usersRepository;

	public UsersController(IUsersRepository usersRepository)
	{
		_usersRepository = usersRepository;
	}

	/// <summary>
	/// Получение адреса пользователя по Id.
	/// </summary> 
	[HttpPost(HttpConstants.postGetUserAddressByIdTask, Name = nameof(GetUserAddressById))]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> GetUserAddressById([FromBody] Guid guid)
	{
		try
		{
			var userAddress = await _usersRepository.GetAddressById(guid);

			return Ok(userAddress);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(GetUserAddressById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}


	/// <summary>
	/// Получение id пользователя по логину (КОСТЫЛЬ, ИБО НЕТ аутентификации. - необходимо.)
	/// </summary> 
	[HttpPost(HttpConstants.postGetUserIdByLoginTask, Name = nameof(GetUserIdByLogin))]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> GetUserIdByLogin([FromBody] string login)
	{
		try
		{
			var userId = await _usersRepository.GetUserIdByLogin(login);

			return Ok(userId);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(GetUserIdByLogin)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}
}
