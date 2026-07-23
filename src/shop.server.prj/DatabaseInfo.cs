namespace Shop.Server;

public class DatabaseInfo : IDatabaseInfo
{
	/// <inheritdoc/>
	public string ServerConnectionString { get; }

	/// <inheritdoc/>
	public string ConnectionString { get; }

	public DatabaseInfo(IConfiguration configRoot)
	{
		ServerConnectionString = configRoot.GetConnectionString("ServerConnection");
		ConnectionString       = configRoot.GetConnectionString("DatabaseConnection");

		ConsoleLog.Write($"Конфиг подключения к серверу БД: {ServerConnectionString}");
		ConsoleLog.Write($"Конфиг подключения к БД: {ConnectionString}");
	} 
}
