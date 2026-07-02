using ReactiveUI;

using System.Reactive.Linq;

using Shop.Client.WPF.Extensions; 
using Shop.Client.WPF.Services;
using Shop.Client.WPF.Views.Menu;
using Shop.Client.WPF.Views.Pages;
using Shop.UI.WPF;

namespace Shop.Client.WPF.Views;
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
	  
	public MainWindowViewModel(
		MainInfo mainInfo)
	{
		_mainInfo = mainInfo;

		MenuViewModel = new MenuViewModel(_mainInfo); 
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
			_mainInfo.GetItem<IProductsService>()
		}.ToArray();
		await _pageService.GoToPageAsync<HomeViewModel>(arrParameters);
	}
}
