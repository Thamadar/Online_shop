
using AutoMapper;
using Microsoft.AspNetCore.Mvc; 
using Shop.Dto;
using Shop.Dto.Products;
using Shop.Dto.Users;
using Shop.Server.Data; 
using Shop.Server.Services.API;
using Shop.Utilities;

namespace Shop.Server.Controllers;

/// <summary>
/// Контроллер товаров.
/// TO DO: доделать пустые запросы.
/// </summary>
[ApiController]
[Route(HttpConstants.products)]
public sealed class ProductsController : ShopControllerBase
{
	private readonly IProductsAPIService _productsAPIService;

	public ProductsController(IMapper mapper, IProductsAPIService productsAPIService) : base(mapper) 
	{
		_productsAPIService = productsAPIService;
	}

	/// <summary>
	/// Получение всех товаров.
	/// </summary> 
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductsResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetProductsResponse>> GetProducts()
	{
		try
		{
			var products = await _productsAPIService.GetProductsAsync(); 
			return Ok(products);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(GetProducts)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Получение товара по id.
	/// </summary> 
	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetProductResponse>> GetProductById([FromRoute] int id)
	{
		try
		{
			var product = await _productsAPIService.GetProductByIdAsync(id); 
			return Ok(product);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(GetProductById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Получение товара по имени.
	/// </summary> 
	[HttpGet("by-name/{name}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<GetProductResponse>> GetProductByName([FromRoute] string name)
	{
		try
		{
			var product = await _productsAPIService.GetProductByNameAsync(name); 
			return Ok(product);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(GetProductByName)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Добавление товаров.
	/// </summary> 
	[HttpPost("batch")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateProductsResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<CreateProductsResponse>> PostProducts([FromBody] CreateProductsRequest parameters)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			var products = await _productsAPIService.CreateProductsAsync(parameters);
			return Ok(products);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(PostProducts)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Добавление товара.
	/// </summary> 
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateProductResponse))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<CreateProductResponse>> PostProduct([FromBody] CreateProductRequest parameters)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			var product = await _productsAPIService.CreateProductAsync(parameters);
			return Ok(product);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(PostProduct)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}
	 
	/// <summary>
	/// Добавление скидки товару.
	/// </summary> 
	[HttpPost("{id:int}/discount")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> AddOrUpdateDiscount([FromRoute] int id, [FromBody] EditProductDiscountRequest parameters)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);

		try
		{
			await _productsAPIService.EditProductDiscountAsync(id, parameters);
			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(PostProduct)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}
	 
	/// <summary>
	/// Обновление товара.
	/// </summary> 
	[HttpPut("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> UpdateProductById(
		[FromRoute] int id)
	{
		try
		{
			return StatusCode(StatusCodes.Status404NotFound);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(UpdateProductById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Удаление скидки товара.
	/// </summary> 
	[HttpDelete("{id:int}/discount")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> ClearProductDiscount([FromRoute] int id)
	{
		try
		{
			await _productsAPIService.ClearProductDiscountAsync(id);
			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(ClearProductDiscount)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}


	/// <summary>
	/// Удаление товара.
	/// </summary> 
	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> DeleteProduct([FromRoute] int id)
	{
		try
		{
			return StatusCode(StatusCodes.Status404NotFound);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(DeleteProduct)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}
}
