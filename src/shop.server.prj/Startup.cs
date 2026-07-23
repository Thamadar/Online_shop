using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Server.Data;
using Shop.Server.Mappers;
using Shop.Server.Repositories;
using Shop.Server.Services;
using Shop.Server.Services.API;
using Shop.Server.Services.Tables;
using Shop.Utilities;

namespace Shop.Server;

public class Startup
{
	public IConfiguration configRoot { get; }

	public Startup(IConfiguration configuration)
	{
		configRoot = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		ConfigureMappers(services);
		ConfigureRepositories(services);
		ConfigureDbContexts(services);
		ConfigureTables(services);
		ConfigureAPIServices(services);

		using(var serviceProvider = services.BuildServiceProvider())
		{
			var serverServices  = serviceProvider.GetServices<IServerService>();
			foreach(var serverService in serverServices)
			{
				serverService.Initialization();
			}
		}  

		services.AddControllers(); 
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(); 
	}

	public void Configure(WebApplication app, IWebHostEnvironment env)
	{ 
		if(app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseAuthorization();

		app.MapControllers();

		app.UseExceptionHandler(exceptionHandlerApp =>
		{
			exceptionHandlerApp.Run(async context =>
			{
				var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
				var exc = exceptionHandlerFeature?.Error;

				if(exc != null)
				{ 
					var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
					logger.LogError(exc, "Unhandled exception");

					context.Response.StatusCode = StatusCodes.Status500InternalServerError;
					context.Response.ContentType = "application/json";

					var body = new ExceptionRepresenter(exc).ToString();
					await context.Response.WriteAsync(body);
				}
			});
		});

		app.Run();
	}

	private void ConfigureMappers(IServiceCollection services)
	{
		services.AddAutoMapper(typeof(OrdersProfile));
		services.AddAutoMapper(typeof(ProductsProfile));
		services.AddAutoMapper(typeof(UserProfile));
	}

	private void ConfigureAPIServices(IServiceCollection services)
	{
		services.AddScoped<IProductsAPIService, ProductsAPIService>();
		services.AddScoped<IOrdersAPIService,   OrdersAPIService>();
		services.AddScoped<IUsersAPIService,    UsersAPIService>();
	}

	private void ConfigureRepositories(IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		services.AddScoped<IProductsRepository, ProductsRepository>();
		services.AddScoped<IOrdersRepository,  OrdersRepository>();
		services.AddScoped<IUsersRepository,   UsersRepository>(); 
	}

	private void ConfigureDbContexts(IServiceCollection services)
	{
		services.AddDbContext<ServerContext>(options =>
			options.UseSqlServer(
				configRoot.GetConnectionString("DatabaseConnection"),
					sqlOptions =>
					{ 
						sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
					}));
	}

	private void ConfigureTables(IServiceCollection services)
	{
		//TO DO: Добавить Dispose после инициализации, чтобы освободить ресурсы?
		services.AddSingleton<IDatabaseInfo, DatabaseInfo>();
		services.AddSingleton<IServerService, ProductTableInit>();
		services.AddSingleton<IServerService, OrderTableInit>();
		services.AddSingleton<IServerService, UserTableInit>();
		services.AddSingleton<IServerService, OrderProductsTableInit>();
	}
}
