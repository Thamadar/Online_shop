using System.Collections.Generic;
using System;
using System.ComponentModel; 
using System.Runtime.CompilerServices;

using Shop.UI.Extensions;
using ReactiveUI;

namespace Shop.UI;

/// <summary>Базовый класс для ViewModel.</summary>
public class ViewModelBase : ReactiveObject, IDisposable
{
	private bool _isDisposed; 

	protected IList<IDisposable> _disposables = new List<IDisposable>();

	public event PropertyChangedEventHandler? PropertyChanged;

	public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		=> PropertyChanged?.Invoke(this, new(propertyName)); 

	protected virtual void Dispose(bool disposing)
	{
		if(!_isDisposed)
		{
			if(disposing)
			{
				_disposables.DisposeAll();
			}

			_isDisposed = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
