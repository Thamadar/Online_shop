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
		ConnectionString       = configRoot.GetConnectionString("DefaultConnection");
	} 
}
