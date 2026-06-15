using ReactiveUI; 

namespace Shop.Client.Avalonia.Views.Pages;
public sealed partial class HomeViewModel
{
	public sealed class HomeViewModelCommands
	{
		public IReactiveCommand ClearBasket { get; }
		public IReactiveCommand CreateOrder { get; }

		public HomeViewModelCommands(HomeViewModel vm)
		{
			ClearBasket = ReactiveCommand.Create(async () => { await vm.ClearBasket(); });
			CreateOrder = ReactiveCommand.Create(async () => { await vm.CreateOrder(); });
		}
	}

	private HomeViewModelCommands? _commands;

	public HomeViewModelCommands Commands => _commands ??= new(this);
	 
	/// <summary>
	/// Очистка текущей корзины.
	/// </summary>
	/// <returns></returns>
	public async Task ClearBasket()
	{
		await _productsService.RemoveAllProductsFromBasket();
	}

	/// <summary>
	/// Создание заказа
	/// </summary> 
	public async Task CreateOrder()
	{
		//TO DO: auth.
		var userEntity = await _usersHttpClient.GetUserByLogin("admin");

		if(userEntity != null)
		{
			var createOrderSuccess = await _productsService.CreateOrder(userEntity.Id, userEntity.Address);

			if(createOrderSuccess)
			{
				//TO DO: MessageBox: success.
			}
		}
		else
		{
			//TO DO: вывод MessageBoxError.
		}
	}
}
