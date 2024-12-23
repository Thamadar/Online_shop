using ReactiveUI;
using Shop.UI;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Shop.Client.Services;
public interface IPageService : IShopService
{
	#region Properties

	/// <summary>
	/// Прослушивание текущей страницы.
	/// </summary>
	IObservable<IPage> CurrentPageObservable { get; }

	/// <summary>
	/// Текущая страница.
	/// </summary>
	IPage CurrentPage { get; }

	#endregion Properties

	#region Methods

	T? CreatePage<T>(params object[] arguments) where T : class, IPage; 

	Task<T?> GoToPageAsync<T>(params object[] arguments) where T : class, IPage; 

	#endregion Methods
}

public class PageService : ViewModelBase, IPageService
{
	#region Fields

	private readonly MainInfo _mainInfo;

	private readonly Subject<IPage> _currentPageSubject = new Subject<IPage>();

	private IPage _currentPage;

	#endregion

	#region Properties

	/// <inheritdoc/>
	public IPage CurrentPage
	{
		get { return _currentPage; }
		set
		{
			if(_currentPage != value)
			{ 
				_currentPage = value;
				_currentPageSubject.OnNext(value);
			}
		}
	}

	/// <inheritdoc/>
	public IObservable<IPage> CurrentPageObservable => _currentPageSubject.AsObservable();

	public List<IPage> Pages { get; } = new List<IPage>();

	#endregion

	#region Constructors

	public PageService(MainInfo mainInfo)
	{
		_mainInfo = mainInfo;
	}

	#endregion

	#region Methods

	public T? CreatePage<T>(params object[] arguments)
		where T : class, IPage
	{
		var page = default(T);

		try
		{
			page = (T?)Activator.CreateInstance(typeof(T), arguments);
		}
		finally
		{ }

		return page;
	}

	public async Task<T?> GoToPageAsync<T>(params object[] arguments)
	where T : class, IPage
	{
		//var previousPage = default(IPage);
		var newPage      = default(T);
		if(!Pages.Any(x => x is T))
		{
			try
			{
				newPage = CreatePage<T>(arguments);
				if(newPage == null)
				{
					Debug.Fail($"Page was null. Type page: {typeof(T).Name}");
					return default;
				}
				Pages.Add(newPage);
				CurrentPage = newPage;
			}
			catch(Exception ex)
			{
				Debug.WriteLine($"Failed to go to page of type {typeof(T).Name}. Error: {ex.Message}");
			}
		}
		else
		{
			CurrentPage = Pages.Where(x => x is T).First();
		}

		await CurrentPage.LoadPageAsync();


		return default;
	}

	#endregion
}
