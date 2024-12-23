using Shop.Client.Services.Page; 

namespace Shop.Client.Views.Pages;
public class BasketViewModel : PageBase
{
	private readonly MainInfo _mainInfo;

	public BasketViewModel()
		:this(MainInfo.DesignMainInfo)
	{ 
	}

	public BasketViewModel(MainInfo mainInfo)
	{
		_mainInfo = mainInfo;

		PageHeader = "Корзина";
	}

	public async override Task DisposeAsync()
	{
		  
		await base.DisposeAsync();
	}

	public override async Task LoadPageAsync()
	{
		 

		await base.LoadPageAsync();
	}

	public override async Task UnloadPageAsync()
	{
		 
		await base.UnloadPageAsync();
	}
}
