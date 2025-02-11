
using ReactiveUI;
using Shop.Client.WPF.Desktop.Extensions;
using Shop.UI.WPF.Desktop;

namespace Shop.Client.WPF.Desktop.Services.Page;
public abstract class PageBase : ViewModelBase, IPage
{
	private readonly MainInfo _mainInfo;

	private string _pageHeader = "UnknownPageHeader";
	private bool _isLoaded;

	/// <inheritdoc/>
	public bool IsLoaded
	{
		get => _isLoaded;
		set => this.RaiseAndSetIfChanged(ref _isLoaded, value);
	}

	/// <inheritdoc/>
	public string PageHeader
	{
		get => _pageHeader;
		protected set => this.RaiseAndSetIfChanged(ref _pageHeader, value);
	} 

	public PageBase(MainInfo mainInfo)
	{
		_mainInfo = mainInfo; 
	}

	public virtual Task DisposeAsync()
	{ 
		Dispose();
		_disposables.DisposeAll();
		return Task.CompletedTask;
	}

	public virtual Task LoadPageAsync()
	{ 
		IsLoaded = true; 
		return Task.CompletedTask;
	}

	public virtual Task UnloadPageAsync()
	{
		IsLoaded = false;
		return Task.CompletedTask;
	} 
}
