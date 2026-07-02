using Avalonia.Media.Imaging;

using ReactiveUI;
using Shop.Dto.Products;
using Shop.UI.Avalonia;
using System.Windows.Input;

namespace Shop.Client.Avalonia.Views.Pages;


/// <inheritdoc/>
public class ProductItemVM : ViewModelBase, IProductItemVM
{
	private Bitmap _image;
	private int _currentSelectedCount;
 
	/// <inheritdoc/>
	public int Id { get; }
	 
	/// <inheritdoc/>
	public string ProductName { get; }
	 
	/// <inheritdoc/>
	public int CurrentCount { get; set; }
	 
	/// <inheritdoc/>
	public decimal BasePrice { get; }

	/// <inheritdoc/>
	public decimal ResultPrice { get; }

	/// <inheritdoc/>
	public decimal? DiscountValue { get; }

	/// <inheritdoc/>
	public DiscountUnit DiscountUnit { get; }

	/// <inheritdoc/>
	public double Weight { get; }
	 
	/// <inheritdoc/>
	public Bitmap Image
	{
		get => _image;
		set => this.RaiseAndSetIfChanged(ref _image, value);
	}

	/// <inheritdoc/>
	public int CurrentSelectedCount
	{
		get => _currentSelectedCount;
		set => this.RaiseAndSetIfChanged(ref _currentSelectedCount, value);
	}

	/// <inheritdoc/>
	public ICommand? AddCommand { get; }

	/// <inheritdoc/>
	public ICommand? RemoveCommand { get; }


	/// <summary>
	/// VM-Объект, инкапсулирующий товар.
	/// </summary> 
	private ProductItemVM(
		int id,
		string productName,
		int currentCount,
		decimal basePrice,
		decimal resultPrice,
		decimal discountValue,
		DiscountUnit discountUnit,
		double weight,
		int? currentSelectedCount       = null,
		ICommand? addCommand    = null,
		ICommand? removeCommand = null)
	{
		Id                   = id;
		ProductName          = productName;
		CurrentCount         = currentCount;
		BasePrice            = basePrice; 
		ResultPrice          = resultPrice;
		DiscountValue        = discountValue == 0 ? null : discountValue;
		DiscountUnit         = discountUnit;
		Weight               = weight;
		CurrentSelectedCount = currentSelectedCount ?? 0; 

		if(addCommand != null)
		{
			AddCommand = addCommand;
		}
		if(removeCommand != null)
		{
			RemoveCommand = removeCommand;
		}
	}

	/// <summary>
	/// VM-Объект, инкапсулирующий товар. Для Image в виде byte[].
	/// </summary> 
	public ProductItemVM(
		int id,
		string productName,
		int currentCount,
		decimal basePrice,
		decimal resultPrice,
		decimal discountValue,
		DiscountUnit discountUnit,
		double weight,
		byte[] image,
		int? currentSelectedCount       = null,
		ICommand? addCommand    = null,
		ICommand? removeCommand = null)
		: this(id, productName, currentCount, basePrice, resultPrice, discountValue, discountUnit, weight, currentSelectedCount, addCommand, removeCommand)
	{ 
		Image = GetImageFromByteArray(image); 
	}

	/// <summary>
	/// VM-Объект, инкапсулирующий товар. Для Image в виде Bitmap.
	/// </summary> 
	public ProductItemVM(
		int id,
		string productName,
		int currentCount,
		decimal basePrice,
		decimal resultPrice,
		decimal discountValue,
		DiscountUnit discountUnit,
		double weight,
		Bitmap image,
		int? currentSelectedCount = null,
		ICommand? addCommand = null,
		ICommand? removeCommand = null)
		: this(id, productName, currentCount, basePrice, resultPrice, discountValue, discountUnit, weight, currentSelectedCount, addCommand, removeCommand)
	{ 
		Image                = image; 
	}
	 
	private static Bitmap GetImageFromByteArray(byte[] byteArray)
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
	 
	/// <inheritdoc/>
	public ProductItemVM Clone()
	{
		return new ProductItemVM(
			Id,
			ProductName,
			CurrentCount,
			BasePrice,
			ResultPrice,
			DiscountValue ?? 0,
			DiscountUnit,
			Weight,
			Image,
			CurrentSelectedCount,
			AddCommand,
			RemoveCommand); 
	}
}
