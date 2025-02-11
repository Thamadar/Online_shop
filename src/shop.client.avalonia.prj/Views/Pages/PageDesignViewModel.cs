using Shop.Client.Avalonia.Services.Page; 

namespace Shop.Client.Avalonia.Views.Pages;
public class PageDesignViewModel : PageBase
{
	public PageDesignViewModel()
		: base(MainInfo.DesignMainInfo)
	{
		PageHeader = "SuperMegaPage";
	}
}
