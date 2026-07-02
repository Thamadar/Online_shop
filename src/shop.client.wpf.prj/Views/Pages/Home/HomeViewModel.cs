using DynamicData;
using ReactiveUI;

using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Shop.Client.WPF.Extensions;
using Shop.Client.WPF.Http;
using Shop.Client.WPF.Services;
using Shop.Client.WPF.Services.Page;
 
namespace Shop.Client.WPF.Views.Pages;

public sealed partial class HomeViewModel : PageBase
{
	private readonly MainInfo _mainInfo;

	private readonly IProductsService _productsService;

	/// <summary>
	/// Отслеживание изменений цены всех выбранных товаров, находящихся в корзине.
	/// </summary>
	private readonly IObservable<decimal?> _resultBasketPriceObservable;

	private ReadOnlyObservableCollection<IProductItemVM> _productInBasketItems = new(new());
	private ReadOnlyObservableCollection<IProductItemVM> _productItems = new(new());

	private decimal? _resultBasketPrice;

	/// <summary>
	/// Отображаемые товары в корзине.
	/// </summary>
	public ReadOnlyObservableCollection<IProductItemVM> ProductInBasketItems => _productInBasketItems;

	/// <summary>
	/// Отображаемые товары.
	/// </summary>
	public ReadOnlyObservableCollection<IProductItemVM> ProductItems => _productItems;

	/// <summary>
	/// Цена всех выбранных товаров, находящихся в корзине.
	/// </summary>
	public decimal? ResultBasketPrice
	{ 
		get => _resultBasketPrice;
		set => this.RaiseAndSetIfChanged(ref _resultBasketPrice, value);
	}

	public HomeViewModel(
		MainInfo mainInfo,
		IProductsService productsService)
		: base(mainInfo)
	{ 
		_productsService = productsService; 

		PageHeader = "HomePageHeader";

		_resultBasketPriceObservable = _productsService.ResultBasketPriceObservable;  

		_productsService
			.ConnectToProductsItems()
			.ObserveOn(RxApp.MainThreadScheduler)
			.Bind(out _productItems)
			.Subscribe()
			.AddTo(_disposables);

		_productsService
			.ConnectToProductsInBasketItems()
			.ObserveOn(RxApp.MainThreadScheduler)
			.Bind(out _productInBasketItems)
			.Subscribe()
			.AddTo(_disposables);

		_resultBasketPriceObservable
			.BindTo(this, x => x.ResultBasketPrice)
			.AddTo(_disposables);
	} 

	public override async Task LoadPageAsync()
	{
		var products = await _productsService.UpdateProductsItemsAsync(); 

		await base.LoadPageAsync();
	}

	public async override Task DisposeAsync()
	{

		await base.DisposeAsync();
	}

	public override async Task UnloadPageAsync()
	{
		 
		await base.UnloadPageAsync();
	} 
}
