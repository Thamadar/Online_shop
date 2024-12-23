namespace Shop.Client.Services;
public interface IPage : IDisposable
{

	#region Properties

	/// <summary>
	/// Прогружена ли страница?
	/// </summary>
	public bool IsLoaded { get; }

	/// <summary>
	/// Заголовок страницы.
	/// </summary>
	string PageHeader { get; }

	#endregion

	#region Methods 

	Task DisposeAsync();

	Task LoadPageAsync(); 

	Task UnloadPageAsync();

	#endregion Methods
}
