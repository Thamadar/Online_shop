using Avalonia.Controls;
using ReactiveUI;
using Shop.Client.Avalonia.Extensions;
using Shop.Client.Avalonia.Http;
using Shop.Client.Avalonia.Services;
using Shop.Client.Avalonia.Views.Menu;
using Shop.Client.Avalonia.Views.Pages;
using Shop.UI.Avalonia;
using System.Reactive.Linq;

namespace Shop.Client.Avalonia.Views;

public sealed partial class MainWindowViewModel : ViewModelBase
{
	private readonly MainInfo _mainInfo;

	private IPage _currentPage;

	private IPageService _pageService;

	/// Текущая отображаемая страница.
	public IPage CurrentPage
	{
		get => _currentPage;
		set => this.RaiseAndSetIfChanged(ref _currentPage, value);
	}

	public MenuViewModel MenuViewModel { get; }

	public MainWindowViewModel()
		:this(MainInfo.DesignMainInfo)
	{

	}

	public MainWindowViewModel(
		MainInfo mainInfo) 
	{
		_mainInfo = mainInfo;

		MenuViewModel = new MenuViewModel(_mainInfo);

		if(Design.IsDesignMode)
		{
			CurrentPage = new PageDesignViewModel();
		}
	}

	public async Task LoadMainWindow()
	{
		_pageService = _mainInfo.GetService<IPageService>();

		_pageService.CurrentPageObservable
			.ObserveOn(RxApp.MainThreadScheduler)
			.BindTo(this, x => x.CurrentPage)
			.AddTo(_disposables); 

		var arrParameters = new object[] {
			_mainInfo,
			_mainInfo.GetItem<IProductsService>(),
			_mainInfo.GetItem<UsersHttpClient>()
		}.ToArray();
		await _pageService.GoToPageAsync<HomeViewModel>(arrParameters); 
	}
}
