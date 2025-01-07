namespace Shop.Server;

public class Database : IDatabase
{
	/// <inheritdoc/>
	public string ServerConnectionString { get; }

	/// <inheritdoc/>
	public string ConnectionString { get; }

	public Database(IConfiguration configRoot)
	{
		ServerConnectionString = configRoot.GetConnectionString("ServerConnection");
		ConnectionString       = configRoot.GetConnectionString("DefaultConnection");
	} 
}
