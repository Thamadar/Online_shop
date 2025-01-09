using Avalonia.Media.Imaging;

using ReactiveUI;

using Shop.UI;

namespace Shop.Client.Views.Pages;

/// <summary>
/// Объект, инкапсулирующий товар.
/// </summary> 
public class ProductItemVM : ViewModelBase
{
	private Bitmap _image;
	private int _currentSelectedCount;

	/// <summary>
	/// Идентификатор товара.
	/// </summary> 
	public int Id { get; }

	/// <summary>
	/// Наименование товара.
	/// </summary>
	public string ProductName { get; }

	/// <summary>
	/// Текущее доступное количество товаров.
	/// </summary>
	public int CurrentCount { get; set; }

	/// <summary>
	/// Текущая стоимость товара в рублях.
	/// </summary>
	public double Price { get; }

	/// <summary>
	/// Стоимость товара до скидки. Может быть null, если нет скидки.
	/// </summary>
	public double? PriceBeforeSale { get; }

	/// <summary>
	/// Вес товара за 1 шт. в граммах.
	/// </summary>
	public double Weight { get; }

	/// <summary>
	/// Изображение товара.
	/// </summary>
	public Bitmap Image
	{
		get => _image;
		set => this.RaiseAndSetIfChanged(ref _image, value);
	}

	/// <summary>
	/// Количество выбранных товаров пользователем
	/// </summary>
	public int CurrentSelectedCount
	{
		get => _currentSelectedCount;
		set => this.RaiseAndSetIfChanged(ref _currentSelectedCount, value);
	}

	public ProductItemVM(
		int id,
		string productName,
		int currentCoutn,
		double price,
		double? priceBeforeSale,
		double weigth,
		byte[] image)
	{
		Id = id;
		ProductName = productName;
		CurrentCount = currentCoutn;
		Price = price;
		PriceBeforeSale = priceBeforeSale;
		Weight = weigth;

		Image = GetImageFromByteArray(image);
	}

	public static Bitmap GetImageFromByteArray(byte[] byteArray)
	{
		if(byteArray == null || byteArray.Length == 0)
		{
			return null;
		}

		Bitmap bitmap;

		using(MemoryStream stream = new MemoryStream(byteArray))
		{
			try
			{
				bitmap = new Bitmap(stream);
			}
			catch(Exception ex)
			{
				return null;
			}
		}

		return bitmap;
	}
}
