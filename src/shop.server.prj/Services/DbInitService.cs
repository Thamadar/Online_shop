﻿using Microsoft.Data.SqlClient;
using Shop.Model.Bracket;

namespace Shop.Server.Services;

public class DbInitService : DatabaseBase
{
	#region Queries

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

	private const string _insertDefaultDataInProductTableIfEmptyQuery = @"
	IF NOT EXISTS (SELECT * FROM Products)
	BEGIN 
		INSERT INTO Products (ProductName, CurrentCount, Price, PriceBeforeSale, Weight, Image)
		VALUES 
		(@ProductName, @CurrentCount, @Price, @PriceBeforeSale, @Weight, @Image)
	END";

	#endregion

	public DbInitService(IDatabase database)
		: base(database)
	{
		Start();
	}

	private void Start()
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
			var productsDataForInsert = GeProductDataForInsert();
			connection.Open();

			foreach(var productEntity in productsDataForInsert)
			{
				string insertDefaultDataInProductTableQuery = _insertDefaultDataInProductTableIfEmptyQuery;
				using(SqlCommand command = new SqlCommand(insertDefaultDataInProductTableQuery, connection))
				{
					command.Parameters.AddWithValue("@ProductName", productEntity.ProductName);
					command.Parameters.AddWithValue("@CurrentCount", productEntity.CurrentCount);
					command.Parameters.AddWithValue("@Price", productEntity.Price);
					command.Parameters.AddWithValue("@PriceBeforeSale", productEntity.PriceBeforeSale ?? -1);
					command.Parameters.AddWithValue("@Weight", productEntity.Weight);
					command.Parameters.AddWithValue("@Image", productEntity.Image);
					command.ExecuteNonQuery();
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
			new ProductEntity() { ProductName = "TomatoesCommon",               Price = 150,  Weight = 250, CurrentCount = 150, Image = File.ReadAllBytes(@"Assets\Images\tomatoes-common-product.png") },
			new ProductEntity() { ProductName = "BananasCommon",                Price = 70,   Weight = 500, CurrentCount = 50,  Image = File.ReadAllBytes(@"Assets\Images\banana-product.png") },
			new ProductEntity() { ProductName = "ApplesGreen",                  Price = 60,   Weight = 400, CurrentCount = 250, Image = File.ReadAllBytes(@"Assets\Images\apples-green-product.png"), PriceBeforeSale = 150 },
			new ProductEntity() { ProductName = "MushroomChampignons",          Price = 90,   Weight = 500, CurrentCount = 450, Image = File.ReadAllBytes(@"Assets\Images\mushroom-champignons-product.png") },
			new ProductEntity() { ProductName = "CucumbersSmoothMediumFruited", Price = 150,  Weight = 300, CurrentCount = 220, Image = File.ReadAllBytes(@"Assets\Images\cucumbers-smooth-medium-fruited-product.png"), PriceBeforeSale = 200 }
		};
	}
}