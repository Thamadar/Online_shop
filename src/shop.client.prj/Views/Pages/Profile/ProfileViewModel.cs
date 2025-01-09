using Shop.Client.Services.Page;

namespace Shop.Client.Views.Pages;
public class ProfileViewModel : PageBase
{
	private readonly MainInfo _mainInfo;

	public ProfileViewModel()
		: this(MainInfo.DesignMainInfo)
	{ 
	}

	public ProfileViewModel(MainInfo mainInfo)
	{
		_mainInfo = mainInfo;

		PageHeader = "ProfilePageHeader";
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
