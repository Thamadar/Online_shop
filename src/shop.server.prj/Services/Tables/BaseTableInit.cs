using Microsoft.Data.SqlClient; 

namespace Shop.Server.Services.Tables;

/// <summary>
/// Базовый класс, имеющий в себе все необходимое для инициализации таблицы.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseTableInit<T> : IServerService
	where T : class
{

	#region Queries
	 
	/// <summary>
	/// Проверка и создание БД.
	/// </summary>
	private const string _checkIsDatabaseExistAndCreateQuery = @"IF NOT EXISTS
	(SELECT * FROM sys.databases WHERE name = 'OnlineShop')
	CREATE DATABASE OnlineShop";


	/// <summary>
	/// Получение количества текущих строк из таблицы.
	/// </summary>
	private string _getCountItemsFromTableQuery;

	/// <summary>
	/// Проверка на наличие таблицы.
	/// </summary>
	private string _checkIsTableExistQuery;

	/// <summary>
	/// Создание таблицы.
	/// </summary>
	protected abstract string CreateTableQuery { get; }

	/// <summary>
	/// Автозаполнение, если таблица пуста. (опционально)
	/// </summary>
	protected abstract string InsertDefaultDataInTableQuery { get; } 

	#endregion

	/// <summary>
	/// Наименование таблицы.
	/// </summary>
	protected abstract string TableName { get; } 

	public IDatabaseInfo DatabaseInfo { get; }

	public BaseTableInit(IDatabaseInfo databaseInfo)
	{
		DatabaseInfo = databaseInfo;

		_checkIsTableExistQuery = $@"IF NOT EXISTS
		(SELECT * FROM information_schema.tables WHERE table_name = '{TableName}')";

		_getCountItemsFromTableQuery = $@"
		SELECT COUNT(*) FROM {TableName}";
	}

	/// <summary>
	/// Базовая инициализация: проверка/создание БД, проверка/создание таблицы
	/// </summary>
	public void Initialization()
	{
		CheckInitDB();

		ConsoleLog.Write($"Инициализация таблицы {TableName}."); 
		TableInitialization(); 
		ConsoleLog.WriteGreen($"Инициализация таблицы {TableName} завершено!"); 
	}

	/// <summary>
	/// Проверка и создание (если её нет) таблицы, а также её автозаполнение, если нет внутри данных (для тестов).
	/// </summary>
	protected virtual void TableInitialization()
	{
		CheckCreateTable(); 
		CheckInsertDataTable();
	}

	/// <summary>
	/// Проверка, что таблица существует. Если отсутствует, то создаем новую. 
	/// </summary>
	protected virtual void CheckCreateTable()
	{ 
		using(SqlConnection connection = new SqlConnection(DatabaseInfo.ConnectionString))
		{
			connection.Open();
			string createTableQuery = $"{_checkIsTableExistQuery} {CreateTableQuery}";
			SqlCommand createTableCommand = new SqlCommand(createTableQuery, connection);
			createTableCommand.ExecuteNonQuery();
		}
	}

	/// <summary>
	/// Проверка, что таблица пуста. Если пуста, то заполняем стартовыми данными. (опционально)
	/// </summary>
	protected virtual void CheckInsertDataTable()
	{
		if(InsertDefaultDataInTableQuery == "")
		{
			return;
		}

		using(SqlConnection connection = new SqlConnection(DatabaseInfo.ConnectionString))
		{
			connection.Open();

			using(SqlCommand getCountCommand = new SqlCommand(_getCountItemsFromTableQuery, connection))
			{
				int countProducts = (int)getCountCommand.ExecuteScalar();

				if(countProducts == 0)
				{
					DataInsertCommands(connection); 
				}
			}
		}
	}

	/// <summary>
	/// Заполнение таблицы начальными данными через foreach и SQLCommand. (опционально).
	/// Пример: (insertCommand.Parameters.AddWithValue("@ColumnName", tItem.ColumnName))
	/// </summary> 
	protected abstract void DataInsertCommands(SqlConnection connection);

	/// <summary>
	/// Данные для автозаполнения таблицы. (опционально)
	/// </summary> 
	protected abstract List<T> GetDataForInsert();

	/// <summary>
	/// Проверка и создание (если её нет) БД.
	/// </summary>
	private void CheckInitDB()
	{ 
		using(SqlConnection connection = new SqlConnection(DatabaseInfo.ServerConnectionString))
		{
			connection.Open();
			string createDatabaseQuery = _checkIsDatabaseExistAndCreateQuery;
			SqlCommand createDatabaseCommand = new SqlCommand(createDatabaseQuery, connection);
			createDatabaseCommand.ExecuteNonQuery();
		} 
	}
}
