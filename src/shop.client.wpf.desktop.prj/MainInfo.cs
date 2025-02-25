using Autofac;
using System.Diagnostics;
using System.Net.Http;

using Shop.Client.WPF.Desktop.Services;
using Shop.Client.WPF.Desktop.Configuration;
using System.Reflection;
using System.Windows.Controls;

namespace Shop.Client.WPF.Desktop;

public class MainInfo
{
	private readonly object _lockerObjects = new object();
	private readonly object _lockerServices = new object();
	private readonly object _lockerViews = new object();
	private IContainer _container;

	private List<IShopService> _services;

	public HttpClient HttpClient { get; private set; }

	public MainInfo()
	{
		HttpClient = new HttpClient();

		var connectionConfiguration = ConnectionConfiguration.Defaults; //TO DO: Сериализация из конфига

		HttpClient.BaseAddress = new Uri(connectionConfiguration.BaseAddress);
		HttpClient.Timeout = TimeSpan.FromMilliseconds(connectionConfiguration.TimeOutInMilliseconds);
	} 

	public void SetIContainer(IContainer container) => _container = container;

	public T GetItem<T>()
		where T : class
	{
		var resultObject = default(T);

		try
		{
			lock(_lockerObjects)
			{
				if(_container.TryResolve(typeof(T), out var tempObject)
					&& tempObject is T targetType)
				{
					resultObject = targetType;
				}
			}
		}
		catch(Exception ex)
		{
			Debug.WriteLine($"Error. Can't get object. Type error: {ex.GetType().Name}. Message: {ex.Message}");
		}

		return resultObject;
	}

	public T GetService<T>()
		where T : IShopService
	{
		var resultService = default(T);

		try
		{
			lock(_lockerServices)
			{
				if(_container.TryResolve(typeof(T), out var tempService)
					&& tempService is T targetType)
				{
					resultService = targetType;
				}
			}
		}
		catch(Exception ex)
		{
			Debug.WriteLine($"Error. Can't get service. Type error: {ex.GetType().Name}. Message: {ex.Message}");
		}

		return resultService;
	}

	public T GetViewForViewModel<T>(Type viewModelType)
		where T : Control
	{
		var resultView = default(T);

		try
		{
			lock(_lockerViews)
			{
				var viewType = Assembly.GetExecutingAssembly()
					.GetTypes()
					.FirstOrDefault(t => t.Name == viewModelType.Name.Replace("ViewModel", "View"));
				 
				if(viewType != null)
				{
					if(_container.TryResolve(viewType, out var tempView)
					&& tempView is T targetType)
					{
						resultView = targetType;
					}
				} 
			}
		}
		catch(Exception ex)
		{
			Debug.WriteLine($"View not found for ViewModel: {viewModelType.Name}");
		}

		return resultView;
	}
}
