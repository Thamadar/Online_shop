using Shop.Client.WPF.Desktop.Services.Page;

namespace Shop.Client.WPF.Desktop.Views.Pages;
public class ProfileViewModel : PageBase
{
	private readonly MainInfo _mainInfo;
	  
	public ProfileViewModel(MainInfo mainInfo)
		: base(mainInfo)
	{ 
		PageHeader = "ProfilePageHeader";
	} 
}
