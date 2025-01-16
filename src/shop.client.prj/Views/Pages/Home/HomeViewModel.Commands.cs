using ReactiveUI; 

namespace Shop.Client.Views.Pages;
public sealed partial class HomeViewModel
{
	public sealed class HomeViewModelCommands
	{
		public IReactiveCommand ClearBasket { get; }

		public HomeViewModelCommands(HomeViewModel vm)
		{
			ClearBasket = ReactiveCommand.Create(async () => { await vm.ClearBasket(); });
		}
	}

	private HomeViewModelCommands? _commands;

	public HomeViewModelCommands Commands => _commands ??= new(this);
}
