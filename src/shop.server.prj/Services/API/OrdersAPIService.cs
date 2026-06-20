
using Shop.Server.Repositories;

namespace Shop.Server.Services.API;

public class OrdersAPIService : IOrdersAPIService
{
	private readonly IOrdersRepository _ordersRepository;


	/// <inheritdoc/>
	public void Initialization()
	{ }
}
