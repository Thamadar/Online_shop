using Autofac;

namespace Shop.Client.WPF.Services;

/// <summary>
/// The registration service.
/// </summary>
public static class RegistrationService
{
	/// <summary>
	/// Creates the container.
	/// </summary>
	/// <param name="appServices">The app services.</param>
	/// <returns>An IContainer.</returns>
	public static IContainer CreateContainer()
	{
		var builder = new ContainerBuilder();

		builder.RegisterAssemblyModules(typeof(Shop.Client.WPF.App).Assembly);
		var container = builder.Build();

		return container;
	}
}
