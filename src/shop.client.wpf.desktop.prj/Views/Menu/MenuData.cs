﻿using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Shop.Client.WPF.Desktop.Extensions;
using Shop.Client.WPF.Desktop.Http;
using Shop.Client.WPF.Desktop.Services;
using Shop.Client.WPF.Desktop.Views.Pages;
using Shop.UI.WPF;

namespace Shop.Client.WPF.Desktop.Views.Menu;
public class MenuData : ViewModelBase
{ 
	private readonly IPageService _pageService;

	private readonly MainInfo _mainInfo; 

	public ObservableCollection<MenuItem> MenuItems { get; } = new ObservableCollection<MenuItem>();
	  
	public MenuData(MainInfo mainInfo)
	{
		_mainInfo = mainInfo;

		_pageService = _mainInfo.GetService<IPageService>();

		_pageService
			.CurrentPageObservable
			.Do(x => { SelectedMenuItem(x); })
			.Subscribe()
			.AddTo(_disposables);

		LoadMenuButtonsData();
	}

	private void SelectedMenuItem(IPage page)
	{
		foreach(var item in MenuItems)
		{
			item.IsSelected = item.PageType == page.GetType() ? true : false;
		}
	}

	private void LoadMenuButtonsData()
	{
		MenuItems.Clear(); 
		MenuItems.Add(new MenuItem(_mainInfo, typeof(HomeViewModel),    "MenuButtonHome",    async _ => await OpenPage<HomeViewModel>(
			_mainInfo.GetItem<IProductsService>(), 
			_mainInfo.GetItem<UsersHttpClient>()))); 
		MenuItems.Add(new MenuItem(_mainInfo, typeof(ProfileViewModel), "MenuButtonProfile", async _ => await OpenPage<ProfileViewModel>())); 
	} 

	private async Task OpenPage<T>(params object[] parameters)
		where T : class, IPage
	{
		var pageService = _mainInfo.GetService<IPageService>();
		var arrParameters = new object[] {_mainInfo}.Concat(parameters).ToArray();
		await pageService.GoToPageAsync<T>(arrParameters);
	} 
}
