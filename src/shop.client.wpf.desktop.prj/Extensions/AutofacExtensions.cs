using Autofac;

using System.Windows.Controls;
 
using Shop.UI.WPF;

namespace Shop.Client.WPF.Desktop.Extensions;
public static class AutofacExtensions
{
	public static void RegisterView<TView, TViewModel>(this ContainerBuilder builder)
		where TView      : Control
		where TViewModel : ViewModelBase
	{ 
		builder
			.RegisterType<TViewModel>()
			.AsSelf()
			.SingleInstance();
		 
		builder.RegisterType<TView>()
			   .AsSelf()
			   .OnActivated(e =>
			   {
				   var view = e.Instance;
				   var viewModel = e.Context.Resolve<TViewModel>();
				   view.DataContext = viewModel;
			   });
	}
}
