using Microsoft.EntityFrameworkCore;
 
using Shop.Server.Entities;
using Shop.Server.Repositories;
using Shop.Server.Services;
using Shop.Server.Services.Tables;

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
		services.AddScoped<IOrdersRepository,  OrdersRepository>();
		services.AddScoped<IUsersRepository,   UsersRepository>();

		//TO DO:Вынести регистрацию модулей в отдельный класс  
		services.AddSingleton<IDatabaseInfo, DatabaseInfo>();
		services.AddSingleton<IServerService, ProductTableInit>();
		services.AddSingleton<IServerService, OrderTableInit>();
		services.AddSingleton<IServerService, UserTableInit>();

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

		//TO DO:Вынести регистрацию модулей в отдельный класс  
		services.AddDbContext<ProductContext>(options => options.UseSqlServer(configRoot.GetConnectionString("DefaultConnection")));
		services.AddDbContext<OrderContext>(options   => options.UseSqlServer(configRoot.GetConnectionString("DefaultConnection")));
		services.AddDbContext<UserContext>(options    => options.UseSqlServer(configRoot.GetConnectionString("DefaultConnection")));
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
