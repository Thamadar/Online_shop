using ReactiveUI;
using Shop.UI.WPF;
using System.IO;
using System.Windows.Media.Imaging;


namespace Shop.Client.WPF.Desktop.Views.Pages;


/// <inheritdoc/>
public class ProductItemVM : ViewModelBase, IProductItemVM
{
	private BitmapImage _image;
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
	public BitmapImage Image
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
		BitmapImage image,
		int? currentSelectedCount = null,
		IReactiveCommand? addCommand = null,
		IReactiveCommand? removeCommand = null)
		: this(id, productName, currentCount, price, priceBeforeSale, weight, currentSelectedCount, addCommand, removeCommand)
	{ 
		Image                = image; 
	}
	 
	private static BitmapImage GetImageFromByteArray(byte[] byteArray)
	{
		if(byteArray == null || byteArray.Length == 0)
		{
			return null;
		} 

		using(MemoryStream stream = new MemoryStream(byteArray))
		{
			try
			{
				var bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = stream;
				bitmapImage.CacheOption  = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
				bitmapImage.Freeze();
				return bitmapImage;
			}
			catch(Exception)
			{
				return null;
			}
		} 
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
