using AutoMapper; 
using Microsoft.AspNetCore.Mvc;  
using Shop.Dto;
using Shop.Dto.Users;
using Shop.Server.Services.API;
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
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersDto))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetUsersDto>> GetUsers()
	{
		try
		{ 
			var users = await _usersAPIService.GetUsersAsync(); 
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
	[HttpGet("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserDto))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetUserDto>> GetUserById([FromRoute] Guid guid)
	{
		try
		{
			var user = await _usersAPIService.GetUserByIdAsync(guid); 
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
	[HttpGet("by-login/{login}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserDto))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetUserDto>> GetUserByLogin([FromRoute] string login)
	{
		try
		{
			var user = await _usersAPIService.GetUserByLoginAsync(login); 
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
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<CreateUsersResponse>> PostUsers([FromBody] CreateUsersRequest request)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			var usersRepsonse = await _usersAPIService.CreateUsersAsync(request); 
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
	public async Task<IActionResult> UpdateUsers([FromBody] EditUserRequest request)
	{
		try
		{
			return StatusCode(StatusCodes.Status404NotFound);
			//var response = await _usersAPIService.EditUserAsync(request); 
			//return Ok(response);
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
	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
	{
		try
		{
			return StatusCode(StatusCodes.Status404NotFound);
			//await _usersAPIService.DeleteUserAsync(id); 
			//return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersController)}.{nameof(DeleteUser)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	} 
}
