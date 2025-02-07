﻿using Shop.Model;
using Shop.Server.Repositories;

using Microsoft.AspNetCore.Mvc;
using Shop.Model.Database.Entities;

namespace Shop.Server.Controllers;

/// <summary>
/// Контроллер товаров.
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
