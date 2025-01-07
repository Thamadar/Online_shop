using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Model.Database.Contexts;
using Shop.Server;
using Shop.Server.Controllers;
using Shop.Server.Repositories;
using Shop.Server.Services;
using System.Reflection;

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
		services.AddSingleton<ProductRepository>();
		services.AddSingleton<ProductsController>();
		services.AddSingleton<IDatabase>(new Database(configRoot));
		services.AddSingleton<DbInitService>();

		services.AddControllers(); 
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(); 

		services.AddDbContext<ProductRepository>(options => options.UseSqlServer(configRoot.GetConnectionString("DefaultConnection"))); 
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
