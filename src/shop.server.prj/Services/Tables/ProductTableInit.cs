using Microsoft.Data.SqlClient; 
using Shop.Utilities;
using Shop.Server.Data;

namespace Shop.Server.Services.Tables;


/// <summary>
/// Объект, инициализирующий таблицу Product в БД.
/// </summary>
public class ProductTableInit : BaseTableInit<ProductEntity>
{
	private readonly IServerService _productsLocalizationTable;

	#region Queries

	/// <inheritdoc/>
	protected override string CreateTableQuery => $@"CREATE TABLE [dbo].Products(
    [Id] [int] IDENTITY(1,1) NOT NULL,  
    [ProductName] [varchar](100) NOT NULL,
    [CurrentCount] [int] NOT NULL,
    [Price] [float] NOT NULL,
    [PriceBeforeSale] [float],
    [Weight] [float] NOT NULL,
    [Image] [varbinary](max) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id]))";

	/// <inheritdoc/>
	protected override string InsertDefaultDataInTableQuery => $@"
		INSERT INTO {TableName} (ProductName, CurrentCount, Price, PriceBeforeSale, Weight, Image)
	    OUTPUT INSERTED.Id
		VALUES (@ProductName, @CurrentCount, @Price, @PriceBeforeSale, @Weight, @Image)"; 

	#endregion

	/// <inheritdoc/>
	protected override string TableName => "Products"; 

	public ProductTableInit(IDatabaseInfo databaseInfo) : base(databaseInfo)
	{
		_productsLocalizationTable = new ProductsLocalizationTableInit(databaseInfo); 
	}

	protected override void TableInitialization()
	{ 
		CheckCreateTable();
		_productsLocalizationTable.Initialization();
		CheckInsertDataTable();
	}

	/// <inheritdoc/>
	protected override void DataInsertCommands(SqlConnection connection)
	{
		var productsDataForInsert = GetDataForInsert();

		foreach(var productEntity in productsDataForInsert)
		{
			using(var transaction = connection.BeginTransaction())
			{
				try
				{
					 
					int productId;
					string insertDefaultDataInProductTableQuery = InsertDefaultDataInTableQuery;
					using(SqlCommand insertCommand = new SqlCommand(insertDefaultDataInProductTableQuery, connection, transaction))
					{
						//insertCommand.Parameters.AddWithValue("@Slug", productEntity.Slug);
						insertCommand.Parameters.AddWithValue("@ProductName", productEntity.ProductName);
						insertCommand.Parameters.AddWithValue("@CurrentCount", productEntity.CurrentCount);
						insertCommand.Parameters.AddWithValue("@Price", productEntity.Price);
						insertCommand.Parameters.AddWithValue("@PriceBeforeSale", productEntity.PriceBeforeSale ?? -1);
						insertCommand.Parameters.AddWithValue("@Weight", productEntity.Weight);
						insertCommand.Parameters.AddWithValue("@Image", productEntity.Image);

						var result = insertCommand.ExecuteScalar();

						if(result == DBNull.Value)
							throw new InvalidOperationException("Failed to retrieve product ID after insert.");
						productId = (int)result;
					} 

					// 3. Вставка локализаций
					if(productEntity.Localizations != null && productEntity.Localizations.Any())
					{
						string insertLocalizationQuery = @"
					INSERT INTO ProductsLocalization (ProductId, LangCode, DisplayName)
					VALUES (@ProductId, @LangCode, @DisplayName)";

						foreach(var loc in productEntity.Localizations)
						{
							using(SqlCommand cmd = new SqlCommand(insertLocalizationQuery, connection, transaction))
							{
								cmd.Parameters.AddWithValue("@ProductId", productId);
								cmd.Parameters.AddWithValue("@LangCode", loc.LangCode);
								cmd.Parameters.AddWithValue("@DisplayName", loc.DisplayName);
								cmd.ExecuteNonQuery();
							}
						}
					}
					transaction.Commit();
				}
				catch
				{ 
					transaction.Rollback();
					throw; 
				}
			}
		}
	}

	/// <inheritdoc/>
	protected override List<ProductEntity> GetDataForInsert()
	{
		//TO DO: add builder.
		return new List<ProductEntity>()
		{
			new ProductEntity() { ProductName = "TomatoesCommon", Price = 150, Weight = 250, CurrentCount = 3, Image = File.ReadAllBytes(@"Assets\Images\tomatoes-common-product.png"),
				Localizations = new List<ProductLocalizationEntity>
				{
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.EN,
						DisplayName = "Tomatoes Common"
					},
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.RU,
						DisplayName = "Томаты обычные"
					}
				}},
			new ProductEntity() { ProductName = "BananasCommon", Price = 70, Weight = 500, CurrentCount = 5, Image = File.ReadAllBytes(@"Assets\Images\banana-product.png"),
				Localizations = new List<ProductLocalizationEntity>
				{
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.EN,
						DisplayName = "Bananas Common"
					},
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.RU,
						DisplayName = "Бананы обычные"
					}
				} },
			new ProductEntity() { ProductName = "ApplesGreen", Price = 60, Weight = 400, CurrentCount = 7, Image = File.ReadAllBytes(@"Assets\Images\apples-green-product.png"), PriceBeforeSale = 150,
				Localizations = new List<ProductLocalizationEntity>
				{
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.EN,
						DisplayName = "Apples Green"
					},
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.RU,
						DisplayName = "Яблоки зеленые"
					}
				} },
			new ProductEntity() { ProductName = "MushroomChampignons", Price = 90, Weight = 500, CurrentCount = 3, Image = File.ReadAllBytes(@"Assets\Images\mushroom-champignons-product.png"),
				Localizations = new List<ProductLocalizationEntity>
				{
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.EN,
						DisplayName = "Mushroom Champignons"
					},
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.RU,
						DisplayName = "Шампиньоны"
					}
				} },
			new ProductEntity() { ProductName = "CucumbersSmoothMediumFruited", Price = 150,  Weight = 300, CurrentCount = 8, Image = File.ReadAllBytes(@"Assets\Images\cucumbers-smooth-medium-fruited-product.png"), PriceBeforeSale = 200,
				Localizations = new List<ProductLocalizationEntity>
				{
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.EN,
						DisplayName = "Cucumbers Smooth Medium Fruited"
					},
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.RU,
						DisplayName = "Огурцы гладкие среднеплодные"
					}
				} },
			new ProductEntity() { ProductName = "BlackBreadDarizkiy", Price = 30, Weight = 350, CurrentCount = 15, Image = File.ReadAllBytes(@"Assets\Images\black-bread-darizkiy-product.png"), PriceBeforeSale = 50,
				Localizations = new List<ProductLocalizationEntity>
				{
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.EN,
						DisplayName = "Black Bread Darizkiy"
					},
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.RU,
						DisplayName = "Черный хлеб Дарницкий"
					}
				} },
			new ProductEntity() { ProductName = "GrapeGreen",  Price = 95, Weight = 500, CurrentCount = 6, Image = File.ReadAllBytes(@"Assets\Images\grape-green-product.png"), PriceBeforeSale = 225,
				Localizations = new List<ProductLocalizationEntity>
				{
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.EN,
						DisplayName = "Grape Green"
					},
					new ProductLocalizationEntity
					{
						LangCode = CultureConstants.RU,
						DisplayName = "Виноград зеленый"
					}
				} }
		};
	}  
}
