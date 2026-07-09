
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
	public async Task<ActionResult<GetOrdersResponse>> GetOrders(CancellationToken ct)
	{ 
		var orders = await _ordersAPIService.GetOrdersAsync(ct); 
		return Ok(orders); 
	}

	/// <summary>
	/// Получение заказа по id.
	/// </summary> 
	[HttpGet("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderResponse))] 
	public async Task<ActionResult<GetOrderResponse>> GetOrderById([FromRoute] Guid id, CancellationToken ct)
	{ 
		var order = await _ordersAPIService.GetOrderByIdAsync(id, ct); 
		return Ok(order); 
	}

	/// <summary>
	/// Получение заказов по id пользователя.
	/// </summary>  
	//var url = @$"{HttpConstants.orders}by-user/{Uri.EscapeDataString(userId)}";
	[HttpGet("by-user/{userId}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersResponse))] 
	public async Task<ActionResult<GetOrdersResponse>> GetOrderByUserId([FromRoute] Guid userId, CancellationToken ct)
	{ 
		var orders = await _ordersAPIService.GetOrdersByUserIdAsync(userId, ct); 
		return Ok(orders); 
	}

	/// <summary>
	/// Создание заказов.
	/// TO DO: удалить, ибо излишка, т.к. нет сценариев пока использования этого запроса?
	/// </summary> 
	[HttpPost("batch")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateOrdersResponse))] 
	public async Task<ActionResult<CreateOrdersResponse>> PostOrders([FromBody] CreateOrdersRequest parameters)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);

		return StatusCode(StatusCodes.Status404NotFound);
		//var ordersResponse = await _ordersAPIService.CreateOrdersAsync(parameters);

		//return Ok(ordersResponse);  
	}

	/// <summary>
	/// Создание заказа.
	/// </summary> 
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateOrderResponse))] 
	public async Task<ActionResult<CreateOrderResponse>> PostOrder([FromBody] CreateOrderRequest parameters)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);
		
		var orderResponse = await _ordersAPIService.CreateOrderAsync(parameters); 
		return Ok(orderResponse); 
	}

	/// <summary>
	/// Успешное завершение заказа.
	/// </summary> 
	[HttpPost("{id:guid}/complete")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))] 
	public async Task<IActionResult> CompleteOrder([FromRoute] Guid id)
	{ 
		await _ordersAPIService.CompletionOrderAsync(id);
		return Ok(); 
	}

	/// <summary>
	/// Отмена заказа.
	/// </summary> 
	[HttpPost("{id:guid}/cancel")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))] 
	public async Task<IActionResult> CancelOrder([FromRoute] Guid id)
	{
		await _ordersAPIService.CancellationOrderAsync(id);
		return Ok();
	}

	/// <summary>
	/// Обновление заказа.
	/// </summary> 
	[HttpPut("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))] 
	public async Task<IActionResult> UpdateOrderById([FromRoute] int id)
	{ 
		//TO DO:
		return StatusCode(StatusCodes.Status404NotFound);
	} 
}

