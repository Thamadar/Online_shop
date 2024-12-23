namespace Shop.Client.Extensions;
public static class DisposeExtension
{
	public static void AddTo(this IDisposable item, ICollection<IDisposable> collection)
	{
		if(!collection.Contains(item))
		{
			collection.Add(item);
		}
	}
	public static void DisposeAll(this ICollection<IDisposable> disposables)
	{
		disposables
			.ToList()
			.ForEach(x => x.Dispose());
		disposables.Clear();
	}
}
