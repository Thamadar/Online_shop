using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class ShopControllerBase : ControllerBase
{
	protected readonly IMapper _mapper;

	protected ShopControllerBase(IMapper mapper)
	{

	}
}
