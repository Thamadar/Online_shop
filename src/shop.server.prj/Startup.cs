using Microsoft.EntityFrameworkCore;

using Shop.Server.Controllers;
using Shop.Server.Entities;
using Shop.Server.Repositories;
using Shop.Server.Services;

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
		//TO DO:Вынести регистрацию модулей в отдельный класс  
		services.AddScoped<IProductRepository, ProductRepository>();

		//TO DO:Вынести регистрацию модулей в отдельный класс  
		services.AddSingleton<IDatabase, Database>();
		services.AddSingleton<IServerService, DbInitService>();

		using(var serviceProvider = services.BuildServiceProvider())
		{
			var serverServices  = serviceProvider.GetServices<IServerService>();
			foreach(var serverService in serverServices)
			{
				serverService.Start();
			}
		}  

		services.AddControllers(); 
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddDbContext<ProductContext>(options => options.UseSqlServer(configRoot.GetConnectionString("DefaultConnection")));
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

		app.Run();
	} 
}
