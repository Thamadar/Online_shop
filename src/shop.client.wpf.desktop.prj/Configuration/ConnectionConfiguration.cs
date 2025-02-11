using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Client.WPF.Desktop.Configuration;


/// <summary>Параметры подключения к серверу.</summary>
public class ConnectionConfiguration
{

	public static ConnectionConfiguration Defaults { get; } = new("http://localhost:5100", 15, 5000);

	private string _baseAddress = string.Empty;

	/// <summary>Базовый адрес.</summary>
	public string BaseAddress => _baseAddress;

	/// <summary>Время жизни соединения в минутах.</summary>
	public int PooledConnectionLifeTimeMinutes { get; }

	/// <summary>Тайм-аут в миллисекундах.</summary>
	public int TimeOutInMilliseconds { get; }

	/// <param name="baseAddress">Базовый адрес.</param>
	/// <param name="pooledConnectionLifeTimeMinutes">Время жизни соединения в минутах.</param>
	/// <param name="timeOutInMilliseconds">Тайм-аут в миллисекундах.</param>
	public ConnectionConfiguration(
		string baseAddress,
		int pooledConnectionLifeTimeMinutes,
		int timeOutInMilliseconds)
	{
		_baseAddress = baseAddress;

		PooledConnectionLifeTimeMinutes = pooledConnectionLifeTimeMinutes;
		TimeOutInMilliseconds = timeOutInMilliseconds;
	}
}
