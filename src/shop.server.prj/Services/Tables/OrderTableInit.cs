using Microsoft.Data.SqlClient;
using Shop.Model.Database.Entities;

namespace Shop.Server.Services.Tables;

/// <summary>
/// Объект, инициализирующий таблицу Orders в БД.
/// </summary>
public class OrderTableInit : BaseTableInit<OrderEntity>
{

	#region Queries

	/// <inheritdoc/>
	protected override string CreateTableQuery => @"CREATE TABLE [dbo].Orders(
		[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID() NOT NULL,
		[UserId] UNIQUEIDENTIFIER NOT NULL,
		[Products] NVARCHAR(MAX) NOT NULL,
		[OrderAddress] NVARCHAR(MAX) NOT NULL,
		[CreatedAt] DATETIME NOT NULL)";

	/// <inheritdoc/>
	protected override string InsertDefaultDataInTableQuery => "";

	#endregion

	/// <inheritdoc/>
	protected override string TableName => "Orders"; // TO DO: в константу

	public OrderTableInit(IDatabaseInfo databaseInfo) : base(databaseInfo)
	{ }

	/// <inheritdoc/>
	protected override void DataInsertCommands(SqlConnection connection)
	{ }

	/// <inheritdoc/>
	protected override List<OrderEntity> GetDataForInsert()
	{
		return new List<OrderEntity>()
		{ };
	}
}
