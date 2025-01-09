using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Client.Views.Pages;
public sealed partial class HomeViewModel
{
	public sealed class HomeViewModelCommands
	{
		//public IReactiveCommand OpenHide { get; } 

		public HomeViewModelCommands(HomeViewModel vm)
		{
		 
		}
	}

	private HomeViewModelCommands? _commands;

	public HomeViewModelCommands Commands => _commands ??= new(this);
}
