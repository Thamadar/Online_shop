using DynamicData;

using ReactiveUI;
using Shop.Client.Extensions;
using Shop.Client.Http;
using Shop.Client.Services.Page;
using System.Collections.ObjectModel;


namespace Shop.Client.Views.Pages;
public sealed partial class HomeViewModel : PageBase
{
	private readonly MainInfo _mainInfo;

	private readonly ProductsHttpClient _productsHttpClient;

	private ObservableCollection<ProductItemVM> _productItems = new ObservableCollection<ProductItemVM>();

	/// <summary>
	/// Отображаемые товары.
	/// </summary>
	public ObservableCollection<ProductItemVM> ProductItems => _productItems;

	public HomeViewModel()
		: this(MainInfo.DesignMainInfo, new ProductsHttpClient(MainInfo.DesignMainInfo))
	{
	}

	public HomeViewModel(
		MainInfo mainInfo,
		ProductsHttpClient productsHttpClient)
	{
		_mainInfo           = mainInfo;
		_productsHttpClient = productsHttpClient;

		PageHeader = "HomePageHeader";
	}

	public async override Task DisposeAsync()
	{

		await base.DisposeAsync();
	}

	public override async Task LoadPageAsync()
	{
		var products = await _productsHttpClient.FetchAllProducts();

		if(products != null)
		{
			ProductItems.AddRange(products.ConvertToVM());
		}

		await base.LoadPageAsync();
	}

	public override async Task UnloadPageAsync()
	{
		ProductItems.Clear();

		await base.UnloadPageAsync();
	}
}
