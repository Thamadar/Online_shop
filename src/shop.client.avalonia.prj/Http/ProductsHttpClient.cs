using Shop.Dto;
using Shop.Dto.Products; 
using Shop.Utilities;

using System.Net.Http.Json;
using System.Text.Json;

namespace Shop.Client.Avalonia.Http;
public class ProductsHttpClient
{
	private readonly MainInfo _mainInfo;

	public ProductsHttpClient(MainInfo mainInfo)
	{
		_mainInfo = mainInfo;
	}

	/// <summary>
	/// Получение всех товаров из БД через HTTP-запрос.
	/// </summary> 
	public async Task<GetProductsResponse?> FetchAllProducts()
	{
		try
		{
			var httpClient = _mainInfo.HttpClient;

			var cancellationToken = new CancellationToken();

			var response = await httpClient.GetFromJsonAsync<GetProductsResponse>(
				$@"{HttpConstants.products}",
				new JsonSerializerOptions(JsonSerializerDefaults.Web),
				cancellationToken).ConfigureAwait(false);

			return response;
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsHttpClient)}.{nameof(FetchAllProducts)} failed: {exc}");
			return null;
		} 
	}
}
