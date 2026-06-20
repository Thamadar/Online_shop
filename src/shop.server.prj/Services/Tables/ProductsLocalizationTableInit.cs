using Microsoft.Data.SqlClient;
using Shop.Server.Data;

namespace Shop.Server.Services.Tables;

/// <summary>
/// Объект, инициализирующий таблицу ProductsLocalization в БД.
/// </summary>
public class ProductsLocalizationTableInit : BaseTableInit<ProductLocalizationEntity>
{
	#region Queries

	/// <inheritdoc/>
	protected override string CreateTableQuery => $@"CREATE TABLE [dbo].{TableName}(
		[ProductId]   INT           NOT NULL,
		[LangCode]    VARCHAR(10)   NOT NULL,
		[DisplayName] NVARCHAR(MAX) NOT NULL,

		CONSTRAINT [PK_ProductsLocalization] PRIMARY KEY CLUSTERED ([ProductId], [LangCode]),
		CONSTRAINT [FK_ProductsLocalization_Products_ProductId]
		    FOREIGN KEY ([ProductId])
		    REFERENCES [dbo].[Products] ([Id])
		    ON DELETE CASCADE)";

	/// <inheritdoc/>
	protected override string InsertDefaultDataInTableQuery => $@"";

	#endregion

	/// <inheritdoc/>
	protected override string TableName => "ProductsLocalization";

	public ProductsLocalizationTableInit(IDatabaseInfo databaseInfo) : base(databaseInfo)
	{ }

	/// <inheritdoc/>
	protected override void DataInsertCommands(SqlConnection connection)
	{  }

	/// <inheritdoc/>
	protected override List<ProductLocalizationEntity> GetDataForInsert()
	{
		return new List<ProductLocalizationEntity>()
		{ };
	}
}

