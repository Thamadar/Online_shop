using Autofac;

using System.Windows;

using Shop.Client.WPF.Desktop.Services;
using Shop.Client.WPF.Desktop.Views;

namespace Shop.Client.WPF.Desktop;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	private IContainer? _container;

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		_container = RegistrationService.CreateContainer();

		var mainInfo = _container.Resolve<MainInfo>();
		mainInfo.SetIContainer(_container);

		var mainWindow = GetMainWindow(_container);
		mainWindow.Closed += OnMainWindowClosed;
		mainWindow.Show();
	}

	/// <summary>
	/// Get main window view.
	/// </summary> 
	private MainWindowView GetMainWindow(IContainer container)
	{
		var mainWindow = container.Resolve<MainWindowView>();
		var viewModel  = container.Resolve<MainWindowViewModel>();
		viewModel.LoadMainWindow();

		mainWindow.DataContext = viewModel;

		return mainWindow;
	}

	/// <summary>
	/// Ons the main window closed.
	/// </summary> 
	private void OnMainWindowClosed(object? sender, System.EventArgs e)
	{
		Application.Current.Shutdown(0);
	}
}
