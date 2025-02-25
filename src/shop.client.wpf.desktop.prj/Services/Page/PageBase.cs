
using ReactiveUI;
using Shop.Client.WPF.Desktop.Extensions;
using Shop.UI.WPF;
using System.Windows.Controls;

namespace Shop.Client.WPF.Desktop.Services.Page;
public abstract class PageBase : ViewModelBase, IPage
{
	private readonly MainInfo _mainInfo;

	private Control _view;
	private string _pageHeader = "UnknownPageHeader";
	private bool _isLoaded;
	 
	/// <inheritdoc/>
	public Control View
	{
		get => _view;
		private set => this.RaiseAndSetIfChanged(ref _view, value);
	}

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

	/// <inheritdoc/>
	public void SetView(Control view)
	{
		View = view;
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
