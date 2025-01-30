using ReactiveUI; 

namespace Shop.Client.Views.Pages;
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
}
