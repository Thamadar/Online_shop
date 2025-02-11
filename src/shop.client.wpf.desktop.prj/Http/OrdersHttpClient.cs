using Shop.Model;
using Shop.Model.Database.Entities;

using System.Net.Http.Json;  

namespace Shop.Client.WPF.Desktop.Http;
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
	/// <returns></returns>
	public async Task<bool?> CreateOrder(OrderEntity orderEntity)
	{
		try
		{
			var httpClient = _mainInfo.HttpClient;

			var cancellationToken = new CancellationToken();

			var response = await httpClient.PostAsJsonAsync(
				$@"{HttpConstants.postCreateOrder}",
				orderEntity,
				cancellationToken).ConfigureAwait(false);

			return response.IsSuccessStatusCode;
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsHttpClient)}.{nameof(CreateOrder)} failed: {exc}");
			return null;
		}
	}
}
