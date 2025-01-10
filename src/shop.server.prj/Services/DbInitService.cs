using Microsoft.Data.SqlClient;
using Shop.Model.Database.Entities;

namespace Shop.Server.Services;

public class DbInitService : DatabaseBase, IServerService
{
	#region Queries

	private const string _getCountProductsFromTableQuery = @"
		SELECT COUNT(*) FROM Products";

	private const string _checkIsDatabaseExistAndCreateQuery = @"IF NOT EXISTS
	(SELECT * FROM sys.databases WHERE name = 'OnlineShop')
	CREATE DATABASE OnlineShop";

	private const string _checkIsProductTableExistQuery = @"IF NOT EXISTS
	(SELECT * FROM information_schema.tables WHERE table_name = 'Products')";

	private const string _createProductTableQuery = @"CREATE TABLE [dbo].Products(
    [Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [ProductName] [varchar](50) NOT NULL,
    [CurrentCount] [int] NOT NULL,
    [Price] [float] NOT NULL,
    [PriceBeforeSale] [float],
    [Weight] [float] NOT NULL,
    [Image] [varbinary](max) NOT NULL)";

	private const string _insertDefaultDataInProductTableQuery = @"
		INSERT INTO Products (ProductName, CurrentCount, Price, PriceBeforeSale, Weight, Image)
		VALUES 
		(@ProductName, @CurrentCount, @Price, @PriceBeforeSale, @Weight, @Image)";

	#endregion

	public DbInitService(IDatabase database)
		: base(database)
	{ }

	public void Start()
	{
		ConsoleLog.Write("Инициализация подключения к БД...");

		// Check if database "OnlineShop" exists, if not, create it
		using(SqlConnection connection = new SqlConnection(_serverConnectionString))
		{
			connection.Open();
			string createDatabaseQuery = _checkIsDatabaseExistAndCreateQuery;
			SqlCommand createDatabaseCommand = new SqlCommand(createDatabaseQuery, connection);
			createDatabaseCommand.ExecuteNonQuery();
		}

		// Check if table "Products" exists in database "OnlineShop", if not, create it 
		using(SqlConnection connection = new SqlConnection(_connectionString))
		{
			connection.Open();
			string createTableQuery = $"{_checkIsProductTableExistQuery} {_createProductTableQuery}";
			SqlCommand createTableCommand = new SqlCommand(createTableQuery, connection);
			createTableCommand.ExecuteNonQuery();
		}


		// Check if table "Products" is empty, if not, insert
		using(SqlConnection connection = new SqlConnection(_connectionString))
		{  
			connection.Open();

			using(SqlCommand getCountCommand = new SqlCommand(_getCountProductsFromTableQuery, connection))
			{
				int countProducts = (int)getCountCommand.ExecuteScalar();

				if(countProducts == 0)
				{
					var productsDataForInsert = GeProductDataForInsert();

					foreach(var productEntity in productsDataForInsert)
					{
						string insertDefaultDataInProductTableQuery = _insertDefaultDataInProductTableQuery;
						using(SqlCommand insertCommand = new SqlCommand(insertDefaultDataInProductTableQuery, connection))
						{
							insertCommand.Parameters.AddWithValue("@ProductName",     productEntity.ProductName);
							insertCommand.Parameters.AddWithValue("@CurrentCount",    productEntity.CurrentCount);
							insertCommand.Parameters.AddWithValue("@Price",           productEntity.Price);
							insertCommand.Parameters.AddWithValue("@PriceBeforeSale", productEntity.PriceBeforeSale ?? -1);
							insertCommand.Parameters.AddWithValue("@Weight",          productEntity.Weight);
							insertCommand.Parameters.AddWithValue("@Image",           productEntity.Image);
							insertCommand.ExecuteNonQuery();
						}
					}
				} 
			} 
		}

		ConsoleLog.WriteGreen("База данных подключена!");
	}

	/// <summary>
	/// Данные для автозаполнения таблицы Продукты (Products).
	/// </summary>
	/// <returns></returns>
	private static List<ProductEntity> GeProductDataForInsert()
	{
		return new List<ProductEntity>()
		{
			new ProductEntity() { ProductName = "TomatoesCommon",               Price = 150,  Weight = 250, CurrentCount = 3, Image = File.ReadAllBytes(@"Assets\Images\tomatoes-common-product.png") },
			new ProductEntity() { ProductName = "BananasCommon",                Price = 70,   Weight = 500, CurrentCount = 5, Image = File.ReadAllBytes(@"Assets\Images\banana-product.png") },
			new ProductEntity() { ProductName = "ApplesGreen",                  Price = 60,   Weight = 400, CurrentCount = 7, Image = File.ReadAllBytes(@"Assets\Images\apples-green-product.png"), PriceBeforeSale = 150 },
			new ProductEntity() { ProductName = "MushroomChampignons",          Price = 90,   Weight = 500, CurrentCount = 3, Image = File.ReadAllBytes(@"Assets\Images\mushroom-champignons-product.png") },
			new ProductEntity() { ProductName = "CucumbersSmoothMediumFruited", Price = 150,  Weight = 300, CurrentCount = 8, Image = File.ReadAllBytes(@"Assets\Images\cucumbers-smooth-medium-fruited-product.png"), PriceBeforeSale = 200 }
		};
	}
}
