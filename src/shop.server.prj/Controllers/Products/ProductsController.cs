using Shop.Model;
using Shop.Model.Bracket;
using Shop.Server.Repositories; 

using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

/// <summary>
/// Контроллер товаров.
/// </summary>
[ApiController]
[Route(HttpConstants.products)] 
public sealed class ProductsController : ControllerBase
{  
	private readonly ProductRepository _productRepository;

	public ProductsController(ProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	/// <summary>
	/// Получение всех товаров.
	/// </summary>
	/// <returns>A Task.</returns>
	[HttpGet(HttpConstants.getTotalProductsTask, Name = nameof(GetAllProducts))]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductEntity[]))]
	[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	public async Task<ActionResult<ProductEntity[]?>> GetAllProducts()
	{
		try
		{ 
			var products = await _productRepository.FetchAllProducts(); 
			 
			return Ok(products);
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsController)}.{nameof(GetAllProducts)} failed: {exc}");
			return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionRepresenter(exc).ToString());
		}
	}
}
