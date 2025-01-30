using System.ComponentModel.DataAnnotations;

namespace Shop.Model.Database.Entities
{
	/// <summary>
	/// Объект, инкапсулирующий пользователя.
	/// </summary> 
	public class UserEntity
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Логин пользователя.
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Адрес пользователя.
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Дата создания.
		/// </summary>
		public DateTime CreatedAt { get; set; }
	}
}
