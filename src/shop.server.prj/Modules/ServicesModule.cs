using Autofac;
using Shop.Server.Repositories;
using Shop.Server.Services;

namespace Shop.Server.Modules;

public sealed class ServicesModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		base.Load(builder);

		builder
			.RegisterType<Database>()
			.As<IDatabase>()
			.SingleInstance();

		builder
			.RegisterType<DbInitService>()
			.AsSelf()
			.SingleInstance();
	}
}
