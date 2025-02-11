using ReactiveUI;

using Shop.UI.WPF.Desktop;

namespace Shop.Client.WPF.Desktop.Views.Menu;

/// <summary>
/// Кнопка меню.
/// </summary>
public class MenuItem : ViewModelBase, IMenuItem
{
	private readonly MainInfo _mainInfo;

	private bool _isSelected;

	public Type PageType { get; }

	/// <inheritdoc/>
	public string DisplayName { get; }
	 
	/// <inheritdoc/>
	public bool IsSelected
	{
		get => _isSelected;
		set => this.RaiseAndSetIfChanged(ref _isSelected, value);
	}

	/// <inheritdoc/>
	public IReactiveCommand Command { get; }

	/// <inheritdoc/>
	public MenuItem(
		MainInfo mainInfo,
		Type pageType,
		string displayName, 
		Func<MenuItem, Task> action)
	{
		_mainInfo = mainInfo;

		PageType    = pageType; 
		DisplayName = displayName;

		Command     = ReactiveCommand.CreateFromTask(action); 
	}
}
