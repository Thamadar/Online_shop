using Avalonia.Controls;
using ReactiveUI;
using Shop.Client.Extensions;
using Shop.Client.Services;
using Shop.UI;
using System.Reactive.Linq;

namespace Shop.Client.Views.Menu;
public sealed partial class MenuViewModel : ViewModelBase
{
	private readonly MainInfo _mainInfo;

	private IPageService _pageService;

	private bool _isOpened = true;

	/// <summary>
	/// Открыто ли меню?
	/// </summary>
	public bool IsOpened
	{
		get => _isOpened;
		private set => this.RaiseAndSetIfChanged(ref _isOpened, value);
	}

	public MenuData MenuData { get; }

	public MenuViewModel()
		: this(MainInfo.DesignMainInfo) { }

	public MenuViewModel(MainInfo mainInfo)
	{
		_mainInfo    = mainInfo; 

		MenuData = new MenuData(mainInfo); 
	}

	/// <summary>
	/// Смена состояния меню: открыто/закрыто.
	/// </summary>
	/// <param name="state"></param>
	public void OpenStateChange(bool? state = null) => IsOpened = (bool)(state != null ? state : !IsOpened); 
}
