using ReactiveUI;

using System.ComponentModel; 
using System.Runtime.CompilerServices; 

namespace Shop.UI.WPF.Desktop
{
	/// <summary>Базовый класс для ViewModel.</summary>
	public class ViewModelBase : ReactiveObject, IDisposable
	{
		private bool _isDisposed;
		protected IList<IDisposable> _disposables = new List<IDisposable>(); 

		protected virtual void Dispose(bool disposing)
		{
			if(!_isDisposed)
			{
				if(disposing)
				{
					foreach(var disposable in _disposables)
					{
						disposable.Dispose();
					}
					_disposables.Clear();
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
}
