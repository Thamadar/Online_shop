using Avalonia.Controls;
using System.Reactive; 
using ReactiveUI; 

namespace Shop.Client.Views.Menu;
public sealed partial class MenuViewModel
{
	public sealed class MenuViewModelCommands
	{
		public IReactiveCommand OpenHide { get; }
		public IReactiveCommand CloseApp { get; } 

		public MenuViewModelCommands(MenuViewModel vm)
		{
			OpenHide = ReactiveCommand.Create(() => { vm.OpenStateChange(); });
			CloseApp = ReactiveCommand.Create<Window, Unit>(
				wnd =>
				{
					wnd.Close();
					return Unit.Default;
				}); 
		}
	}

	private MenuViewModelCommands? _commands;

	public MenuViewModelCommands Commands => _commands ??= new(this);
}
