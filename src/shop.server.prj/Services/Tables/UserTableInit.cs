using Microsoft.Data.SqlClient;

using Shop.Model.Database.Entities;

namespace Shop.Server.Services.Tables;

/// <summary>
/// Объект, инициализирующий таблицу Users в БД.
/// </summary>
public class UserTableInit : BaseTableInit<UserEntity>
{

	#region Queries

	/// <inheritdoc/>
	protected override string CreateTableQuery => @"CREATE TABLE [dbo].Users(
		[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID() NOT NULL,
		[Password] NVARCHAR(MAX) NOT NULL,
		[Login] NVARCHAR(100) UNIQUE NOT NULL,
		[Address] NVARCHAR(MAX) NOT NULL,
		[CreatedAt] DATETIME NOT NULL)";

	/// <inheritdoc/>
	protected override string InsertDefaultDataInTableQuery => @"INSERT INTO dbo.Users
		(Password, Login, Address, CreatedAt)
		VALUES
		(@Password, @Login, @Address, @CreatedAt)";

	#endregion

	/// <inheritdoc/>
	protected override string TableName => "Users";

	public UserTableInit(IDatabaseInfo databaseInfo) : base(databaseInfo)
	{ }

	/// <inheritdoc/>
	protected override void DataInsertCommands(SqlConnection connection)
	{
		var itemsDataForInsert = GetDataForInsert();

		foreach(var itemDataForInsert in itemsDataForInsert)
		{
			string insertDefaultDataInProductTableQuery = InsertDefaultDataInTableQuery;
			using(SqlCommand insertCommand = new SqlCommand(insertDefaultDataInProductTableQuery, connection))
			{ 
				insertCommand.Parameters.AddWithValue("@Login",     itemDataForInsert.Login);
				insertCommand.Parameters.AddWithValue("@Password",  itemDataForInsert.Password);
				insertCommand.Parameters.AddWithValue("@Address",   itemDataForInsert.Address);
				insertCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
				insertCommand.ExecuteNonQuery();
			}
		}
	}

	/// <inheritdoc/>
	protected override List<UserEntity> GetDataForInsert()
	{
		return new List<UserEntity>()
		{
			new UserEntity() { Login = "admin", Password = "admin", Address = "Россия; Московская область; Москва; улица Ленина; дом 13; кв. 45; подъезд 2" }
		};
	}
}
