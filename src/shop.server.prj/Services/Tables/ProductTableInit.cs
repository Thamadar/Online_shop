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
    [AvailableCount] [int] NOT NULL,
    [BasePrice] [decimal](18,2) NOT NULL,
    [DiscountValue] [decimal](18,2) NOT NULL,
    [DiscountUnit] [int] NOT NULL,
    [Weight] [int] NOT NULL,
    [Image] [varbinary](max) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id]))";

	/// <inheritdoc/>
	protected override string InsertDefaultDataInTableQuery => $@"
		INSERT INTO {TableName} (ProductName, AvailableCount, BasePrice, DiscountValue, DiscountUnit, Weight, Image)
	    OUTPUT INSERTED.Id
		VALUES (@ProductName, @AvailableCount, @BasePrice, @DiscountValue, @DiscountUnit, @Weight, @Image)"; 

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
						insertCommand.Parameters.AddWithValue("@ProductName", productEntity.ProductName);
						insertCommand.Parameters.AddWithValue("@AvailableCount", productEntity.AvailableCount);
						insertCommand.Parameters.AddWithValue("@BasePrice", productEntity.BasePrice);
						insertCommand.Parameters.AddWithValue("@DiscountValue", productEntity.DiscountValue);
						insertCommand.Parameters.AddWithValue("@DiscountUnit", productEntity.DiscountUnit);
						insertCommand.Parameters.AddWithValue("@Weight", productEntity.Weight);
						insertCommand.Parameters.AddWithValue("@Image", productEntity.Image);

						var result = insertCommand.ExecuteScalar();

						if(result == DBNull.Value)
							throw new InvalidOperationException("Failed to retrieve product ID after insert.");
						productId = (int)result;
					} 
					 
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
			new ProductEntity() { ProductName = "TomatoesCommon", BasePrice = 150, Weight = 250, AvailableCount = 3, Image = File.ReadAllBytes(@"Assets\Images\tomatoes-common-product.png"),
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
			new ProductEntity() { ProductName = "BananasCommon", BasePrice = 70, Weight = 500, AvailableCount = 5, Image = File.ReadAllBytes(@"Assets\Images\banana-product.png"),
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
			new ProductEntity() { ProductName = "ApplesGreen", BasePrice = 60, Weight = 400, AvailableCount = 7, Image = File.ReadAllBytes(@"Assets\Images\apples-green-product.png"),
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
			new ProductEntity() { ProductName = "MushroomChampignons", BasePrice = 90, Weight = 500, AvailableCount = 3, Image = File.ReadAllBytes(@"Assets\Images\mushroom-champignons-product.png"),
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
			new ProductEntity() { ProductName = "CucumbersSmoothMediumFruited", BasePrice = 150,  Weight = 300, AvailableCount = 8, Image = File.ReadAllBytes(@"Assets\Images\cucumbers-smooth-medium-fruited-product.png"),
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
			new ProductEntity() { ProductName = "BlackBreadDarizkiy", BasePrice = 30, Weight = 350, AvailableCount = 15, Image = File.ReadAllBytes(@"Assets\Images\black-bread-darizkiy-product.png"),
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
			new ProductEntity() { ProductName = "GrapeGreen",  BasePrice = 95, Weight = 500, AvailableCount = 6, Image = File.ReadAllBytes(@"Assets\Images\grape-green-product.png"),
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
