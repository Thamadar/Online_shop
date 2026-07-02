 

using Shop.Dto;
using Shop.Dto.Orders;
using Shop.Utilities;
using System.Net.Http.Json;  

namespace Shop.Client.WPF.Http;
public class OrdersHttpClient
{
	private readonly MainInfo _mainInfo;

	public OrdersHttpClient(MainInfo mainInfo)
	{
		_mainInfo = mainInfo;
	}

	/// <summary>
	/// Создание заказа в БД через HTTP-запрос.
	/// </summary> 
	public async Task<CreateOrderResponse?> CreateOrder(CreateOrderRequest createOrderRequest)
	{
		try
		{
			var httpClient = _mainInfo.HttpClient;

			var cancellationToken = new CancellationToken();

			var responseMessage = await httpClient.PostAsJsonAsync(
				$@"{HttpConstants.orders}",
				createOrderRequest,
				cancellationToken).ConfigureAwait(false);

			return await responseMessage.Content.ReadFromJsonAsync<CreateOrderResponse>();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersHttpClient)}.{nameof(CreateOrder)} failed: {exc}");
			return null;
		}
	}

	/// <summary>
	/// Создание заказов в БД через HTTP-запрос.
	/// </summary> 
	public async Task<CreateOrdersResponse?> CreateOrders(CreateOrdersRequest createOrdersRequest)
	{
		try
		{
			var httpClient = _mainInfo.HttpClient;

			var cancellationToken = new CancellationToken();

			var responseMessage = await httpClient.PostAsJsonAsync(
				$@"{HttpConstants.orders}/batch",
				createOrdersRequest,
				cancellationToken).ConfigureAwait(false);

			return await responseMessage.Content.ReadFromJsonAsync<CreateOrdersResponse>();
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(OrdersHttpClient)}.{nameof(CreateOrders)} failed: {exc}");
			return null;
		}
	}
}
