
using Autofac;
using Shop.Client.Services;

namespace Shop.Client.Modules;
public class ServicesModule : Autofac.Module
{
	protected override void Load(ContainerBuilder builder)
	{ 
		builder
			.RegisterType<MainInfo>()
			.AsSelf() 
			.SingleInstance();

		builder
			.RegisterType<PageService>()
			.As<IPageService>()
			.AsSelf()
			.SingleInstance();
	}
}
