using Microsoft.Data.SqlClient;
using Shop.Model.Database.Entities; 

namespace Shop.Server.Services.Tables;


/// <summary>
/// Объект, инициализирующий таблицу Product в БД.
/// </summary>
public class ProductTableInit : BaseTableInit<ProductEntity>
{

	#region Queries

	/// <inheritdoc/>
	protected override string CreateTableQuery => @"CREATE TABLE [dbo].Products(
    [Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [ProductName] [varchar](50) NOT NULL,
    [CurrentCount] [int] NOT NULL,
    [Price] [float] NOT NULL,
    [PriceBeforeSale] [float],
    [Weight] [float] NOT NULL,
    [Image] [varbinary](max) NOT NULL)";

	/// <inheritdoc/>
	protected override string InsertDefaultDataInTableQuery => @"
		INSERT INTO Products (ProductName, CurrentCount, Price, PriceBeforeSale, Weight, Image)
		VALUES 
		(@ProductName, @CurrentCount, @Price, @PriceBeforeSale, @Weight, @Image)"; 

	#endregion

	/// <inheritdoc/>
	protected override string TableName => "Products"; 

	public ProductTableInit(IDatabaseInfo databaseInfo) : base(databaseInfo)
	{ }

	/// <inheritdoc/>
	protected override void DataInsertCommands(SqlConnection connection)
	{
		var productsDataForInsert = GetDataForInsert();

		foreach(var productEntity in productsDataForInsert)
		{
			string insertDefaultDataInProductTableQuery = InsertDefaultDataInTableQuery;
			using(SqlCommand insertCommand = new SqlCommand(insertDefaultDataInProductTableQuery, connection))
			{
				insertCommand.Parameters.AddWithValue("@ProductName", productEntity.ProductName);
				insertCommand.Parameters.AddWithValue("@CurrentCount", productEntity.CurrentCount);
				insertCommand.Parameters.AddWithValue("@Price", productEntity.Price);
				insertCommand.Parameters.AddWithValue("@PriceBeforeSale", productEntity.PriceBeforeSale ?? -1);
				insertCommand.Parameters.AddWithValue("@Weight", productEntity.Weight);
				insertCommand.Parameters.AddWithValue("@Image", productEntity.Image);
				insertCommand.ExecuteNonQuery();
			}
		}
	}

	/// <inheritdoc/>
	protected override List<ProductEntity> GetDataForInsert()
	{
		return new List<ProductEntity>()
		{
			new ProductEntity() { ProductName = "TomatoesCommon",               Price = 150,  Weight = 250, CurrentCount = 3,  Image = File.ReadAllBytes(@"Assets\Images\tomatoes-common-product.png") },
			new ProductEntity() { ProductName = "BananasCommon",                Price = 70,   Weight = 500, CurrentCount = 5,  Image = File.ReadAllBytes(@"Assets\Images\banana-product.png") },
			new ProductEntity() { ProductName = "ApplesGreen",                  Price = 60,   Weight = 400, CurrentCount = 7,  Image = File.ReadAllBytes(@"Assets\Images\apples-green-product.png"), PriceBeforeSale = 150 },
			new ProductEntity() { ProductName = "MushroomChampignons",          Price = 90,   Weight = 500, CurrentCount = 3,  Image = File.ReadAllBytes(@"Assets\Images\mushroom-champignons-product.png") },
			new ProductEntity() { ProductName = "CucumbersSmoothMediumFruited", Price = 150,  Weight = 300, CurrentCount = 8,  Image = File.ReadAllBytes(@"Assets\Images\cucumbers-smooth-medium-fruited-product.png"), PriceBeforeSale = 200 },
			new ProductEntity() { ProductName = "BlackBreadDarizkiy",			Price = 30,   Weight = 350, CurrentCount = 15, Image = File.ReadAllBytes(@"Assets\Images\black-bread-darizkiy-product.png"), PriceBeforeSale = 50 },
			new ProductEntity() { ProductName = "GrapeGreen",                   Price = 95,   Weight = 500, CurrentCount = 6,  Image = File.ReadAllBytes(@"Assets\Images\grape-green-product.png"), PriceBeforeSale = 225 }
		};
	}  
}
