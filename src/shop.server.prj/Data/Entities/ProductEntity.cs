using Shop.Dto.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Server.Data;
  
/// <summary>
/// Объект, инкапсулирующий товар.
/// </summary> 
public class ProductEntity
{
	//TO DO: добавить классификацию товара: овощи, мясо, ...

	/// <summary>
	/// Идентификатор товара.
	/// </summary>
	[Key]
	public int Id { get; set; }
	 
	/// <summary>
	/// Ключ для локализации наименования продукта.
	/// </summary>
	[Required]
	[MaxLength(100)]
	public string ProductName { get; set; }

	/// <summary>
	/// Локализация.
	/// </summary>
	[Required] 
	public ICollection<ProductLocalizationEntity> Localizations { get; set; }
	 
	/// <summary>
	/// Текущее доступное количество товаров.
	/// </summary>
	[Required]
	public int CurrentCount { get; set; }

	/// <summary>
	/// Базовая стоимость товара в рублях.
	/// </summary>
	[Required]
	[Column(TypeName = "decimal(18,2)")]
	public decimal BasePrice { get; set; }

	/// <summary>
	/// Размер скидки. Тип единицы измерения - DiscountUnit.
	/// </summary>
	[Required]
	[Column(TypeName = "decimal(18,2)")]
	public decimal DiscountValue { get; private set; }

	/// <summary>
	/// Тип измерения скидки.
	/// </summary>
	[Required]
	public DiscountUnit DiscountUnit { get; private set; } = DiscountUnit.Percent;
	 
	/// <summary>
	/// Итоговая цена товара.
	/// </summary>
	[NotMapped]
	public decimal ResultPrice => CalculateResultPrice();

	/// <summary>
	/// Вес товара за 1 шт. в граммах.
	/// </summary>
	[Required]
	public int Weight { get; set; }

	/// <summary>
	/// Изображение товара.
	/// </summary>
	[Required]
	public byte[] Image { get; set; }

	/// <summary>
	/// Установить скидку товара.
	/// </summary> 
	public void SetDiscount(decimal discountValue, DiscountUnit discountUnit)
	{ 
		if(discountUnit == DiscountUnit.Percent)
		{
			discountValue = Math.Clamp(discountValue, 0m, 100m);
		}
		else if(discountUnit == DiscountUnit.FixedAmount)
		{
			discountValue = Math.Clamp(DiscountValue, 0m, BasePrice);  
		}

		DiscountValue = discountValue;
		DiscountUnit  = discountUnit;
	}

	/// <summary>
	/// Удалить скидку товара.
	/// </summary>
	public void ClearDiscount()
	{
		DiscountValue = 0;
		DiscountUnit  = DiscountUnit.Percent;
	}

	/// <summary>
	/// Высчитать итоговую цену, учитывая скидку.
	/// </summary>  
	private decimal CalculateResultPrice()
	{
		if(DiscountValue == 0)
			return BasePrice;

		decimal? resultPrice = null; 
		if(DiscountUnit == DiscountUnit.Percent)
		{
			var discountValue = Math.Clamp(DiscountValue, 0m, 100m);  
			resultPrice       = BasePrice - (BasePrice * discountValue / 100m);
		}
		else if(DiscountUnit == DiscountUnit.FixedAmount)
		{
			var discountAmount = Math.Clamp(DiscountValue, 0m, BasePrice);
			resultPrice        = BasePrice - discountAmount;
		} 

		return resultPrice ?? BasePrice;
	}
}
