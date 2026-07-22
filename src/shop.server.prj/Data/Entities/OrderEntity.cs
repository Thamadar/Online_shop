using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Server.Data;

/// <summary>
/// Объект, инкапсулирующий заказ.
/// </summary> 
public class OrderEntity
{
	/// <summary>
	/// Идентификатор заказа.
	/// </summary>
	[Key]
	[NotNull]
	public Guid Id { get; private set; }

	/// <summary>
	/// Идентификатор пользователя.
	/// </summary>
	[NotNull]
	public Guid UserId { get; private set; }

	/// <summary>
	/// Список продуктов в заказе.
	/// </summary>
	[Required]
	public ICollection<OrderProductEntity> OrderProducts { get; private set; }

	/// <summary>
	/// Адрес заказа.
	/// </summary>
	[NotNull]
	public string OrderAddress { get; set; }

	/// <summary>
	/// Дата создания.
	/// </summary>
	[NotNull]
	public DateTime CreatedAt { get; private set; }

	/// <summary>
	/// Дата завершения заказа: успешное завершение или отмена.
	/// </summary>
	public DateTime? CompletedAt { get; private set; }

	/// <summary>
	/// Был ли отменен заказ?
	/// </summary>
	[NotNull]
	public bool IsCancelled { get; private set; } 

	/// <summary>
	/// Завершен ли заказ?
	/// </summary>
	[NotMapped]
	public bool IsCompleted => CompletedAt.HasValue; 

	/// <summary>
	/// Завершение заказа.
	/// </summary>
	public void Complete()
	{
		if(IsCompleted)
			throw new InvalidOperationException("Заказ уже завершен.");

		if(IsCancelled)
			throw new InvalidOperationException("Отмененный заказ нельзя завершить.");

		CompletedAt = DateTime.UtcNow; 
	}

	/// <summary>
	/// Отмена заказа.
	/// </summary>
	public void Cancel()
	{
		if(IsCompleted)
			throw new InvalidOperationException("Завершенный заказ нельзя отменить.");

		IsCancelled = true;
		CompletedAt = DateTime.UtcNow; 
	}
}
