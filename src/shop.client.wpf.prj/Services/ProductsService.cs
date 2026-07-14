using DynamicData;
using ReactiveUI;
using Shop.Client.WPF.Extensions;
using Shop.Client.WPF.Http;
using Shop.Client.WPF.Views.Pages;
using Shop.Dto.Orders;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace Shop.Client.WPF.Services;
 
public class ProductsService : IProductsService
{
	#region Fields

	private readonly ISourceList<IProductItemVM> _productsInBasket = new SourceList<IProductItemVM>();
	private readonly ISourceList<IProductItemVM> _totalProducts = new SourceList<IProductItemVM>();
	private readonly Subject<decimal?> _resultBasketPriceSubject = new Subject<decimal?>(); 

	private readonly MainInfo _mainInfo;
	private readonly ProductsHttpClient _productsHttpClient;
	private readonly OrdersHttpClient _ordersHttpClient;
	private readonly UsersHttpClient _usersHttpClient;
	private readonly List<IDisposable> _disposables = new List<IDisposable>();

	private decimal? _resultBasketPrice;

	/// <summary>
	/// Цена всех выбранных товаров, находящихся в корзине.
	/// </summary>
	private decimal? ResultBasketPrice
	{
		get => _resultBasketPrice;
		set
		{
			_resultBasketPrice = value;
			_resultBasketPriceSubject.OnNext(value);
		}
	}

	#endregion

	#region Properties

	/// <inheritdoc/>
	public IObservable<decimal?> ResultBasketPriceObservable => _resultBasketPriceSubject.AsObservable();

	#endregion

	#region .ctor

	public ProductsService(
		MainInfo mainInfo,
		ProductsHttpClient productsHttpClient,
		UsersHttpClient usersHttpClient,
		OrdersHttpClient ordersHttpClient)
	{
		_mainInfo = mainInfo;
		_productsHttpClient = productsHttpClient;
		_usersHttpClient = usersHttpClient;
		_ordersHttpClient = ordersHttpClient;

		_productsInBasket
			.Connect()
			.ObserveOn(RxApp.MainThreadScheduler)
			.Subscribe(changeSet =>
			{
				CalculateResultBasketPrice();
			})
			.AddTo(_disposables);
	}

	#endregion

	#region Methods

	/// <inheritdoc/>
	public IObservable<IChangeSet<IProductItemVM>> ConnectToProductsItems()
	{
		return _totalProducts.Connect();
	}

	/// <inheritdoc/>
	public IObservable<IChangeSet<IProductItemVM>> ConnectToProductsInBasketItems()
	{
		return _productsInBasket.Connect();
	}

	/// <inheritdoc/>
	public async Task<bool> UpdateProductsItemsAsync()
	{
		var result = default(bool);

		var products = await _productsHttpClient.FetchAllProducts();

		if(products != null)
		{
			_totalProducts.Clear();
			_productsInBasket.Clear();

			_totalProducts
				.AddRange(products.Products
				.Select(x => x.ConvertToVM(
					addCommand: () => AddProductToBasket(x.Id),
					removeCommand: () => RemoveProductFromBasket(x.Id))));

			//TO DO: ЗАГРУЗКА личной сохраненной ранее корзины пользователя из бд/локального json файла

			result = true;
			return result;
		}

		return result;
	}

	/// <inheritdoc/>
	public Task<bool> AddProductToBasket(int idProduct)
	{
		var result = default(bool);

		var productFromBasket = _productsInBasket.Items.FirstOrDefault(x => x.Id == idProduct);
		if(productFromBasket != null)
		{
			var resultSelectedProductCount = productFromBasket.CurrentSelectedCount + 1;
			if(productFromBasket.CurrentSelectedCount + 1 <= productFromBasket.CurrentCount)
			{
				var productFromTotalList = _totalProducts.Items.FirstOrDefault(x => x.Id == idProduct);
				if(productFromTotalList != null)
				{
					productFromTotalList.CurrentSelectedCount = resultSelectedProductCount;
					productFromBasket.CurrentSelectedCount = resultSelectedProductCount;

					result = true;
				}
			}
		}
		else
		{
			var addProduct = _totalProducts.Items.FirstOrDefault(x => x.Id == idProduct);
			if(addProduct != null)
			{
				var resultSelectedProductCount = addProduct.CurrentSelectedCount + 1;
				addProduct.CurrentSelectedCount = resultSelectedProductCount <= addProduct.CurrentCount ?
												  resultSelectedProductCount :
												  addProduct.CurrentCount;

				addProduct = addProduct.Clone();
				_productsInBasket.Add(addProduct);

				result = true;
			}
		}

		CalculateResultBasketPrice();
		return Task.FromResult(result);
	}

	/// <inheritdoc/>
	public Task<bool> RemoveProductFromBasket(int idProduct)
	{
		var result = default(bool);

		var productInBasket = _productsInBasket.Items.Where(x => x.Id == idProduct).FirstOrDefault();
		if(productInBasket != null)
		{
			var countWithMinus = productInBasket.CurrentSelectedCount - 1;
			productInBasket.CurrentSelectedCount = countWithMinus <= 0 ?
												   0 :
												   countWithMinus;

			var productInTotal = _totalProducts.Items.Where(x => x.Id == idProduct).FirstOrDefault();

			if(productInTotal != null)
			{
				productInTotal.CurrentSelectedCount = productInBasket.CurrentSelectedCount;
			}

			if(productInBasket.CurrentSelectedCount == 0)
			{
				_productsInBasket.Remove(productInBasket);
			}

			result = true;
		}

		CalculateResultBasketPrice();
		return Task.FromResult(result);
	}


	/// <inheritdoc/>
	public Task<bool> AddProductToBasket(int[] idProducts)
	{
		CalculateResultBasketPrice();
		return Task.FromResult(false);
	}

	/// <inheritdoc/>
	public Task<bool> RemoveProductFromBasket(int[] idProducts)
	{
		CalculateResultBasketPrice();
		return Task.FromResult(false);
	}

	/// <inheritdoc/>
	public void TotalRemoveProductFromBasket(int idProduct)
	{
		var productInBasket = _productsInBasket.Items.Where(x => x.Id == idProduct).FirstOrDefault();
		if(productInBasket != null)
		{
			productInBasket.CurrentSelectedCount = 0;
			var productInTotal = _totalProducts.Items.Where(x => x.Id == idProduct).FirstOrDefault();

			if(productInTotal != null)
			{
				productInTotal.CurrentSelectedCount = productInBasket.CurrentSelectedCount;
			}

			_productsInBasket.Remove(productInBasket);
		}
	}

	/// <inheritdoc/>
	public void RemoveAllProductsFromBasket()
	{
		foreach(var product in _totalProducts.Items)
		{
			TotalRemoveProductFromBasket(product.Id);
		}
	}


	/// <inheritdoc/>
	public async Task<bool> CreateOrderAsync()
	{
		var result = default(bool);
		var userEntity = await _usersHttpClient.GetUserByLogin("admin");
		var orderProducts = _productsInBasket.Items.GetOrderProducts();
		//var products = _productsInBasket.Items.GetProductsFromString();

		if(userEntity != null && orderProducts.Count > 0)
		{
			var orderRequest = new CreateOrderRequest(userEntity.Id, orderProducts, userEntity.Address);
			var orderResponse = await _ordersHttpClient.CreateOrder(orderRequest);
			if(orderResponse != null)
			{
				await UpdateProductsItemsAsync();
				result = true;
			}
		}
		return result;
	}

	/// <summary>
	/// Высчитывание итоговой цены за все товары в корзине пользователя.
	/// </summary>
	private void CalculateResultBasketPrice()
	{
		decimal resultSum = 0;
		foreach(var product in _productsInBasket.Items)
		{
			resultSum = resultSum + (product.ResultPrice * product.CurrentSelectedCount);
		}

		ResultBasketPrice = resultSum == 0 ? null : resultSum;
	}

	#endregion
}
