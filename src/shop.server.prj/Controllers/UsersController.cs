using AutoMapper;
using Microsoft.AspNetCore.Mvc;  
using Shop.Server.Services.API;
using Shop.Dto.Users;
using Shop.Dto;
using Shop.Utilities;

namespace Shop.Server.Controllers;

/// <summary>
/// Контроллер заказов.
/// TO DO: доделать пустые запросы.
/// </summary>
[ApiController]
[Route(HttpConstants.users)]
public sealed class UsersController : ShopControllerBase
{
	private readonly IUsersAPIService _usersAPIService;

	public UsersController(IMapper mapper, IUsersAPIService usersAPIService) : base(mapper)
	{
		_usersAPIService = usersAPIService;
	} 

	/// <summary>
	/// Получение всех пользователей.
	/// </summary> 
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetUsersResponse>> GetUsers()
	{
		try
		{ 
			var users = await _usersAPIService.GetUsers();

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
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserItemDto))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetUserItemDto?>> GetUserById([FromRoute] Guid guid)
	{
		try
		{
			var user = await _usersAPIService.GetUserById(guid);

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
	[HttpGet("by-login/{login}&{}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserItemDto))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetUserItemDto?>> GetUserByLogin(string login)
	{
		try
		{
			var user = await _usersAPIService.GetUserByLogin(login);
			

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
	[HttpPost("batch")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<CreateUserResponse>))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<CreateUsersResponse>> PostUsers([FromBody] CreateUsersRequest request)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			var usersRepsonse = await _usersAPIService.CreateUsers(request);

			return Ok(usersRepsonse); 
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
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> UpdateUsers([FromBody] EditUserItemRequest parameters)
	{
		try
		{
			//var userSuccess = await _usersAPIService.UpdateUsers(parameters);
			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(UpdateUsers)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}


	/// <summary>
	/// Удаление пользователя.
	/// </summary> 
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
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
