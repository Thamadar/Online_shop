using DynamicData;
using Shop.Client.Extensions;
using Shop.Client.Http;
using Shop.Client.Views.Pages;
using Shop.Model.Database.Entities;
using System.Text;

namespace Shop.Client.Services;
 
public class ProductsService : IProductsService
{
	#region Fields

	private readonly ISourceList<IProductItemVM> _productsInBasket  = new SourceList<IProductItemVM>();
	private readonly ISourceList<IProductItemVM> _totalProducts     = new SourceList<IProductItemVM>();

	private readonly ProductsHttpClient _productsHttpClient;
	private readonly OrdersHttpClient _ordersHttpClient;
	private readonly MainInfo _mainInfo;

	#endregion

	#region Properties

	#endregion

	#region .ctor

	public ProductsService(
		MainInfo mainInfo,
		ProductsHttpClient productsHttpClient,
		OrdersHttpClient ordersHttpClient)
	{
		_mainInfo           = mainInfo;
		_productsHttpClient = productsHttpClient;
		_ordersHttpClient   = ordersHttpClient;
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
	public async Task<bool> UpdateProductsItems()
	{
		var result = default(bool);

		var products = await _productsHttpClient.FetchAllProducts();

		if(products != null)
		{
			_totalProducts.Clear();
			_productsInBasket.Clear();

			_totalProducts
				.AddRange(products
				.Select(x => x.ConvertToVM(
					addCommand:    async () => await AddProductToBasket(x.Id),
					removeCommand: async () => await RemoveProductFromBasket(x.Id))));

			//TO DO: ЗАГРУЗКА личной сохраненной ранее корзины пользователя из бд/локального json файла

			result = true;
			return result;
		}

		return result;
	}
	 
	/// <inheritdoc/>
	public async Task<bool> AddProductToBasket(int idProduct)
	{
		var result = default(bool);

		var productInBasket = _productsInBasket.Items.Where(x => x.Id == idProduct).FirstOrDefault();
		if(productInBasket != null)
		{
			//TO DO: КАКАЯ-ТО работа по запросу на сервер, дабы проверить этот плюс и в случае чего записать к себе в БД, дабы пользователь имел сохранную корзину всегда, а не в памяти.

			var countWithPlus = productInBasket.CurrentSelectedCount + 1;

			productInBasket.CurrentSelectedCount = countWithPlus <= productInBasket.CurrentCount ?
												   countWithPlus :
												   productInBasket.CurrentCount;

			var productInTotal = _totalProducts.Items.Where(x => x.Id == idProduct).FirstOrDefault();

			if(productInTotal != null)
			{
				productInTotal.CurrentSelectedCount = productInBasket.CurrentSelectedCount;
			}

			return true;
		}

		var addProduct = _totalProducts.Items.Where(x => x.Id == idProduct).FirstOrDefault();

		if(addProduct != null)
		{
			var countWithPlus = addProduct.CurrentSelectedCount + 1;

			addProduct.CurrentSelectedCount = countWithPlus <= addProduct.CurrentCount ?
											  countWithPlus :
											  addProduct.CurrentCount;

			addProduct = addProduct.Clone();

			_productsInBasket.Add(addProduct);

			return true;
		} 
		
		return result;
	}

	/// <inheritdoc/>
	public async Task<bool> AddProductToBasket(int[] idProducts)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc/>
	public async Task<bool> RemoveProductFromBasket(int[] idProducts)
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc/>
	public async  Task<bool> RemoveProductFromBasket(int idProduct)
	{ 
		var result = default(bool);

		var productInBasket = _productsInBasket.Items.Where(x => x.Id == idProduct).FirstOrDefault();
		if(productInBasket != null)
		{
			//TO DO: КАКАЯ-ТО работа по запросу на сервер, дабы проверить этот минус и в случае чего записать к себе в БД, дабы пользователь имел сохранную корзину всегда, а не в памяти.

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

			return true;
		} 

		return result;
	}


	/// <inheritdoc/>
	public async Task<bool> TotalRemoveProductFromBasket(int idProduct)
	{
		var result = default(bool);

		var productInBasket = _productsInBasket.Items.Where(x => x.Id == idProduct).FirstOrDefault();
		if(productInBasket != null)
		{
			//TO DO: КАКАЯ-ТО работа по запросу на сервер, дабы проверить этот минус и в случае чего записать к себе в БД, дабы пользователь имел сохранную корзину всегда, а не в памяти.

			productInBasket.CurrentSelectedCount = 0; 
			var productInTotal = _totalProducts.Items.Where(x => x.Id == idProduct).FirstOrDefault();

			if(productInTotal != null)
			{
				productInTotal.CurrentSelectedCount = productInBasket.CurrentSelectedCount;
			}

			_productsInBasket.Remove(productInBasket);

			return true;
		}

		return result;
	}

	/// <inheritdoc/>
	public async Task<bool> RemoveAllProductsFromBasket()
	{
		var result = default(bool);

		foreach(var product in _totalProducts.Items)
		{
			await TotalRemoveProductFromBasket(product.Id);
		}

		return true;
	}


	/// <inheritdoc/>
	public async Task<bool> CreateOrder(Guid userId, string address)
	{
		var result = default(bool);

		StringBuilder stringBuilder = new StringBuilder();

		stringBuilder.AppendJoin(';', _productsInBasket.Items.Where(x => x.CurrentSelectedCount > 0).Select(x => $"{x.Id},{x.CurrentSelectedCount}"));

		var products = stringBuilder.ToString();

		if(products.Length > 0)
		{ 
			var orderEntity = new OrderEntity()
			{
				 OrderAddress = address,
				 UserId       = userId,
				 Products     = products
			};

			var orderResponse = await _ordersHttpClient.CreateOrder(orderEntity);
			if(orderResponse != null &&
			   orderResponse.Value)
			{
				await RemoveAllProductsFromBasket();

				return true;
			}
		} 

		return false;
	}

	#endregion
}
