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
	public Guid Id { get; set; }

	/// <summary>
	/// Идентификатор пользователя.
	/// </summary>
	[NotNull]
	public Guid UserId { get; set; }

	/// <summary>
	/// Товары из заказа. (id, количество. Разделение через ;).
	/// Пример: "2,5;3,1;6,3"
	/// TO DO: отдельная таблица. Убрать эту логику, заменив на ту, что ProductLocalizationEntity.
	/// </summary>
	[NotNull]
	public string Products { get; set; }

	/// <summary>
	/// Адрес заказа.
	/// </summary>
	[NotNull]
	public string OrderAddress { get; set; }

	/// <summary>
	/// Дата создания.
	/// </summary>
	[NotNull]
	public DateTime CreatedAt { get; set; }

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
