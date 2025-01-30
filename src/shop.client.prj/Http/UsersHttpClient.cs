using Avalonia.Controls;
using Shop.Model.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Client.Http;
public class UsersHttpClient
{
	private readonly MainInfo _mainInfo;

	public UsersHttpClient(MainInfo mainInfo)
	{
		_mainInfo = mainInfo;
	}

	/// <summary>
	/// Получение адреса пользователя из БД через HTTP-запрос.
	/// </summary>
	/// <returns>id-пользователя</returns>
	public async Task<string?> GetAddressById(Guid userId)
	{
		try
		{
			var httpClient = _mainInfo.HttpClient;

			var cancellationToken = new CancellationToken();

			// Отправляем запрос и получаем ответ

			var response = await httpClient.PostAsJsonAsync(
				$@"{HttpConstants.postGetUserAddressById}",
				userId,
				cancellationToken).ConfigureAwait(false);

			// Проверяем, успешен ли ответ
			response.EnsureSuccessStatusCode();

			// Читаем содержимое ответа как строку
			return await response.Content.ReadAsStringAsync();  
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsHttpClient)}.{nameof(GetAddressById)} failed: {exc}");
			return null;
		}
	}

	/// <summary>
	/// КОСТЫЛЬ, ИБО НЕТ аутентификации. - необходимо.
	/// </summary> 
	public async Task<Guid?> GetUserIdByName(string name)
	{
		try
		{
			var httpClient = _mainInfo.HttpClient;

			var cancellationToken = new CancellationToken();

			// Отправляем запрос и получаем ответ

			var response = await httpClient.PostAsJsonAsync(
				$@"{HttpConstants.postGetUserIdByLogin}",
				name,
				cancellationToken).ConfigureAwait(false);

			// Проверяем, успешен ли ответ
			response.EnsureSuccessStatusCode();

			// Читаем содержимое ответа как строку
			var userIdString = await response.Content.ReadAsStringAsync();
			var resultUserIdString = userIdString.Replace($"\"", "");
			if(Guid.TryParse(resultUserIdString, out Guid userId))
			{
				return userId;
			}
			else
			{
				// Если не удалось преобразовать в Guid, можно вернуть null или обработать ошибку
				return null;
			}
		}
		catch(Exception exc)
		{
			ConsoleLog.WriteError($@"{nameof(ProductsHttpClient)}.{nameof(GetAddressById)} failed: {exc}");
			return null;
		}
	}
}
