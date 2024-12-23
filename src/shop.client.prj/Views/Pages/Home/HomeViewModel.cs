using Shop.Client.Services.Page;

namespace Shop.Client.Views.Pages;
public class HomeViewModel : PageBase
{
	private readonly MainInfo _mainInfo;

	public HomeViewModel()
		: this(MainInfo.DesignMainInfo)
	{ 
	}

	public HomeViewModel(MainInfo mainInfo)
	{
		_mainInfo = mainInfo;

		PageHeader = "Магазин";
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
