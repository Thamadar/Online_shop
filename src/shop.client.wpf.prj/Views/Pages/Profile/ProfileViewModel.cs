using Shop.Client.WPF.Services.Page;

namespace Shop.Client.WPF.Views.Pages;
public class ProfileViewModel : PageBase
{
	private readonly MainInfo _mainInfo;
	  
	public ProfileViewModel(MainInfo mainInfo)
		: base(mainInfo)
	{ 
		PageHeader = "ProfilePageHeader";
	} 
}
