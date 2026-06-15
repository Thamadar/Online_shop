using Shop.Model;
using Shop.Server.Repositories;

using Microsoft.AspNetCore.Mvc;
using Shop.Model.Database.Entities;

namespace Shop.Server.Controllers;

/// <summary>
/// Контроллер товаров.
/// TO DO: доделать пустые запросы.
/// </summary>
[ApiController]
[Route(HttpConstants.products)]
public sealed class ProductsController : ControllerBase
{
	private readonly IProductRepository _productRepository;

	public ProductsController(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	/// <summary>
	/// Получение всех товаров.
	/// </summary> 
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductEntity[]))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<ProductEntity[]?>> GetProducts()
	{
		try
		{
			var products = await _productRepository.GetProducts();

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
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductEntity))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<ProductEntity?>> GetProductById([FromRoute] int id)
	{
		try
		{
			//var products = await _productRepository.FetchAllProducts();

			return Ok(null);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(GetProductById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Добавление товаров.
	/// </summary> 
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> PostProducts([FromBody] ProductEntity[] parameters)
	{
		try
		{

			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(PostProducts)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}

	/// <summary>
	/// Обновление товара.
	/// </summary> 
	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> UpdateProductById(
		[FromRoute] int id,
		[FromBody] ProductEntity parameters)
	{
		try
		{

			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(UpdateProductById)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}


	/// <summary>
	/// Удаление товара.
	/// </summary> 
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<IActionResult> DeleteProduct([FromRoute] int id)
	{
		try
		{

			return Ok();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(DeleteProduct)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}
}
