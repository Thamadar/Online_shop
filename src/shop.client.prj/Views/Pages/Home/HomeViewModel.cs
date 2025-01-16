using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Shop.Client.Extensions;
using Shop.Client.Http;
using Shop.Client.Services;
using Shop.Client.Services.Page;
using System.Collections.ObjectModel;
using System.Reactive.Linq;


namespace Shop.Client.Views.Pages;

public sealed partial class HomeViewModel : PageBase
{
	private readonly MainInfo _mainInfo;

	private readonly IProductsService _productsService;

	private ReadOnlyObservableCollection<IProductItemVM> _productInBasketItems = new(new());
	private ReadOnlyObservableCollection<IProductItemVM> _productItems = new(new());

	/// <summary>
	/// Отображаемые товары в корзине.
	/// </summary>
	public ReadOnlyObservableCollection<IProductItemVM> ProductInBasketItems => _productInBasketItems;

	/// <summary>
	/// Отображаемые товары.
	/// </summary>
	public ReadOnlyObservableCollection<IProductItemVM> ProductItems => _productItems;

	public HomeViewModel()
		: this(MainInfo.DesignMainInfo, new ProductsService(MainInfo.DesignMainInfo, new ProductsHttpClient(MainInfo.DesignMainInfo)))
	{
	}

	public HomeViewModel(
		MainInfo mainInfo,
		IProductsService productsService)
	{
		_mainInfo        = mainInfo;
		_productsService = productsService; 

		PageHeader = "HomePageHeader";

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
	} 

	public override async Task LoadPageAsync()
	{
		var products = await _productsService.UpdateProductsItems(); 

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

	public async Task ClearBasket()
	{
		await _productsService.RemoveAllProductsFromBasket();
	}
}
