
using AutoMapper;
using Microsoft.AspNetCore.Mvc; 
using Shop.Server.Data; 
using Shop.Server.Services.API;
using Shop.Dto;
using Shop.Utilities;
using Shop.Dto.Orders;

namespace Shop.Server.Controllers;

/// <summary>
/// Контроллер заказов.
/// TO DO: доделать пустые запросы.
/// </summary>
[ApiController]
[Route(HttpConstants.orders)]
public sealed class OrdersController : ShopControllerBase
{
	private readonly IOrdersAPIService _ordersAPIService;

	public OrdersController(IMapper mapper, IOrdersAPIService ordersAPIService) : base(mapper)
	{
		_ordersAPIService = ordersAPIService;
	} 

	/// <summary>
	/// Получение всех заказов.
	/// </summary> 
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetOrdersResponse>> GetOrders()
	{
		try
		{
			var orders = await _ordersAPIService.GetOrdersAsync();

			return Ok(orders);
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
	[HttpGet("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetOrderResponse>> GetOrderById([FromRoute] Guid id)
	{
		try
		{
			var order = await _ordersAPIService.GetOrderByIdAsync(id);

			return Ok(order);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(GetOrderById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Получение заказов по id пользователя.
	/// </summary>  
	//var url = @$"{HttpConstants.orders}by-user/{Uri.EscapeDataString(userId)}";
	[HttpGet("by-user/{userId}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetOrdersResponse>> GetOrderByUserId([FromRoute] Guid userId)
	{
		try
		{
			var orders = await _ordersAPIService.GetOrdersByUserIdAsync(userId);

			return Ok(orders);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(GetOrderByUserId)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Создание заказов.
	/// TO DO: удалить, ибо излишка, т.к. нет сценариев пока использования этого запроса?
	/// </summary> 
	[HttpPost("batch")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateOrdersResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<CreateOrdersResponse>> PostOrders([FromBody] CreateOrdersRequest parameters)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);
		try
		{
			return StatusCode(StatusCodes.Status404NotFound);
			//var ordersResponse = await _ordersAPIService.CreateOrdersAsync(parameters);

			//return Ok(ordersResponse);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(PostOrders)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Создание заказа.
	/// </summary> 
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateOrderResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<CreateOrderResponse>> PostOrder([FromBody] CreateOrderRequest parameters)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);
		try
		{
			var orderResponse = await _ordersAPIService.CreateOrderAsync(parameters);

			return Ok(orderResponse);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(PostOrder)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Успешное завершение заказа.
	/// </summary> 
	[HttpPost("{id:guid}/complete")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> CompleteOrder([FromRoute] Guid id)
	{
		try
		{
			await _ordersAPIService.CompletionOrderAsync(id);
			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(CompleteOrder)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Отмена заказа.
	/// </summary> 
	[HttpPost("{id:guid}/cancel")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> CancelOrder([FromRoute] Guid id)
	{
		try
		{
			await _ordersAPIService.CancellationOrderAsync(id);
			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(CancelOrder)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Обновление заказа.
	/// </summary> 
	[HttpPut("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> UpdateOrderById(
		[FromRoute] int id)
	{
		try
		{
			//TO DO:
			return StatusCode(StatusCodes.Status404NotFound);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersController)}.{nameof(UpdateOrderById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	} 
}

