﻿using Autofac;

using Shop.Client.Avalonia.Views; 
using Shop.Client.Avalonia.Views.Pages;

namespace Shop.Client.Avalonia.Modules;

public class ViewModelsModule : Autofac.Module
{ 
	protected override void Load(ContainerBuilder builder)
	{
		builder
			.RegisterType<MainWindowView>() 
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<MainWindowViewModel>() 
			.AsSelf()
			.SingleInstance();

		#region Panels

		builder
			.RegisterType<HomeView>()
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<HomeViewModel>()
			.AsSelf()
			.SingleInstance(); 

		builder
			.RegisterType<ProfileView>()
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<ProfileViewModel>()
			.AsSelf()
			.SingleInstance();

		#endregion

	}
}
