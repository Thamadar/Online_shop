using Autofac;

using Shop.Client.Avalonia.Http; 

namespace Shop.Client.Avalonia.Modules;
public class HttpsModule : Autofac.Module
{
	protected override void Load(ContainerBuilder builder)
	{ 

		builder
			.RegisterType<ProductsHttpClient>()
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<OrdersHttpClient>()
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<UsersHttpClient>()
			.AsSelf()
			.SingleInstance();

	}
}
