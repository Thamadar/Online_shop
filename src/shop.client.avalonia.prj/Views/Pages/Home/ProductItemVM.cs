using Avalonia.Media.Imaging;

using ReactiveUI;

using Shop.UI.Avalonia;

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
	public double Price { get; }


	/// <inheritdoc/>
	public double? PriceBeforeSale { get; }


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
	public IReactiveCommand? AddCommand { get; }

	/// <inheritdoc/>
	public IReactiveCommand? RemoveCommand { get; }


	/// <summary>
	/// VM-Объект, инкапсулирующий товар.
	/// </summary> 
	private ProductItemVM(
		int id,
		string productName,
		int currentCount,
		double price,
		double? priceBeforeSale,
		double weight,
		int? currentSelectedCount       = null,
		IReactiveCommand? addCommand    = null,
		IReactiveCommand? removeCommand = null)
	{
		Id                   = id;
		ProductName          = productName;
		CurrentCount         = currentCount;
		Price                = price; 
		Weight               = weight;
		CurrentSelectedCount = currentSelectedCount ?? 0;
		PriceBeforeSale      = priceBeforeSale == -1 ?
							   null :
							   priceBeforeSale;

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
		double price,
		double? priceBeforeSale,
		double weight,
		byte[] image,
		int? currentSelectedCount       = null,
		IReactiveCommand? addCommand    = null,
		IReactiveCommand? removeCommand = null)
		: this(id, productName, currentCount, price, priceBeforeSale, weight, currentSelectedCount, addCommand, removeCommand)
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
		double price,
		double? priceBeforeSale,
		double weight, 
		Bitmap image,
		int? currentSelectedCount = null,
		IReactiveCommand? addCommand = null,
		IReactiveCommand? removeCommand = null)
		: this(id, productName, currentCount, price, priceBeforeSale, weight, currentSelectedCount, addCommand, removeCommand)
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
			Price,
			PriceBeforeSale,
			Weight,
			Image,
			CurrentSelectedCount,
			AddCommand,
			RemoveCommand); 
	}
}
