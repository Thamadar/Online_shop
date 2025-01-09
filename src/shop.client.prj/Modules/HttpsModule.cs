﻿using Autofac;

using Shop.Client.Http; 

namespace Shop.Client.Modules;
public class HttpsModule : Autofac.Module
{
	protected override void Load(ContainerBuilder builder)
	{ 

		builder
			.RegisterType<ProductsHttpClient>()
			.AsSelf()
			.SingleInstance(); 

	}
}
