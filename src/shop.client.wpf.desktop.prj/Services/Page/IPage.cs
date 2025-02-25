using System.Windows.Controls;

namespace Shop.Client.WPF.Desktop.Services;
public interface IPage : IDisposable
{

	#region Properties

	/// <summary>
	/// Страница.
	/// </summary>
	public Control View { get; }

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

	/// <summary>
	/// Установка View для ViewModel.
	/// </summary> 
	void SetView(Control view);

	Task DisposeAsync();

	Task LoadPageAsync(); 

	Task UnloadPageAsync();

	#endregion Methods
}
