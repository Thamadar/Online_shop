namespace Shop.Server;

/// <summary>
/// Хранилище данных по подключению к БД.
/// </summary>
public interface IDatabaseInfo
{
	/// <summary>
	/// Подключение к серверу БД.
	/// </summary>
	string ServerConnectionString { get; }

	/// <summary>
	/// Полное подключение к БД. (к серверу и к БД)
	/// </summary>
	string ConnectionString { get; }
}
