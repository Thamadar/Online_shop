using Autofac;

using Shop.Client.WPF.Extensions;
using Shop.Client.WPF.Views;
using Shop.Client.WPF.Views.Pages; 

namespace Shop.Client.WPF.Modules;
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
