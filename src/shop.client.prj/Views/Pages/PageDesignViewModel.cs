using Shop.Client.Services.Page; 

namespace Shop.Client.Views.Pages;
public class PageDesignViewModel : PageBase
{
	public PageDesignViewModel()
		: base(MainInfo.DesignMainInfo)
	{
		PageHeader = "SuperMegaPage";
	}
}
