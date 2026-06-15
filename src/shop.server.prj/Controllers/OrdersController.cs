using Shop.Model.Database.Entities;
using Shop.Model;
using Shop.Server.Repositories;

using Microsoft.AspNetCore.Mvc; 

namespace Shop.Server.Controllers;

/// <summary>
/// Контроллер заказов.
/// TO DO: доделать пустые запросы.
/// </summary>
[ApiController]
[Route(HttpConstants.orders)]
public sealed class OrdersController : ControllerBase
{
	private readonly IOrdersRepository _ordersRepository;

	public OrdersController(IOrdersRepository ordersRepository)
	{
		_ordersRepository = ordersRepository;
	} 
	 
	/// <summary>
	/// Получение всех заказов.
	/// </summary> 
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderEntity[]))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<OrderEntity[]?>> GetOrders()
	{
		try
		{ 

			return Ok( );
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(GetOrders)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Получение заказа по id.
	/// </summary> 
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderEntity))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<OrderEntity?>> GetOrderById([FromRoute] int id)
	{
		try
		{ 
			return Ok(null);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(GetOrderById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Создание заказов.
	/// </summary> 
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> PostOrders([FromBody] OrderEntity[] parameters)
	{
		try
		{
			var orderSuccess = await _ordersRepository.PostOrders(parameters);

			return Ok(orderSuccess);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(PostOrders)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	} 
	/// <summary>
	/// Обновление заказа.
	/// </summary> 
	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> UpdateOrderById(
		[FromRoute] int id,
		[FromBody] OrderEntity parameters)
	{
		try
		{

			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(UpdateOrderById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}


	/// <summary>
	/// Удаление заказа.
	/// </summary> 
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> DeleteOrder([FromRoute] int id)
	{
		try
		{

			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(DeleteOrder)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}
}

