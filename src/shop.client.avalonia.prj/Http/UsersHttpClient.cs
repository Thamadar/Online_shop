using Shop.Dto;
using Shop.Dto.Users;
using Shop.Utilities;

using System.Net.Http.Json; 
using System.Text.Json; 

namespace Shop.Client.Avalonia.Http;
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
	public async Task<GetUserDto?> GetUserByLogin(string name)
	{
		try
		{
			var httpClient = _mainInfo.HttpClient;

			var cancellationToken = new CancellationToken();

			var url = @$"{HttpConstants.users}by-login/{Uri.EscapeDataString(name)}"; 

			var response = await httpClient.GetFromJsonAsync<GetUserDto>(
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
