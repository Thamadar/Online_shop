using Shop.Model.Database.Entities;
using Shop.Model;
using Shop.Server.Repositories;

using Microsoft.AspNetCore.Mvc; 

namespace Shop.Server.Controllers;

/// <summary>
/// Контроллер заказов.
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
	/// Создание заказа.
	/// </summary> 
	[HttpPost(HttpConstants.postCreateOrderTask, Name = nameof(CreateOrder))] 
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> CreateOrder([FromBody] OrderEntity orderEntity)
	{
		try
		{
			var orderSuccess = await _ordersRepository.CreateOrder(orderEntity); 

			return Ok(orderSuccess);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(CreateOrder)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}
}

