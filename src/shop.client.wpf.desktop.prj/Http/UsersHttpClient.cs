using Shop.Model;
using Shop.Model.Database.Entities;
using System.Net.Http.Json;
using System.Text.Json;

namespace Shop.Client.WPF.Desktop.Http;
public class UsersHttpClient
{
	private readonly MainInfo _mainInfo;

	public UsersHttpClient(MainInfo mainInfo)
	{
		_mainInfo = mainInfo;
	}

	/// <summary>
	/// Получение User'а по Login'у.
	/// TO DO: auth, authController.
	/// </summary> 
	public async Task<UserEntity?> GetUserByLogin(string name)
	{
		try
		{
			var httpClient = _mainInfo.HttpClient;

			var cancellationToken = new CancellationToken();

			var url = @$"{HttpConstants.users}by-login?login={Uri.EscapeDataString(name)}";

			var response = await httpClient.GetFromJsonAsync<UserEntity>(
				url,
				new JsonSerializerOptions(JsonSerializerDefaults.Web),
				cancellationToken).ConfigureAwait(false);

			return response;
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(UsersHttpClient)}.{nameof(GetUserByLogin)} failed: {exc}");
			return null;
		}
	}
}
