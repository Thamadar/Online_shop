using Avalonia.Media.Imaging;
using ReactiveUI;

namespace Shop.Client.Views.Pages;

public interface IProductItemVM
{
	/// <summary>
	/// Идентификатор товара.
	/// </summary> 
	int Id { get; }

	/// <summary>
	/// Наименование товара.
	/// </summary>
	string ProductName { get; }

	/// <summary>
	/// Текущее доступное количество товаров.
	/// </summary>
	int CurrentCount { get; }

	/// <summary>
	/// Текущая стоимость товара в рублях.
	/// </summary>
	double Price { get; }

	/// <summary>
	/// Стоимость товара до скидки. Может быть null, если нет скидки.
	/// </summary>
	double? PriceBeforeSale { get; }

	/// <summary>
	/// Вес товара за 1 шт. в граммах.
	/// </summary>
	double Weight { get; }

	/// <summary>
	/// Изображение товара.
	/// </summary>
	Bitmap Image { get; }

	/// <summary>
	/// Количество выбранных товаров пользователем
	/// </summary>
	int CurrentSelectedCount { get; set; }

	/// <summary>
	/// Command, реагирующий на нажатие "+". По дефолту пуст и используется стандартный от AddRemoveButton.cs, где плюсуется CurrentSelectedCount.
	/// </summary>
	IReactiveCommand? AddCommand { get; }

	/// <summary>
	/// Command, реагирующий на нажатие "-". По дефолту пуст и используется стандартный от AddRemoveButton.cs, где минусуетя CurrentSelectedCount.
	/// </summary>
	IReactiveCommand? RemoveCommand { get; }

	/// <summary>
	/// Клонирование экземпляра.
	/// </summary> 
	ProductItemVM Clone();
}
