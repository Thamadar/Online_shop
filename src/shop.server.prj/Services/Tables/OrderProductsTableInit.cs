using Microsoft.Data.SqlClient;
using Shop.Server.Data;

namespace Shop.Server.Services.Tables;

/// <summary>
/// Объект, инициализирующий таблицу OrderProducts в БД.
/// </summary>
public class OrderProductsTableInit : BaseTableInit<OrderProductEntity>
{
	#region Queries

	/// <inheritdoc/>
	protected override string CreateTableQuery => $@"CREATE TABLE [dbo].{TableName}(
		[OrderId] UNIQUEIDENTIFIER NOT NULL,
        [ProductId] INT NOT NULL,
        [Quantity] INT NOT NULL DEFAULT 1, 

		CONSTRAINT PK_OrdersProducts PRIMARY KEY (OrderId, ProductId),
		CONSTRAINT FK_OrdersProducts_Orders 
			FOREIGN KEY (OrderId) 
			REFERENCES Orders(Id) 
			ON DELETE CASCADE)";

	/// <inheritdoc/>
	protected override string InsertDefaultDataInTableQuery => $@"";

	#endregion

	/// <inheritdoc/>
	protected override string TableName => "OrderProducts";

	public OrderProductsTableInit(IDatabaseInfo databaseInfo) : base(databaseInfo)
	{ }

	/// <inheritdoc/>
	protected override void DataInsertCommands(SqlConnection connection)
	{ }

	/// <inheritdoc/>
	protected override List<OrderProductEntity> GetDataForInsert()
	{
		return new List<OrderProductEntity>()
		{ };
	}
}

