using Autofac; 

using Shop.Client.Views;
using Shop.Client.Views.Menu;

namespace Shop.Client.Modules;

public class ViewModelsModule : Autofac.Module
{ 
	protected override void Load(ContainerBuilder builder)
	{
		builder
			.RegisterType<MainWindowView>() 
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<MainWindowViewModel>() 
			.AsSelf()
			.SingleInstance();

		#region Panels


		#endregion

	}
}
