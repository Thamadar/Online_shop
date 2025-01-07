using Microsoft.EntityFrameworkCore;

namespace Shop.Server;

public interface IDatabase
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

/// <summary>
/// Базовый класс для объектов, использующий connect к БД.
/// </summary>
public abstract class DatabaseBase : DbContext
{
	protected string _serverConnectionString;
	protected string _connectionString; 

	public DatabaseBase(IDatabase database)
	{
		_serverConnectionString = database.ServerConnectionString;
		_connectionString       = database.ConnectionString;
	} 

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(_connectionString);
	}
}
