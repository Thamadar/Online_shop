using Autofac;
using Shop.Server.Controllers;
using Shop.Server.Repositories;

namespace Shop.Server.Modules;

public sealed class RepositoriesModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		base.Load(builder);

		builder
			.RegisterType<ProductRepository>()
			.AsSelf()
			.SingleInstance();
		 
		builder.RegisterType<ProductsController>()
			.AsSelf()
			.SingleInstance();

	}
}
