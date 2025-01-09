using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Shop.Client.Views.Pages;
 
public sealed partial class BasketViewModel
{
	public sealed class BasketViewModelCommands
	{ 

		public BasketViewModelCommands(BasketViewModel vm)
		{
		 
		}
	}

	private BasketViewModelCommands? _commands;

	public BasketViewModelCommands Commands => _commands ??= new(this);
}
