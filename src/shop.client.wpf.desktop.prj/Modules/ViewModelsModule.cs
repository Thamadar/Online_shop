using Autofac;

using Shop.Client.WPF.Desktop.Extensions;
using Shop.Client.WPF.Desktop.Views;
using Shop.Client.WPF.Desktop.Views.Pages; 

namespace Shop.Client.WPF.Desktop.Modules;
public class ViewModelsModule : Autofac.Module
{
	protected override void Load(ContainerBuilder builder)
	{

		builder.RegisterView<MainWindowView, MainWindowViewModel>();

		#region Panels 

		builder.RegisterView<HomeView,    HomeViewModel>();
		builder.RegisterView<ProfileView, ProfileViewModel>();

		#endregion

	}
}
