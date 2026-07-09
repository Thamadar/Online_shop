using Microsoft.EntityFrameworkCore.Storage;
using Shop.Server.Repositories;

namespace Shop.Server.Services;

public interface IUnitOfWork : IDisposable
{
	/// <summary>
	/// Репозиторий пользователей.
	/// </summary>
	IUsersRepository Users { get; }
	/// <summary>
	/// Репозиторий товаров.
	/// </summary>
	IProductsRepository Products { get; }
	/// <summary>
	/// Репозиторий заказов.
	/// </summary>
	IOrdersRepository Orders { get; } 

	/// <summary>
	/// Начать транзакцию.
	/// </summary> 
	Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default);
	/// <summary>
	/// Завершить транзакцию.
	/// </summary> 
	Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken ct = default);
	/// <summary>
	/// Откатить транзакцию.
	/// </summary> 
	Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken ct = default);
}
