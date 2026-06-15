using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Model;
using Shop.Model.Database.Entities;
using Shop.Server.Repositories;

namespace Shop.Server.Controllers;

/// <summary>
/// Контроллер заказов.
/// TO DO: доделать пустые запросы.
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

	///// <summary>
	///// Получение адреса пользователя по Id.
	///// </summary> 
	//[HttpGet("{id}")]
	//[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
	//[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	//public async Task<IActionResult> GetUserAddressById(Guid guid)
	//{
	//	try
	//	{
	//		var userAddress = await _usersRepository.GetAddressById(guid);

	//		return Ok(userAddress);
	//	}
	//	catch(Exception exc)
	//	{
	//		ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(GetUserAddressById)} failed: {exc}");
	//		return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
	//	}
	//} 

	/// <summary>
	/// Получение всех пользователей.
	/// </summary> 
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserEntity>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<IEnumerable<UserEntity>>> GetUsers()
	{
		try
		{ 
			var users = await _usersRepository.GetUsers();

			return Ok(users);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(GetUsers)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Получение пользователя по логину (КОСТЫЛЬ, ИБО НЕТ аутентификации. - необходимо.)
	/// TO DO: authController.
	/// </summary> 
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserEntity))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<UserEntity?>> GetUserById([FromRoute] Guid guid)
	{
		try
		{
			var user = await _usersRepository.GetUserById(guid);

			return Ok(user);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(GetUserById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Получение пользователя по логину (КОСТЫЛЬ, ИБО НЕТ аутентификации. - необходимо.)
	/// TO DO: authController.
	/// </summary> 
	[HttpGet("by-login")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserEntity))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<UserEntity?>> GetUserByLogin([FromQuery] string login)
	{
		try
		{
			var user = await _usersRepository.GetUserByLogin(login);

			return Ok(user);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(GetUserByLogin)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Создание пользователей.
	/// </summary> 
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> PostUsers([FromBody] UserEntity[] parameters)
	{
		try
		{
			var userSuccess = await _usersRepository.PostUsers(parameters);

			return Ok(); 
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(PostUsers)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Обновление пользователя.
	/// </summary> 
	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> UpdateUserById(
		[FromRoute] int id,
		[FromBody] UserEntity parameters)
	{
		try
		{
			//var userSuccess = await _usersRepository.UpdateUser(parameters);
			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(UpdateUserById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}


	/// <summary>
	/// Удаление пользователя.
	/// </summary> 
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> DeleteUser([FromRoute] int id)
	{
		try
		{

			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(DeleteUser)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	} 
}
