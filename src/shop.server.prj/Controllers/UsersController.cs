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
	public async Task<ActionResult<GetUsersDto>> GetUsers(CancellationToken ct)
	{ 
		var users = await _usersAPIService.GetUsersAsync(ct); 
		return Ok(users); 
	}

	/// <summary>
	/// Получение пользователя по логину (КОСТЫЛЬ, ИБО НЕТ аутентификации. - необходимо.)
	/// TO DO: authController.
	/// </summary> 
	[HttpGet("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserDto))] 
	public async Task<ActionResult<GetUserDto>> GetUserById([FromRoute] Guid guid, CancellationToken ct)
	{ 
		var user = await _usersAPIService.GetUserByIdAsync(guid, ct); 
		return Ok(user); 
	}

	/// <summary>
	/// Получение пользователя по логину (КОСТЫЛЬ, ИБО НЕТ аутентификации. - необходимо.)
	/// TO DO: authController.
	/// </summary> 
	[HttpGet("by-login/{login}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserDto))] 
	public async Task<ActionResult<GetUserDto>> GetUserByLogin([FromRoute] string login, CancellationToken ct)
	{ 
		var user = await _usersAPIService.GetUserByLoginAsync(login, ct); 
		return Ok(user);  
	}

	/// <summary>
	/// Создание пользователей.
	/// </summary> 
	[HttpPost("batch")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponse))] 
	public async Task<ActionResult<CreateUsersResponse>> PostUsers([FromBody] CreateUsersRequest request)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);
		 
		var usersRepsonse = await _usersAPIService.CreateUsersAsync(request); 
		return Ok(usersRepsonse);  
	}

	/// <summary>
	/// Обновление пользователя.
	/// </summary> 
	[HttpPut]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	public async Task<IActionResult> UpdateUsers([FromBody] EditUserRequest request, CancellationToken ct)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);

		return StatusCode(StatusCodes.Status404NotFound);
		//var response = await _usersAPIService.EditUserAsync(request); 
		//return Ok(response); 
	}


	/// <summary>
	/// Удаление пользователя.
	/// </summary> 
	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))] 
	public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken ct)
	{ 
		return StatusCode(StatusCodes.Status404NotFound);
		//await _usersAPIService.DeleteUserAsync(id); 
		//return Ok(); 
	} 
}
