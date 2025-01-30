using System.ComponentModel.DataAnnotations;

namespace Shop.Model.Database.Entities
{
	/// <summary>
	/// Объект, инкапсулирующий заказ.
	/// </summary> 
	public class OrderEntity
	{
		/// <summary>
		/// Идентификатор заказа.
		/// </summary>
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// Товары из заказа. (id, количество. Разделение через ;).
		/// Пример: "2,5;3,1;6,3"
		/// </summary>
		public string Products { get; set; }

		/// <summary>
		/// Адрес заказа.
		/// </summary>
		public string OrderAddress { get; set; }

		/// <summary>
		/// Дата создания.
		/// </summary>
		public DateTime CreatedAt { get; set; }
	}
}
