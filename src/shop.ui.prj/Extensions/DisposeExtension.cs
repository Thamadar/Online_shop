using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.UI.Extensions;
public static class DisposeExtension
{ 
	public static void DisposeAll(this ICollection<IDisposable> disposables)
	{
		disposables
			.ToList()
			.ForEach(x => x.Dispose());
		disposables.Clear();
	}
}
