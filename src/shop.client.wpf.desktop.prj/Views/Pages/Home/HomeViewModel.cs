using DynamicData;
using ReactiveUI;

using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Shop.Client.WPF.Desktop.Extensions;
using Shop.Client.WPF.Desktop.Http;
using Shop.Client.WPF.Desktop.Services;
using Shop.Client.WPF.Desktop.Services.Page;
 
namespace Shop.Client.WPF.Desktop.Views.Pages;

public sealed partial class HomeViewModel : PageBase
{
	private readonly MainInfo _mainInfo;

	private readonly IProductsService _productsService;

	private readonly UsersHttpClient _usersHttpClient;

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

	public HomeViewModel(
		MainInfo mainInfo,
		IProductsService productsService,
		UsersHttpClient usersHttpClient)
		: base(mainInfo)
	{ 
		_productsService = productsService;
		_usersHttpClient = usersHttpClient;

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

	/// <summary>
	/// Очистка текущей корзины.
	/// </summary>
	/// <returns></returns>
	public async Task ClearBasket()
	{
		await _productsService.RemoveAllProductsFromBasket();
	}

	/// <summary>
	/// Создание заказа
	/// </summary> 
	public async Task CreateOrder()
	{
		var userId = new Guid?();
		//TO DO: userId = Получение текущего юзера из памяти, если бы было аутентификация пользователя. 
		//из-за чего Хардкорд (плохо)
		//TO DO: вынести в сервис UsersService/или что-то иное.
		userId = await _usersHttpClient.GetUserIdByName("admin");

		if(userId != null)
		{
			//TO DO: вынести в сервис UsersService/или что-то иное.
			var addressOrder = await _usersHttpClient.GetAddressById(userId.Value);

			if(addressOrder != null)
			{
				var createOrderSuccess = await _productsService.CreateOrder(userId.Value, addressOrder);

				if(createOrderSuccess)
				{
					//TO DO: MessageBox: success.
				}
			}
			else
			{
				//TO DO: вывод MessageBoxError.
			}
		}
		else
		{
			//TO DO: вывод MessageBoxError.
		}
	}
}
