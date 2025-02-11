using ReactiveUI;
using Shop.Client.Avalonia.Services;

namespace Shop.Client.Avalonia.Views.Menu;

/// <summary>
/// Кнопка меню.
/// </summary>
public interface IMenuItem
{
	public Type PageType { get; }

	/// <summary>
	/// Наименование кнопки.
	/// </summary>
	string DisplayName { get; }

	/// <summary>
	/// Выбрана ли данная страница?
	/// </summary>
	bool IsSelected { get; set; }

	/// <summary>
	/// Команда.
	/// </summary>
	IReactiveCommand Command { get; }
}
