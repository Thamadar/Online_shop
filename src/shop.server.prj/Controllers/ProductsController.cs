
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
	[ResponseCache(Duration = 6000, Location = ResponseCacheLocation.Any)]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductsResponse))] 
	public async Task<ActionResult<GetProductsResponse>> GetProducts(CancellationToken ct)
	{ 
		var products = await _productsAPIService.GetProductsAsync(ct); 
		return Ok(products);
	}

	/// <summary>
	/// Получение товара по id.
	/// </summary> 
	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductResponse))] 
	public async Task<ActionResult<GetProductResponse>> GetProductById([FromRoute] int id, CancellationToken ct)
	{ 
		var product = await _productsAPIService.GetProductByIdAsync(id, ct); 
		return Ok(product); 
	}

	/// <summary>
	/// Получение товара по имени.
	/// </summary> 
	[HttpGet("by-name/{name}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductResponse))] 
	public async Task<ActionResult<GetProductResponse>> GetProductByName([FromRoute] string name, CancellationToken ct)
	{ 
		var product = await _productsAPIService.GetProductByNameAsync(name, ct); 
		return Ok(product); 
	}

	/// <summary>
	/// Добавление товаров.
	/// </summary> 
	[HttpPost("batch")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateProductsResponse))] 
	public async Task<ActionResult<CreateProductsResponse>> PostProducts([FromBody] CreateProductsRequest parameters)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);

		var products = await _productsAPIService.CreateProductsAsync(parameters);
		return Ok(products);
	}

	/// <summary>
	/// Добавление товара.
	/// </summary> 
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateProductResponse))] 
	public async Task<ActionResult<CreateProductResponse>> PostProduct([FromBody] CreateProductRequest parameters)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);
		 
		var product = await _productsAPIService.CreateProductAsync(parameters);
		return Ok(product); 
	}
	 
	/// <summary>
	/// Добавление скидки товару.
	/// </summary> 
	[HttpPost("{id:int}/discount")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))] 
	public async Task<IActionResult> AddOrUpdateDiscount(
		[FromRoute] int id,
		[FromBody] EditProductDiscountRequest parameters)
	{
		if(!ModelState.IsValid)
			return BadRequest(ModelState);

		 
		await _productsAPIService.EditProductDiscountAsync(id, parameters);
		return Ok(); 
	}
	 
	/// <summary>
	/// Обновление товара.
	/// </summary> 
	[HttpPut("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))] 
	public async Task<IActionResult> UpdateProductById([FromRoute] int id)
	{
		//TO DO:
		return StatusCode(StatusCodes.Status404NotFound); 
	}

	/// <summary>
	/// Удаление скидки товара.
	/// </summary> 
	[HttpDelete("{id:int}/discount")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
	public async Task<IActionResult> ClearProductDiscount([FromRoute] int id)
	{ 
		await _productsAPIService.ClearProductDiscountAsync(id);
		return Ok(); 
	}


	/// <summary>
	/// Удаление товара.
	/// </summary> 
	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))] 
	public async Task<IActionResult> DeleteProduct([FromRoute] int id)
	{
		//TO DO:
		return StatusCode(StatusCodes.Status404NotFound); 
	}
}
