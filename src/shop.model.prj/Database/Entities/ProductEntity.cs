﻿using System.ComponentModel.DataAnnotations;

namespace Shop.Model.Database.Entities
{
	/// <summary>
	/// Объект, инкапсулирующий товар.
	/// </summary> 
	public class ProductEntity
	{
		/// <summary>
		/// Идентификатор товара.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Наименование товара.
		/// </summary>
		public string ProductName { get; set; }

		/// <summary>
		/// Текущее доступное количество товаров.
		/// </summary>
		public int CurrentCount { get; set; }

		/// <summary>
		/// Текущая стоимость товара в рублях.
		/// </summary>
		public double Price { get; set; }

		/// <summary>
		/// Стоимость товара до скидки. Может быть null, если нет скидки.
		/// </summary>
		public double? PriceBeforeSale { get; set; } // Discont (размер скидки)

		/// <summary>
		/// Вес товара за 1 шт. в граммах.
		/// </summary>
		public double Weight { get; set; }

		/// <summary>
		/// Изображение товара.
		/// </summary>
		public byte[] Image { get; set; }
	}
}
