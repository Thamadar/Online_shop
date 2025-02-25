using Autofac;
using Shop.Client.WPF.Desktop.Services;

namespace Shop.Client.WPF.Desktop.Modules;
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

		builder.RegisterType<ProductsService>()
			.As<IProductsService>()
			.AsSelf()
			.SingleInstance(); 
	}
}
