using Microsoft.EntityFrameworkCore.Storage;
using Shop.Server.Repositories;

namespace Shop.Server.Services;

public class UnitOfWork : IUnitOfWork
{
	private readonly ServerContext _context;
	private readonly IUsersRepository _users;
	private readonly IProductsRepository _products;
	private readonly IOrdersRepository _orders;

	/// <inheritdoc/>
	public IUsersRepository Users       => _users;

	/// <inheritdoc/> 
	public IProductsRepository Products => _products;

	/// <inheritdoc/>
	public IOrdersRepository Orders     => _orders;

	public UnitOfWork(
		ServerContext context,
		IUsersRepository users,
		IProductsRepository products,
		IOrdersRepository orders)
	{
		_context = context;

		_users    = users;
		_products = products;
		_orders   = orders;
	}

	/// <inheritdoc/>
	public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default)
		=> await _context.Database.BeginTransactionAsync(ct);

	/// <inheritdoc/>
	public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken ct = default)
		=> await transaction.CommitAsync(ct);

	/// <inheritdoc/>
	public async Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken ct = default)
		=> await transaction.RollbackAsync(ct);

	public void Dispose() => _context.Dispose();
}
