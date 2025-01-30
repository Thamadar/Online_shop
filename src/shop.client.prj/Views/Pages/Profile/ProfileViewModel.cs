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
}
